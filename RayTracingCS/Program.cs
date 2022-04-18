using System;
using System.Threading;


namespace RayTracingCS
{
    class Program
    {
        static void Main(string[] args)
        {

            var canvas_pixels = 1000;
            Canvas canvas = new(canvas_pixels, canvas_pixels);
            canvas.Flush(Color.Black);


            Point ray_origin    = new Point(0, 0, -5);
            double wall_z       = 10.0d;
            double wall_size    = 7.0d;
            double pixel_size   = wall_size / canvas_pixels;
            double half         = wall_size / 2;

            var s = new Sphere(new Point(0,0,0), 1d);
            //s.Transformation = Mat4.Scaling(250, 250, 1);


            var empty_xs = new Intersection<Sphere>();
            var r = new Ray(ray_origin, (new Point(0,0,0) - ray_origin).Normalize());

            for (int x = 0; x < canvas_pixels; x++) 
            {
                for (int y = 0; y < canvas_pixels; y++)
                {
                    var world_y = half - pixel_size * y;
                    var world_x = -half + pixel_size * x;
                    var pos = new Point(world_x, world_y, wall_z);

                    r.direction = (pos - ray_origin).Normalize();


                    var xs = s.intersects(r);
                    if (!Intersection<Sphere>.Hit(xs).Equals(empty_xs))
                        canvas.WritePixel(x, y, Color.Red);


                    if ((x * canvas_pixels + y) % 5000 == 0)
                        Console.WriteLine($"{(x * canvas_pixels + y)} out of {canvas_pixels * canvas_pixels}");

                }
            }

            
            canvas.ToPPM();


            

        }
    }
}





/*                      Benchmarking v1
 *                      
 * Canvas size     10000    1000    500     250     100     50
 * Time[ms]        35min    12042   4100    900     160     50
 * 
 * 
 * 
 *                      Benchmarking v2
 * Canvas size     10000    1000    500     250     100     50
 * Time[ms]         7min    5299   1700     430      86     40                        
 *                      Benchmarking v3
 * Canvas size
 * Time
 *                      Benchmarking v4
 * Canvas size
 * Time
 *                      Benchmarking v5
 * Canvas size
 * Time
 *                      Benchmarking v6
 * Canvas size
 * Time
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * */