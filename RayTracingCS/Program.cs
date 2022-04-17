using System;

namespace RayTracingCS
{
    public struct Point
    {
        double x, y, z, w;

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1.0d;
        }
    }
    public struct Vector
    {
        double x, y, z, w;
        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0.0d;
        }
    }












    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
