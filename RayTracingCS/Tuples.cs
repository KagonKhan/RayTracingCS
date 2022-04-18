using System;
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

        public Ray(Point o, Vector d)
        {
            origin = o;
            direction = d;
        }

        public Point position(double t)
        {
            return direction * t + origin;
        }

        public Ray Transform(Mat4 transformation)
        {
            return new Ray(transformation * origin, transformation * direction);
        }


    }

    public struct Intersection<T>
    {
        public T obj;
        public double t;
        public Intersection(T obj, double t)
        {
            this.obj = obj;
            this.t = t;
        }


        public static Intersection<T> Hit(params Intersection<T>[] xs)
        {

            Array.Sort(xs, delegate(Intersection<T> a, Intersection<T> b) { return a.t > b.t ? 1 : a.t == b.t ? 0 : -1; });

            for (int i = 0; i < xs.Length; i++)
                if (xs[i].t > 0d)
                    return xs[i];

            return new Intersection<T>();
        }
    }


    public struct Sphere
    {
        public Point position;
        double radius;

        public Mat4 Transformation;
            
        public Sphere (Point pos, double r)
        {
            radius = r;
            position = pos;
            Transformation = Mat4.I;
        }


        public void SetTransform(Mat4 transformation)
        {
             Transformation = transformation;
        }

        public Intersection<Sphere>[] intersects (Ray ray)
        {

            ray = ray.Transform(Transformation.Inverse());

            var sphereToRay = ray.origin - new Point(0,0,0);

            var a = ray.direction.Dot(ray.direction);
            var b = 2 * ray.direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;

            var disc = Math.Pow(b, 2) - 4 * a * c;

            if (disc < 0)
                return Array.Empty<Intersection<Sphere>>();
            else {
                var t1 = (-b - Math.Sqrt(disc)) / (2 * a);
                var t2 = (-b + Math.Sqrt(disc)) / (2 * a);

                var o1 = new Intersection<Sphere>(this, t1);
                var o2 = new Intersection<Sphere>(this, t2);

                return new Intersection<Sphere>[2] { o1, o2 };
            }
        }
    }

    public abstract class Tuple
    {
        public double x, y, z, w;

        protected Tuple(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(x + " ");
            sb.Append(y + " ");
            sb.Append(z + " ");
            sb.Append(w);

            return sb.ToString();
        }


        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default: return x;
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default: x = value; break;
                }
            }
        }

    }

    public class Color : Tuple
    {
        private double x, y, z, w;
        public double r {
            get => base.x;
            set => base.x = value;
        }
        public double g {
            get => base.y;
            set => base.y = value;
        }
        public double b {
            get => base.z;
            set => base.z = value;
        }

        public Color(double r = 0.0d, double g = 0.0d, double b = 0.0d) : base(r, g, b, 1.0d) { }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(r + " ");
            sb.Append(g + " ");
            sb.Append(b);

            return sb.ToString();
        }
        public override bool Equals(Object obj)
        {
            if (obj is Color)
            {
                var that = obj as Color;
                return this.r == that.r && this.g == that.g && this.b == that.b;
            }

            return false;
        }
        #region constant colors
        static public readonly Color red        = new(255, 0, 0);
        static public readonly Color Black      = new(0,0,0);
        static public readonly Color White      = new(255,255,255);
        static public readonly Color Red        = new(255,0,0);
        static public readonly Color Lime       = new(0,255,0);
        static public readonly Color Blue       = new(0,0,255);
        static public readonly Color Yellow     = new(255,255,0);
        static public readonly Color Cyan       = new(0,255,255);
        static public readonly Color Magenta    = new(255,0,255);
        static public readonly Color Silver     = new(192,192,192);
        static public readonly Color Gray       = new(128,128,128);
        static public readonly Color Maroon     = new(128,0,0);
        static public readonly Color Olive      = new(128,128,0);
        static public readonly Color Green      = new(0,128,0);
        static public readonly Color Purple     = new(128, 0, 128);
        static public readonly Color Teal       = new(0,128,128);
        static public readonly Color Navy       = new(0, 0, 128);
        #endregion

        #region operator overloads
        public static Color operator +(Color a, Color b)
        {
            Color retVal = new();
            retVal.r = a.r + b.r;
            retVal.g = a.g + b.g;
            retVal.b = a.b + b.b;
            return retVal;
        }
        public static Color operator -(Color a, Color b)
        {
            Color retVal = new();
            retVal.r = a.r - b.r;
            retVal.g = a.g - b.g;
            retVal.b = a.b - b.b;
            return retVal;
        }
        public static Color operator *(Color a, double scalar)
        {
            Color retVal = new();
            retVal.r = a.r * scalar;
            retVal.g = a.g * scalar;
            retVal.b = a.b * scalar;
            return retVal;
        }
        public static Color operator *(double scalar, Color a)
        {
            return a * scalar;
        }
        public static Color operator *(Color a, Color b)
        {
            Color retVal = new();
            retVal.r = a.r * b.r;
            retVal.g = a.g * b.g;
            retVal.b = a.b * b.b;
            return retVal;
        }

        #endregion
    }


    public class Point : Tuple
    {
        public Point(double x = 0.0d, double y = 0.0d, double z = 0.0d) : base(x, y, z, 1.0d) { }

    #region operator overloads
        public static Point operator +(Point a, Vector b)
        {
            Point retVal = new();
            retVal.x = a.x + b.x;
            retVal.y = a.y + b.y;
            retVal.z = a.z + b.z;
            retVal.w = a.w + b.w;
            return retVal;
        }
        public static Point operator +(Vector a, Point b)
        {
            Point retVal = new();
            retVal.x = a.x + b.x;
            retVal.y = a.y + b.y;
            retVal.z = a.z + b.z;
            retVal.w = a.w + b.w;
            return retVal;
        }
        public static Vector operator -(Point a, Point b)
        {
            Vector retVal = new();
            retVal.x = a.x - b.x;
            retVal.y = a.y - b.y;
            retVal.z = a.z - b.z;
            retVal.w = a.w - b.w;
            return retVal;
        }
        public static Point operator -(Point a, Vector b)
        {
            Point retVal = new();
            retVal.x = a.x - b.x;
            retVal.y = a.y - b.y;
            retVal.z = a.z - b.z;
            retVal.w = a.w - b.w;
            return retVal;
        }
        public static Point operator -(Point a)
        {
            Point retVal = new();
            retVal.x = -a.x;
            retVal.y = -a.y;
            retVal.z = -a.z;
            retVal.w = a.w == 0.0d ? a.w : -a.w;
            return retVal;
        }
        public static bool operator ==(Point a, Point b)
        {
            for (int i = 0; i < 4; i++)
                if (Math.Abs(a[i] - b[i]) > Mat.eps)
                    return false;
            return true;
        }
        public static bool operator !=(Point a, Point b)
        {
            for (int i = 0; i < 4; i++)
                if (Math.Abs(a[i] - b[i]) > Mat.eps)
                    return true;
            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj is Point)
            {
                var that = obj as Point;
                return this.x == that.x
                    && this.y == that.y
                    && this.z == that.z
                    && this.w == that.w;
            }

            return false;
        }
        #endregion


    }

    public class Vector : Tuple
    {
        public Vector(double x = 0.0d, double y = 0.0d, double z = 0.0d) : base(x, y, z, 0.0d) { }

        #region operator overloads
 
        public static Vector operator +(Vector a, Vector b)
        {
            Vector retVal = new();
            retVal.x = a.x + b.x;
            retVal.y = a.y + b.y;
            retVal.z = a.z + b.z;
            retVal.w = a.w + b.w;
            return retVal;
        }
        public static Vector operator -(Vector a, Vector b)
        {
            Vector retVal = new();
            retVal.x = a.x - b.x;
            retVal.y = a.y - b.y;
            retVal.z = a.z - b.z;
            retVal.w = a.w - b.w;
            return retVal;
        }
        public static Vector operator -(Vector a)
        {
            Vector retVal = new();
            retVal.x = -a.x;
            retVal.y = -a.y;
            retVal.z = -a.z;
            retVal.w = a.w == 0.0d ? a.w : -a.w;

            return retVal;
        }
        public static Vector operator *(Vector a, double scalar)
        {
            Vector retVal = new();
            retVal.x = a.x * scalar;
            retVal.y = a.y * scalar;
            retVal.z = a.z * scalar;
            retVal.w = a.w * scalar;

            return retVal;
        }
        public static Vector operator *(double scalar, Vector a)
        {
            return a * scalar;
        }
        public static Vector operator /(Vector a, double scalar)
        {
            Vector retVal = new();
            retVal.x = a.x / scalar;
            retVal.y = a.y / scalar;
            retVal.z = a.z / scalar;
            retVal.w = a.w / scalar;

            return retVal;
        }
        public static Vector operator /(double scalar, Vector a)
        {
            return a / scalar;
        }
        public static bool operator ==(Vector a, Vector b)
        {
            for (int i = 0; i < 4; i++)
                if (Math.Abs(a[i] - b[i]) > Mat.eps)
                    return false;
            return true;
        }
        public static bool operator !=(Vector a, Vector b)
        {
            for (int i = 0; i < 4; i++)
                if (Math.Abs(a[i] - b[i]) > Mat.eps)
                    return true;
            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj is Vector)
            {
                var that = obj as Vector;
                return this.x == that.x
                    && this.y == that.y
                    && this.z == that.z
                    && this.w == that.w;
            }

            return false;
        }
        #endregion


        public double Magnitude()
        {
            double sum = Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z, 2) + Math.Pow(this.w, 2);
            return Math.Sqrt(sum);
        }
        public Vector Normalize()
        {
            Vector retVal = new();
            double mag = Magnitude();

            retVal.x = x / mag;
            retVal.y = y / mag;
            retVal.z = z / mag;
            retVal.w = w / mag;

            return retVal;
        }
        public double Dot(Vector b)
        {
            return  x * b.x +
                    y * b.y +
                    z * b.z +
                    w * b.w;
        }
        public Vector Cross(Vector b)
        {
            return new Vector(y * b.z - z * b.y,
                              z * b.x - x * b.z,
                              x * b.y - y * b.x);
        }

    }
}
