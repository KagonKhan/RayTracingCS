using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{

    public class Material
    {
        public float ambient;
        public float diffuse;
        public float specular;
        public float shininess;
        public Color color;

        public Material(float amb, float dif, float spe, float shi, in Color col)
        {
            ambient = amb;
            diffuse = dif;
            specular = spe;
            shininess = shi;
            color = col;
        }

        public Material()
        {
            color = new Color(1, 1, 1);
            ambient = 0.1f;
            diffuse = 0.9f;
            specular = 0.9f;
            shininess = 200f;
        }
    }

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
        public LinkedList<Light> lights = new LinkedList<Light>();

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

            lights.AddLast(light);
            objects.AddLast(s1);
            objects.AddLast(s2);
        }



        public List<Intersection> Intersect(in Ray r)
        {

            List<Intersection> retVal = new List<Intersection>();

            foreach (var item in objects)
            {
                var xs = item.intersects(r);

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
            foreach (var light in lights)
            {
                retVal += light.Lighting(comp.obj.material, comp.point, comp.eye, comp.normal);
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
    }
}
