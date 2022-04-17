using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{
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

    }



    public class Point : Tuple
    {
        public Point(double x = 0.0d, double y = 0.0d, double z = 0.0d) : base(x, y, z, 1.0d) { }
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
    }

    public class Vector : Tuple
    {
        public Vector(double x = 0.0d, double y = 0.0d, double z = 0.0d) : base(x, y, z, 0.0d) { }

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
    }
}
