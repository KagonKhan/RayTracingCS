﻿using System;
using System.Threading;


namespace RayTracingCS
{

   
    class Program
    {


       



        static void Main(string[] args)
        {


            var canvas_pixels = 2500;
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


            var empty_xs = new Intersection();
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

                    if (!hit.Equals(empty_xs))
                    {
                        var point = r.position(hit.t);
                        var normal = Sphere.Normal((Sphere)hit.obj, point);
                        var eye = -r.direction;

                        Color color = PointLight.Lighting(((Sphere)(hit.obj)).material, light, point, eye, normal);
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
 * */