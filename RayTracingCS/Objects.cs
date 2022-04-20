using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{

    abstract public class HitObject
    {
        public static int currID = 0;
        public int ID;
        public Material material =   new Material();
        public Mat4 Transformation = new Mat4(Mat4.I);

        protected HitObject()
        {
            ID = currID++;
        }

        abstract public List<Intersection> intersects(in Ray ray);
        public abstract Vector LocalNormalAt(in Point p);
        public Vector NormalAt(in Point p)
        {
            Mat4 inv = Transformation.Inversed();
            Point local_point = inv * p;
            Vector local_normal = LocalNormalAt(local_point);
            Vector world_normal = inv.Transposed() * local_normal;
            world_normal.W = 0;

            return world_normal.Normalized();
        }
    }

  
    public class Sphere : HitObject
    {

        public Sphere() : base() {}
    
        
        public override List<Intersection> intersects(in Ray ray)
        {
            
            var r = ray.Transform(Transformation.Inversed());
    
            var sphereToRay = r.origin - new Point(0, 0, 0);
    
            var a = r.direction.Dot(r.direction);
            var b = 2 * r.direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;
    
            var disc = Math.Pow(b, 2) - 4 * a * c;
    
            if (disc < 0)
                return new List<Intersection>();
            else
            {
                var t1 = (-b - Math.Sqrt(disc)) / (2 * a);
                var t2 = (-b + Math.Sqrt(disc)) / (2 * a);
        
                return new List<Intersection> { new(this, t1), new(this, t2) };
            }
        }


        public override Vector LocalNormalAt(in Point p)
        {
            Vector norm = (p - new Point(0, 0, 0));
            norm.W = 0;
            return norm.Normalized();
        }
    }
}
