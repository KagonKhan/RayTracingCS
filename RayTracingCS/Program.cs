using System;
using System.Threading;


namespace RayTracingCS
{

   
    class Program
    {

        static void Main(string[] args)
        {
            #region spheres
            Sphere floor = new Sphere();
            floor.Transformation = MatMaths.I.Scaled(10, 0.01, 10);
            floor.material = new Material();
            floor.material.color = new Color(1, 0.9, 0.9);
            floor.material.specular = 0;


            Sphere left_wall = new Sphere();
            left_wall.Transformation = MatMaths.I.Translated(0, 0, 5).
                                                  RotatedY(-Math.PI / 4).
                                                  RotatedX(Math.PI / 2).
                                                  Scaled(10, 0.01, 10);
            left_wall.material = floor.material;

            Sphere right_wall = new Sphere();
            right_wall.Transformation = MatMaths.I.Translated(0, 0, 5).
                                                  RotatedY(Math.PI / 4).
                                                  RotatedX(Math.PI / 2).
                                                  Scaled(10, 0.01, 10);
            right_wall.material = left_wall.material;




            Sphere middle = new Sphere();
            middle.Transformation = MatMaths.I.Translated(-0.5, 1, 0.5);
            middle.material = new Material();
            middle.material.color = new Color(0.1, 1, 0.5);
            middle.material.diffuse = 0.7f;
            middle.material.specular = 0.3f;

            Sphere right = new Sphere();
            right.Transformation = MatMaths.I.Translated(1.5, 0.5, -0.5).Scaled(0.5, 0.5, 0.5);
            right.material = new Material();
            right.material.color = new Color(0.5, 1, 0.1);
            right.material.diffuse = 0.7f;
            right.material.specular = 0.3f;

            Sphere left = new Sphere();
            left.Transformation = MatMaths.I.Translated(-1.5, 0.33, -0.75).Scaled(0.33, 0.33, 0.33);
            left.material = new Material();
            left.material.color = new Color(1, 0.8, 0.1);
            left.material.diffuse = 0.7f;
            left.material.specular = 0.3f;
            #endregion


            World w = new World(floor, left_wall, right_wall, middle, right, left);
            w.lights.Add(new PointLight(new Point(-10, 10, -10), new Color(0.76, 0.66, 0.66)));
            w.lights.Add(new PointLight(new Point( 10, 10, -10), new Color(0.76, 0.66, 0.66)));


            Camera c = new Camera(7680/8, 4320/8, Math.PI / 3);
            c.transform = MatMaths.ViewTransform(new Point(0, 1.5, -5), new Point(0, 1, 0), new Vector(0, 1, 0));

            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            Canvas canvas = c.Render(w);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);

            watch = System.Diagnostics.Stopwatch.StartNew();
            canvas.ToPPM();
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);



        }
    }
}





/*                      Benchmarking v1 (10 GB mem)
 *                      
 * Canvas size     10000    1000    500     250     100     50
 * Time[ms]        35min    12042   4100    900     160     50
 * 
 * 
 * 
 *                      Benchmarking v2 (everything ref structed, mult optimizations, arg in opt, 2.5 GB mem)
 * Canvas size     10000    1000    500     250     100     50
 * Time[ms]         7min    5299   1700     430      86     40                        
 * 
 * 
 * 
 *                      Benchmarking v3 (compared to V1 - new inverse method - checks, 10GB mem)
 * Canvas size     10000    1000    500     250     100     50
 * Time[ms]         5min    3500   1050     240      55     27  
 * 
 * 
 *                      Benchmarking v4 (compared to V1 - new inverse method - no checks, 9GB mem)
 * Canvas size     10000    1000    500     250     100     50
 * Time[ms]         2min    1600   500     115      30     <20  
 * 
 * 
 * 
 * 
 *                      Benchmarking v5 2.5GB
 * Canvas size     10000    1000    500     250     100     50
 * Time[ms]         50s
 * 
 * 
 * 
 * 
 *                      Benchmarking v6 inverse loop unroll 
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
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * var canvas_pixels = 200;
            Canvas canvas = new(canvas_pixels, canvas_pixels);
            canvas.Flush(Color.Black);


            Point ray_origin    = new Point(0, 0, -5);
            double wall_z       = 10.0d;
            double wall_size    = 7.0d;
            double pixel_size   = wall_size / canvas_pixels;
            double half         = wall_size / 2;

            var s = new Sphere(new Point(0,0,0), 1d);
            s.material = new Material(0.1f, 0.9f, 0.9f, 200f, new Color(1, 0.2, 1));


            var light_position = new Point(-10, 10, -10);
            var light_color = new Color(255, 255, 255);
            var light = new PointLight(light_position, light_color);








            //s.Transformation = Mat4.Scaling(250, 250, 1);


            var r = new Ray(ray_origin, (new Point(0,0,0) - ray_origin).Normalized());


            var watch = System.Diagnostics.Stopwatch.StartNew();


            for (int x = 0; x < canvas_pixels; x++) 
            {
                for (int y = 0; y < canvas_pixels; y++)
                {
                    var world_y =  half - pixel_size * y;
                    var world_x = -half + pixel_size * x;
                    var pos = new Point(world_x, world_y, wall_z);

                    r.direction = (pos - ray_origin).Normalized();


                    var xs = s.intersects(r);
                    var hit = Intersection.Hit(xs);

                    if (hit != null)
                    {
                        var point = r.position(hit.t);
                        var normal = Sphere.Normal((Sphere)hit.obj, point);
                        var eye = -r.direction;

                        Color color = light.Lighting(((Sphere)(hit.obj)).material, point, eye, normal);
                        canvas.WritePixel(x, y, color);
                    }


                    if ((x * canvas_pixels + y) % 5000 == 0)
                        Console.WriteLine($"{(x * canvas_pixels + y)} out of {canvas_pixels * canvas_pixels}");

                }
            }
            
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);



            canvas.ToPPM();
 * 
 * */