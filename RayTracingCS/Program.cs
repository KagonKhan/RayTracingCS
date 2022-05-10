using System;
using System.Windows;

namespace RayTracingCS
{

   
    public static class Program
    {
        const double pi = Math.PI;

        static public Canvas canvas;


        static World w;
        static Camera c;


        static public void InitRealTime(int width, int height)
        {
            Plane floor = new Plane();
            floor.Transformation = MatMaths.Translation(0, -1, 0);
            floor.material.color = new Color(1, 1, 1);
            floor.material.specular = 0;
            floor.material.reflective = 0;

            Plane back_wall = new Plane();
            back_wall.Transformation = MatMaths.I.Translated(0, 2, 0.5).RotatedX(pi / 2);
            back_wall.material.color = new Color(1, 1, 1);
            back_wall.material.specular = 0;
            back_wall.material.reflective = 0.7;





            Sphere middle = new Sphere();
            middle.Transformation = MatMaths.Translation(0, 1.9, -1.5);
            middle.material.color = new Color(0.1, 0.1, 0.1);
            middle.material.ambient = 0.5f;
            middle.material.transparency = 1.0;
            middle.material.refraction = 1.5;

            Sphere right = new Sphere();
            right.Transformation = MatMaths.I.Translated(3, 0.5, -1.5).Scaled(0.25, 0.25, 0.25);
            right.material.color = new Color(1, 1, 1);
            right.material.diffuse = 0.7f;
            right.material.transparency = 0.3;
            right.material.refraction = 1.00029;



            Sphere left = new Sphere();
            left.Transformation = MatMaths.I.Translated(-1.5, 0.33, -0.75).Scaled(0.33, 0.33, 0.33);
            left.material.color = new Color(1, 0.8, 0.1);
            left.material.diffuse = 0.7f;
            left.material.specular = 0.3f;
            left.material.reflective = 0.5;


            var one = new StripePattern(Pattern.Black, Pattern.White);
            var two = new StripePattern(Pattern.White, Pattern.Black);
            two.Transformation = Mat4.I.RotatedY(pi / 2);

            //back_wall.material.pattern = new NestedPattern(one,two);
            //floor.material.pattern = new Checkered2DPattern(Pattern.Black, Pattern.White);
            back_wall.material.pattern = new RadialGradientPattern(Pattern.Black, Pattern.White);
            left.material.pattern = new RadialGradientPattern(new Color(1, 0, 1), new Color(0, 1, 1));
            right.material.pattern = new NestedPattern(new StripePattern(Pattern.Cyan, Pattern.Magenta), new RingPattern(Pattern.Maroon, Pattern.Gray));
            //middle.material.pattern = new NestedPattern(new StripePattern(Pattern.Cyan, Pattern.Magenta), new RingPattern(Pattern.Maroon, Pattern.Gray));
            //middle.material.pattern.Transformation = Mat4.I.RotatedY(-0.5).RotatedX(-0.3).Scaled(0.03125, 0.125, 0.6125);





            w = new World(floor, back_wall, middle, right, left);
            w.lights.Add(new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1)));
            //w.lights.Add(new PointLight(new Point( 10, 10, -10), new Color(0.66, 0.66, 0.66)));


            c = new Camera(width, height, Math.PI / 3);
            c.transform = MatMaths.ViewTransform(new Point(0, 2, -7), new Point(0, 1, 0), new Vector(0, 1, 0));
        }


        static public Color RenderRealTime()
        {
            return Canvas.ClampR(c.RenderRealTime(w) * 255);
        }

        static public Color RenderRealTime(int y, int x)
        {
            return Canvas.ClampR(c.RenderRealTime(y, x, w) * 255);
        }


        public static void Main()
        {
            InitRealTime(7680 / 16, 4320 / 16);

            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
                canvas = c.Render(w);
            watch.Stop();

            long elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);


            watch = System.Diagnostics.Stopwatch.StartNew();
            //canvas.ToPPM();
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