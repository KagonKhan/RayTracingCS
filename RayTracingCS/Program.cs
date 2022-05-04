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
            var wall_material = new Material();
            wall_material.pattern = new StripePattern(new Color(0.45, 0.45, 0.45), new Color(0.55, 0.55, 0.55));
            wall_material.pattern.Transformation = Mat4.I.RotatedY(1.5708).Scaled(0.25, 0.25, 0.25);
            wall_material.ambient = 0;
            wall_material.diffuse = 0.4f;
            wall_material.specular = 0;
            wall_material.reflective = 0.3;


            Plane floor = new Plane();
            floor.Transformation = MatMaths.I.RotatedY(0.31415);
            floor.material = new Material();
            floor.material.pattern = new Checkered2DPattern(new Color(0.35, 0.35, 0.35), new Color(0.65, 0.65, 0.65));
            floor.material.specular = 0;
            floor.material.reflective = 0.4;

            Plane ceiling = new Plane();
            ceiling.Transformation = MatMaths.I.Translated(0, 5, 0);
            ceiling.material = new Material();
            ceiling.material.color = new Color(0.8, 0.8, 0.8);
            ceiling.material.ambient = 0.3f;
            ceiling.material.specular = 0;


            Plane left_wall = new Plane();
            left_wall.Transformation = MatMaths.I.Translated(-5, 0, 0).RotatedY(1.5708).RotatedZ(1.5708);
            left_wall.material = wall_material;

            Plane right_wall = new Plane();
            right_wall.Transformation = MatMaths.I.Translated(5, 0, 0).RotatedY(1.5708).RotatedZ(1.5708);
            right_wall.material = wall_material;


            Plane front_wall = new Plane();
            front_wall.Transformation = MatMaths.I.Translated(0, 0, 5).RotatedX(1.5708);
            front_wall.material = wall_material;

            Plane back_wall = new Plane();
            back_wall.Transformation = MatMaths.I.Translated(0, 0, -5).RotatedX(1.5708);
            back_wall.material = wall_material;


            Sphere s1 = new Sphere();
            s1.Transformation = Mat4.I.Translated(4.6, 0.4, 1).Scaled(0.4, 0.4, 0.4);
            s1.material = new Material();
            s1.material.color = new Color(0.8, 0.5, 0.3);
            s1.material.shininess = 50f;

            Sphere s2 = new Sphere();
            s2.Transformation = Mat4.I.Translated(4.7, 0.3, 0.4).Scaled(0.3, 0.3, 0.3);
            s2.material = new Material();
            s2.material.color = new Color(0.9, 0.4, 0.5);
            s2.material.shininess = 50f;

            Sphere s3 = new Sphere();
            s3.Transformation = Mat4.I.Translated(-1, 0.5, 4.5).Scaled(0.5, 0.5, 0.5);
            s3.material = new Material();
            s3.material.color = new Color(0.4, 0.9, 0.6);
            s3.material.shininess = 50f;

            Sphere s4 = new Sphere();
            s4.Transformation = Mat4.I.Translated(-1.7, 0.3, 4.7).Scaled(0.3, 0.3, 0.3);
            s4.material = new Material();
            s4.material.color = new Color(0.4, 0.6, 0.9);
            s4.material.shininess = 50f;


            Sphere s5 = new Sphere();
            s5.Transformation = Mat4.I.Translated(-0.6, 1, 0.6);
            s5.material = new Material();
            s5.material.color = new Color(1, 0.3, 0.2);
            s5.material.shininess = 5f;
            s5.material.specular = 0.4f;

            Sphere s6 = new Sphere();
            s6.Transformation = Mat4.I.Translated(0.6, 0.7, -0.6).Scaled(0.7, 0.7, 0.7);
            s6.material = new Material();
            s6.material.color = new Color(0, 0, 0.2);
            s6.material.ambient = 0.0f;
            s6.material.diffuse = 0.4f;
            s6.material.specular = 0.9f;
            s6.material.shininess = 300f;
            s6.material.reflective = 0.9;
            s6.material.transparency = 0.9;
            s6.material.refraction = 1.5;


            Sphere s7 = new Sphere();
            s7.Transformation = Mat4.I.Translated(-0.7, 0.5, -0.8).Scaled(0.5, 0.5, 0.5);
            s7.material = new Material();
            s7.material.color = new Color(0, 0.2, 0);
            s7.material.ambient = 0.0f;
            s7.material.diffuse = 0.4f;
            s7.material.specular = 0.9f;
            s7.material.shininess = 300f;
            s7.material.reflective = 0.9;
            s7.material.transparency = 0.9;
            s7.material.refraction = 1.5;


            c = new Camera(width, height, 1.152);
            c.transform = MatMaths.ViewTransform(new Point(-2.6, 1.5, -3.9), new Point(-0.6, 1, -0.8), new Vector(0, 1, 0));

            w = new World(floor, ceiling, front_wall, left_wall, right_wall, back_wall, s1, s2, s3, s4, s5, s6, s7);
            w.lights.Add(new PointLight(new Point(-4.9, 4.9, -1), new Color(1, 1, 1)));
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