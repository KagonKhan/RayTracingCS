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
        public Material material    = new();
        public Mat4 Transformation  = new(Mat4.I);

        protected HitObject()
        {
            ID = currID++;
        }


        protected abstract Vector LocalNormalAt(in Point p);
        protected abstract List<Intersection> LocalIntersectionsWith(in Ray ray);


        public Vector NormalAt(in Point p)
        {
            Mat4 inv = Transformation.Inversed();
            Point local_point = inv * p;
            Vector local_normal = LocalNormalAt(local_point);
            Vector world_normal = inv.Transposed() * local_normal;
            world_normal.W = 0;

            return world_normal.Normalized();
        }

        public List<Intersection> IntersectionsWith(in Ray ray)
        {
            Ray r = ray.Transform(Transformation.Inversed());
            return LocalIntersectionsWith(r);
        }
    }

    public class TestObject : HitObject
    {

        public Ray saved_ray;

        public TestObject() : base() { }

        protected override List<Intersection> LocalIntersectionsWith(in Ray ray)
        {

            saved_ray = ray;
            return new List<Intersection>();
        }

        protected override Vector LocalNormalAt(in Point p)
        {
            Vector norm = (p - new Point(0, 0, 0));
            // careful for w != 0
            return norm.Normalized();
        }
    }


    public class Sphere : HitObject
    {

        public Sphere() : base() {}
    
        
        protected override List<Intersection> LocalIntersectionsWith(in Ray ray)
        {

            Vector sphereToRay = ray.origin - new Point(0, 0, 0);

            double a = ray.direction.Dot(ray.direction);
            double b = 2 * ray.direction.Dot(sphereToRay);
            double c = sphereToRay.Dot(sphereToRay) - 1;

            double delta = b * b - 4 * a * c;
    
            if (delta < 0)
                return new List<Intersection>();
            else
            {
                double t1 = (-b - Math.Sqrt(delta)) / (2 * a);
                double t2 = (-b + Math.Sqrt(delta)) / (2 * a);


                Console.WriteLine(t1 + ", " + t2);
                return new List<Intersection> { new(this, t1), new(this, t2) };
            }
        }


        protected override Vector LocalNormalAt(in Point p)
        {
            Vector norm = (p - new Point(0, 0, 0));
            // careful for w != 0
            return norm.Normalized();
        }
    }
    
    public class Plane : HitObject
    {
        public Plane() : base() {}

        protected override List<Intersection> LocalIntersectionsWith(in Ray ray)
        {
            if (Math.Abs(ray.direction.Y) < MatMaths.eps)
                return new List<Intersection>();

            double t = -ray.origin.Y / ray.direction.Y;

            return new List<Intersection>() { new Intersection(this, t) };

        }
        protected override Vector LocalNormalAt(in Point p)
        {
            return new Vector(0, 1, 0);
        }
    }
}
