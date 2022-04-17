using System;

namespace RayTracingCS
{



    public struct Point
    {
        public double x, y, z, w;

        public Point(double x = 0.0d, double y = 0.0d, double z = 0.0d)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1.0d;
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
    public struct Vector
    {
        public double x, y, z, w;
        public Vector(double x = 0.0d, double y = 0.0d, double z = 0.0d)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0.0d;
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




    class Program
    {

        static void Main(string[] args)
        {
            
        }
    }
}
