using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{

    public struct PointLight
    {
        public Point pos;
        public Color intensity;

        public PointLight(Point p, Color i)
        {
            pos = p;
            intensity = i;
        }


        public static Color Lighting(in Material m, in PointLight l, in Point p, in Vector eye, in Vector norm)
        {
            var eff_color = m.color * l.intensity;
            var ambient = eff_color * m.ambient;
            
            
            var lightV = MatMaths.Norm(l.pos - p);

            Color dif;
            Color spec;

            var light_dot_normal = MatMaths.Dot(lightV, norm);
            if (light_dot_normal < 0)
            {
                dif = Color.Black;
                spec = Color.Black;
            }
            else
            {
                dif = eff_color * m.diffuse * light_dot_normal;

                var reflectv = Vector.Reflect(-lightV, norm);
                var reflect_dot_eye = MatMaths.Dot(reflectv, eye);

                if (reflect_dot_eye <= 0)
                    spec = Color.Black;
                else
                {
                    var factor = Math.Pow(reflect_dot_eye, m.shininess);
                    spec = l.intensity * m.specular * factor;

                }

            }
            Color c = ambient + dif + spec;
            Clamp(ref (c));

            return c;
        }


        public static void Clamp(ref Color c)
        {
            c.r = Math.Clamp(c.r,0,255);
            c.g = Math.Clamp(c.g,0,255);
            c.b = Math.Clamp(c.b,0,255);
        }
    }



    public struct Material
    {
        public float ambient;
        public float diffuse;
        public float specular;
        public float shininess;
        public Color color;

        public Material(float amb, float dif, float spe, float shi, in Color col)
        {
            ambient=amb;
            diffuse = dif;
            specular = spe;
            shininess = shi;
            color = col;
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
    
    abstract public class HitObject
    {
        public static int currID = 0;
        public int ID;

        public HitObject()
        {
            ID = currID++;
        }

        abstract public Intersection[] intersects(in Ray ray);
    }

    public class Intersection
    {
        public HitObject? obj;
        public double t;

        public Intersection()
        {
            obj = null;
            t = 0;
        }

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

        public static Intersection Hit(params Intersection[] xs)
        {
    
            Array.Sort(xs, delegate (Intersection a, Intersection b) { return a.t > b.t ? 1 : a.t == b.t ? 0 : -1; });
    
            for (int i = 0; i < xs.Length; i++)
                if (xs[i].t > 0d)
                    return xs[i];
    
            return new Intersection();
        }
    }
    
    public class Sphere : HitObject
    {
        public Point position;
        double radius;
        public Mat4 Transformation;
        public Material material;


        public Sphere(in Point pos, double r)
        {
            radius = r;
            position = pos;
            Transformation =  new Mat4(Mat4.I);
        }
        public Sphere()
        {
            radius = 1;
            position = new Point(0,0,0);
            Transformation =  new Mat4(Mat4.I);
        }
    
        public void SetTransform(in Mat4 transformation)
        {
            Transformation = transformation;
        }
        
        public override Intersection[] intersects(in Ray ray)
        {
            
            var r = ray.Transform(Transformation.GetInverse());
    
            var sphereToRay = r.origin - new Point(0, 0, 0);
    
            var a = r.direction.Dot(r.direction);
            var b = 2 * r.direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;
    
            var disc = Math.Pow(b, 2) - 4 * a * c;
    
            if (disc < 0)
                return Array.Empty<Intersection>();
            else
            {
                var t1 = (-b - Math.Sqrt(disc)) / (2 * a);
                var t2 = (-b + Math.Sqrt(disc)) / (2 * a);
    
                var o1 = new Intersection(this, t1);
                var o2 = new Intersection(this, t2);
    
                return new Intersection[2] { o1, o2 };
            }
        }


        public Vector Normal(in Point p)
        {
            Mat4 t_inv = Transformation.Inverse();

            Point obj_p = t_inv * p;
            Vector obj_norm = obj_p - position;
            Vector world_norm = t_inv.Transposed() * obj_norm;
            world_norm.W = 0;

            return world_norm.Normalized();
        }
        
        public static Vector Normal(Sphere s, in Point p)
        {
            Mat4 t_inv = s.Transformation.Inverse();

            Point obj_p = t_inv * p;
            Vector obj_norm = obj_p - s.position;
            Vector world_norm = t_inv.Transposed() * obj_norm;
            world_norm.W = 0;

            return world_norm.Normalized();
        }
        


    }
}
