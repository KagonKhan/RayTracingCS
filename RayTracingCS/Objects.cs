using System;
using System.Collections.Generic;


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
            Ray r = ray.Transformed(Transformation.Inversed());

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
            norm.W=0;
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

                return new List<Intersection> { new(this, t1), new(this, t2) };
            }
        }


        protected override Vector LocalNormalAt(in Point p)
        {
            Vector norm = (p - new Point(0, 0, 0));
            norm.W = 0;
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

    public class Cube : HitObject
    {
        public Cube() : base() { }
        protected override List<Intersection> LocalIntersectionsWith(in Ray ray)
        {
            (double xtmin, double xtmax) = CheckAxis(ray.origin.X, ray.direction.X);
            (double ytmin, double ytmax) = CheckAxis(ray.origin.Y, ray.direction.Y);
            (double ztmin, double ztmax) = CheckAxis(ray.origin.Z, ray.direction.Z);

            double tmin = MatMaths.Max(xtmin, ytmin, ztmin);
            double tmax = MatMaths.Min(xtmax, ytmax, ztmax);

            if(tmin > tmax)
                return new List<Intersection>();
            else
                return new List<Intersection>()
                       {
                            new Intersection(this, tmin),
                            new Intersection(this, tmax)
                       };

        }
        protected override Vector LocalNormalAt(in Point p)
        {
            Point abs = p.AsAbs();
            double max = MatMaths.Max(abs.X, abs.Y, abs.Z);

            if(max == abs.X)
                return new Vector(p.X, 0, 0);
            if(max == abs.Y)
                return new Vector(0, p.X, 0);
            else
                return new Vector(0, 0, p.Z);
        }


        private (double min, double max) CheckAxis(double origin, double direction)
        {
            double min_numerator = -1 - origin;
            double max_numerator =  1 - origin;

            direction = 1.0 / direction;
            double tmin = min_numerator * direction;
            double tmax = max_numerator * direction;
            

            if (tmin > tmax)
                return (tmax, tmin);
            else
                return (tmin, tmax);

        }
    }

    public class Cylinder : HitObject
    {
        private bool closed;
        private double ymin;
        private double ymax;
        
        public Cylinder(bool closed = false, double min = double.NegativeInfinity, double max = double.PositiveInfinity) : base()
        {
            this.closed = closed;
            ymin = min;
            ymax = max;
        }

        

        protected override List<Intersection> LocalIntersectionsWith(in Ray ray)
        {
            double a = Math.Pow(ray.direction.X, 2) + Math.Pow(ray.direction.Z, 2);

            if (Math.Abs(a) < MatMaths.eps)
                return new();

            double b = 2 * ray.origin.X * ray.direction.X +
                       2 * ray.origin.Z * ray.direction.Z;
            double c = Math.Pow(ray.origin.X, 2) + Math.Pow(ray.origin.Z, 2) - 1;
            double d = b*b - 4*a*c;

            if (d < 0)
                return new();


            double t0 = (-b - Math.Sqrt(d))/ (2 * a);
            double t1 = (-b + Math.Sqrt(d))/ (2 * a);

            if (t0 > t1)
                (t0, t1) = (t1, t0);

            List<Intersection> retVal = new();

            double y0 = ray.origin.Y + t0 * ray.direction.Y;
            if (ymin < y0 && y0 < ymax)
                retVal.Add(new(this, t0));

            double y1 = ray.origin.Y + t1 * ray.direction.Y;
            if (ymin < y1 && y1 < ymax)
                retVal.Add(new(this, t1));

            IntersectEnds(ray, ref retVal);


            return retVal;
        }
        protected override Vector LocalNormalAt(in Point p)
        {
            double dist = Math.Pow(p.X, 2) + Math.Pow(p.Z, 2);

            if(dist < 1 && p.Y >= ymax - MatMaths.eps)
                return new Vector(0, 1, 0);

            if(dist < 1 && p.Y <= ymin + MatMaths.eps)
                return new Vector(0, -1, 0);

            return new Vector(p.X, 0, p.Z);
        }


        private bool CheckEnds(in Ray r, double t)
        {
            double x = r.origin.X + t * r.direction.X;
            double z = r.origin.Z + t * r.direction.Z;

            return (Math.Pow(x, 2) + Math.Pow(z, 2)) <= 1;
        }
        private void IntersectEnds(in Ray r, ref List<Intersection> xs)
        {
            if (!closed || r.direction.Y <= MatMaths.eps)
                return;

            double t = (ymin - r.origin.Y) / r.direction.Y;
            if (CheckEnds(r, t))
                xs.Add(new (this, t));

            t = (ymax - r.origin.Y) / r.direction.Y;
            if (CheckEnds(r, t))
                xs.Add(new(this, t));

        }
    }

    public class Cone : HitObject
    {
        private bool closed;
        private double ymin;
        private double ymax;

        public Cone(bool closed = false, double min = double.NegativeInfinity, double max = double.PositiveInfinity) : base()
        {
            this.closed = closed;
            ymin = min;
            ymax = max;
        }
        protected override List<Intersection> LocalIntersectionsWith(in Ray ray)
        {
            ray.direction.Normalize();
            double a = Math.Pow(ray.direction.X, 2) -
                       Math.Pow(ray.direction.Y, 2) +  
                       Math.Pow(ray.direction.Z, 2);

            double b = 2.0 * ray.origin.X * ray.direction.X -
                       2.0 * ray.origin.Y * ray.direction.Y +
                       2.0 * ray.origin.Z * ray.direction.Z;

            double c = Math.Pow(ray.origin.X, 2) -
                       Math.Pow(ray.origin.Y, 2) +
                       Math.Pow(ray.origin.Z, 2);

            double d = b*b - 4.0*a*c;

            if (Math.Abs(a) < MatMaths.eps && Math.Abs(b) < MatMaths.eps)
                return new();



            if (d < 0)
                return new();

            if (Math.Abs(a) < MatMaths.eps) {
                double t = -c / (2*b);
                return new() {
                    new(this, t)
                };
            }
            double t0 = (-b - Math.Sqrt(d))/ (2 * a);
            double t1 = (-b + Math.Sqrt(d))/ (2 * a);

            if (t0 > t1)
                (t0, t1) = (t1, t0);

            List<Intersection> retVal = new();

            double y0 = ray.origin.Y + t0 * ray.direction.Y;
            if (ymin < y0 && y0 < ymax)
                retVal.Add(new(this, t0));

            double y1 = ray.origin.Y + t1 * ray.direction.Y;
            if (ymin < y1 && y1 < ymax)
                retVal.Add(new(this, t1));

            IntersectEnds(ray, ref retVal);


            return retVal;
        }
        protected override Vector LocalNormalAt(in Point p)
        {
            double dist = Math.Pow(p.X, 2) + Math.Pow(p.Z, 2);

            if (dist < 1 && p.Y >= ymax - MatMaths.eps)
                return new Vector(0, 1, 0);

            if (dist < 1 && p.Y <= ymin + MatMaths.eps)
                return new Vector(0, -1, 0);

            double y = Math.Sqrt(Math.Pow(p.X, 2) +  Math.Pow(p.Z, 2));

            return new Vector(p.X, (p.Y <0 ? y: -y), p.Z);
        }


        private bool CheckEnds(in Ray r, double t)
        {
            double x = r.origin.X + t * r.direction.X;
            double y = r.origin.Y + t * r.direction.Y;
            double z = r.origin.Z + t * r.direction.Z;

            return (Math.Pow(x, 2) + Math.Pow(z, 2)) <= Math.Abs(y);
        }
        private void IntersectEnds(in Ray r, ref List<Intersection> xs)
        {
            if (!closed || r.direction.Y <= MatMaths.eps)
                return;

            double t = (ymin - r.origin.Y) / r.direction.Y;
            if (CheckEnds(r, t))
                xs.Add(new(this, t));

            t = (ymax - r.origin.Y) / r.direction.Y;
            if (CheckEnds(r, t))
                xs.Add(new(this, t));

        }
    }

}
