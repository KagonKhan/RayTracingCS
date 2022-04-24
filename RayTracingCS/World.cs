﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{




    public struct Ray
    {
        public Point origin;
        public Vector direction;

        public Ray(in Point o, in Vector d)
        {
            origin = o;
            direction = d;
        }

        public Point position(double t)
        {
            return direction * t + origin;
        }

        public Ray Transform(in Mat4 transformation)
        {
            return new Ray(transformation * origin, transformation * direction);
        }
    }

    public struct Computations
    {
        public double t;
        public HitObject obj;
        public Point point;
        public Point over_point;
        public Vector eye;
        public Vector normal;
        public bool inside;

        public Computations(double t, in HitObject obj, in Point p, in Vector eye, in Vector norm, bool inside)
        {
            this.t = t;
            this.obj = obj;
            this.point = p;
            this.eye = eye;
            this.normal = norm;
            this.inside = inside;
            this.over_point = p;

            over_point = p + normal * (MatMaths.eps);
        }
    }



    public class Intersection
    {
        public HitObject? obj;
        public double t;

        public Intersection(in HitObject obj, double t)
        {
            this.obj = obj;
            this.t = t;
        }

        public override bool Equals(object obj)
        {
            if (obj is Intersection p)
            {
                return this.obj == p.obj
                    && t == p.t;
            }

            return false;
        }

        public static Intersection Hit(in List<Intersection> xs)
        {
            if (xs == null) return null;


            xs.Sort((Intersection a, Intersection b) => MatMaths.SpaceshipOp(a.t, b.t));

            for (int i = 0; i < xs.Count; i++)
                if (xs[i].t > 0d)
                    return xs[i];

            return null;
        }

        public Computations Compute(in Ray r)
        {
            var pos = r.position(t);
            var norm = obj.NormalAt(pos);
            bool inside;
                
            if (norm.Dot(-r.direction) < 0)
            {
                inside = true;
                norm = -norm;
            }
            else
                inside = false;

            return new Computations(t, obj, pos, -r.direction, norm, inside);
        }
    }


    public class World
    {
        // Possibly a dictionary with IDs
        public LinkedList<HitObject> objects = new LinkedList<HitObject>();
        public List<Light> lights = new List<Light>();

        public World(params HitObject[] objs)
        {
            for (int i = 0; i < objs.Length; i++)
                objects.AddLast(objs[i]);
        }
        public World()
        {
            var light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
            var s1 = new Sphere();
            var s2 = new Sphere();

            s1.material.color = new Color(0.8f, 1.0f, 0.6f);
            s1.material.diffuse = 0.7f;
            s1.material.specular = 0.2f;

            s2.Transformation = MatMaths.Scaling(0.5, 0.5, 0.5);

            lights.Add(light);
            objects.AddLast(s1);
            objects.AddLast(s2);
        }



        public List<Intersection> Intersect(in Ray r)
        {

            List<Intersection> retVal = new List<Intersection>();

            foreach (var item in objects)
            {
                var xs = item.IntersectionsWith(r);

                if (xs != null)
                {
                    retVal.AddRange(xs);
                }
            }

            retVal.Sort((Intersection a, Intersection b) => MatMaths.SpaceshipOp(a.t, b.t));

            return retVal;
        }

        public Color Shading(in Computations comp)
        {
            Color retVal = Color.Black;

            int lightIndex = 0;
            foreach (var light in lights)
            {
                bool shaded = IsShadowed(comp.over_point, lightIndex);
                retVal += light.Lighting(comp.obj.material, comp.obj, comp.point, comp.eye, comp.normal, shaded);

                lightIndex++;
            }

            return retVal;
        }
    
        public Color Coloring(in Ray r)
        {
            var xs = Intersect(r);
            var hit = Intersection.Hit(xs);


            if (hit == null)
            {
                return Color.Black;
            }
            else {
                Computations comp = hit.Compute(r);
                return Shading(comp);
            }
        }

        
        public bool IsShadowed(in Point p, int lightIndex)
        {
            Vector v = lights[lightIndex].pos - p;

            double distance = v.Magnitude();
            Vector direction = v.Normalized();

            Ray r = new Ray(p, direction);

            var xs = Intersect(r);
            var h = Intersection.Hit(xs);

            if (h != null && h.t < distance)
                return true;
            else
                return false;
        }
    }


    public class Camera
    {
        public int width;
        public int height;
        public double fov;
        public Mat4 transform;
        public double pxsize;
        public double halfWidth;
        public double halfHeight;

        public Camera(int w, int h, double fov)
        {
            width = w;
            height = h;
            this.fov = fov;
            transform = MatMaths.I;

            var half_view = Math.Tan(fov / 2);
            double aspect = (double)width / height;

            if (aspect >= 1)
            {
                halfWidth = half_view;
                halfHeight = half_view / aspect;
            }
            else
            {
                halfWidth = half_view * aspect;
                halfHeight = half_view;
            }

            pxsize = 2d * halfWidth / width;
        }

        public Ray Ray(int row, int col)
        {
            var xoffset = (col + 0.5) * pxsize;
            var yoffset = (row + 0.5) * pxsize;

            var world_x = halfWidth - xoffset;
            var world_y = halfHeight - yoffset;

            var camInverse = transform.Inversed();

            var pixel = camInverse * new Point(world_x, world_y, -1);
            var origin = camInverse * new Point(0, 0, 0);
            var dir = (pixel - origin).Normalized();

            return new Ray(origin, dir);
        }

        public Canvas Render(in World w)
        {
            Canvas retVal = new Canvas(height, width);

            Ray r;
            Color c; 

            for(int row = 0; row < height; row++)
                for(int col = 0; col < width; col++)
                {
                    r = Ray(row, col);
                    c = w.Coloring(r);

                    retVal.WritePixel(row, col, c);

                    if ((row * width + col) % 10000 == 0)
                        Console.WriteLine($"{((row * width + col)/1000)} out of {height * width / 1000}");

                }


            return retVal;
        }


        static int row = 0, col = 0;
        public Color RenderRealTime(in World w)
        {

            Ray r = Ray(row, col);
            Color c = w.Coloring(r);

            col++;
            if(col >= width)
            {
                col = 0;
                row++;
            }

            return c;
        }
    }
}