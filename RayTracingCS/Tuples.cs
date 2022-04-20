using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RayTracingCS
{

    public struct Color
    {
        public double r, g, b;

        #region color constants
        static public readonly Color red = new(255, 0, 0);
        static public readonly Color Black = new(0, 0, 0);
        static public readonly Color White = new(255, 255, 255);
        static public readonly Color Red = new(255, 0, 0);
        static public readonly Color Lime = new(0, 255, 0);
        static public readonly Color Blue = new(0, 0, 255);
        static public readonly Color Yellow = new(255, 255, 0);
        static public readonly Color Cyan = new(0, 255, 255);
        static public readonly Color Magenta = new(255, 0, 255);
        static public readonly Color Silver = new(192, 192, 192);
        static public readonly Color Gray = new(128, 128, 128);
        static public readonly Color Maroon = new(128, 0, 0);
        static public readonly Color Olive = new(128, 128, 0);
        static public readonly Color Green = new(0, 128, 0);
        static public readonly Color Purple = new(128, 0, 128);
        static public readonly Color Teal = new(0, 128, 128);
        static public readonly Color Navy = new(0, 0, 128);
        #endregion

        public Color(double r = 0.0d, double g = 0.0d, double b = 0.0d)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public override string ToString()
        {
            return r + " " + g + " " + b;
        }
        public override bool Equals(Object obj)
        {
            if (obj is Color c)
                return this.r == c.r && this.g == c.g && this.b == c.b;

            return false;
        }


        #region operator overloads
        public static Color operator +(in Color a, in Color b) {
            return new(a.r + b.r, a.g + b.g, a.b + b.b);
        }
        public static Color operator -(in Color a, in Color b) {
            return new(a.r - b.r, a.g - b.g, a.b - b.b);
        }

        public static Color operator *(in Color a, double scalar) {
            return new(a.r * scalar, a.g * scalar, a.b * scalar);
        }
        public static Color operator *(double scalar, in Color a) {
            return a * scalar;
        }
        public static Color operator *(in Color a, in Color b) {
            return new(a.r * b.r, a.g * b.g, a.b * b.b);
        }

        #endregion
    }


    public struct Point
    {
        private double[] val;

        public Point(double x, double y, double z, double w = 1.0d)
        {
            val = new double[4];
            val[0] = x;
            val[1] = y;
            val[2] = z;
            val[3] = w;
        }
        public Point(int zero)
        {
            val = new double[4];
            val[0] = 0;
            val[1] = 0;
            val[2] = 0;
            val[3] = 1;
        }


        #region operator overloads
        public override string ToString()
        {
            return val[0] + " " + val[1] + " " + val[2] + " " + val[3];
        }
        public double this[int index]
        {
            get => val[index];
            set => val[index] = value;
        }
        public double X
        {
            get => val[0];
            set => val[0] = value;
        }
        public double Y
        {
            get => val[1];
            set => val[1] = value;
        }
        public double Z
        {
            get => val[2];
            set => val[2] = value;
        }
        public double W
        {
            get => val[3];
            set => val[3] = value;
        }
        public static Point operator +(in Point a, in Vector b)
        {
            return new Point(a[0] + b[0], a[1] + b[1], a[2] + b[2], a[3] + b[3]);
        }
        public static Point operator +(in Vector a, in Point b)
        {
            return b + a;
        }
        public static Vector operator -(in Point a, in Point b)
        {
            return new Vector(a[0] - b[0], a[1] - b[1], a[2] - b[2], a[3] - b[3]);
        }
        public static Point operator -(in Point a, in Vector b)
        {
            return new Point(a[0] - b[0], a[1] - b[1], a[2] - b[2], a[3] - b[3]);
        }
        public static Point operator -(in Point a)
        {
            return new Point(-a[0], -a[1], -a[2], -a[3]);
        }
        public static bool operator ==(in Point a, in Point b)
        {
            for (int i = 0; i < 4; i++)
                if (Math.Abs(a[i] - b[i]) > Mat.eps)
                    return false;
            return true;
        }
        public static bool operator !=(in Point a, in Point b)
        {
            for (int i = 0; i < 4; i++)
                if (Math.Abs(a[i] - b[i]) > Mat.eps)
                    return true;
            return false;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;

            Point v = (Point)obj;
            return this == v;
        }
        #endregion


    }

    public struct Vector
    {
        private double[] val;
        public Vector(double x = 0.0d, double y = 0.0d, double z = 0.0d, double w = 0.0d)
        {
            val = new double[4];
            val[0] = x;
            val[1] = y;
            val[2] = z;
            val[3] = w;
        }
        public Vector(int zero)
        {
            val = new double[4];
            val[0] = 0;
            val[1] = 0;
            val[2] = 0;
            val[3] = 0;
        }

        #region operator overloads
        public override string ToString()
        {
            return val[0] + " " + val[1] + " " + val[2] + " " + val[3];
        }
        public double this[int index]
        {
            get => val[index];
            set => val[index] = value;
        }
        public double X
        {
            get => val[0];
            set => val[0] = value;
        }
        public double Y
        {
            get => val[1];
            set => val[1] = value;
        }
        public double Z
        {
            get => val[2];
            set => val[2] = value;
        }
        public double W
        {
            get => val[3];
            set => val[3] = value;
        }


        public static Vector operator +(in Vector a, in Vector b)
        {
            return new Vector(a[0] + b[0], a[1] + b[1], a[2] + b[2], a[3] + b[3]);
        }
        public static Vector operator -(in Vector a, in Vector b)
        {
            return new Vector(a[0] - b[0], a[1] - b[1], a[2] - b[2], a[3] - b[3]);
        }
        public static Vector operator -(in Vector a)
        {
            return new Vector(-a[0], -a[1], -a[2], a[3]==0? a[3]: -a[3]);
        }
        public static Vector operator *(in Vector a, double scalar)
        {
            return new Vector(a[0] * scalar, a[1] * scalar, a[2] * scalar, a[3] * scalar);
        }
        public static Vector operator *(double scalar, in Vector a)
        {
            return a * scalar;
        }
        public static Vector operator /(in Vector a, double scalar)
        {
            scalar = 1 / scalar;
            return new Vector(a[0] * scalar, a[1] * scalar, a[2] * scalar, a[3] * scalar);
        }
        public static Vector operator /(double scalar, in Vector a)
        {
            return a / scalar;
        }
        public static bool operator ==(in Vector a, in Vector b)
        {
            for (int i = 0; i < 4; i++)
                if (Math.Abs(a[i] - b[i]) > Mat.eps)
                    return false;
            return true;
        }
        public static bool operator !=(in Vector a, in Vector b)
        {
            for (int i = 0; i < 4; i++)
                if (Math.Abs(a[i] - b[i]) > Mat.eps)
                    return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector))
                return false;

            Vector v = (Vector)obj;
            return this == v;
        }
        #endregion

        public double Magnitude()
        {
            return Math.Sqrt(val[0] * val[0] + val[1] * val[1] + val[2] * val[2] + val[3] * val[3]);
        }
        public Vector Normalized()
        {
            return this / Magnitude();
        }
        public void Normalize()
        {
            double mag = Magnitude();
            val[0] /= mag;
            val[1] /= mag;
            val[2] /= mag;
            val[3] /= mag;
        }
        public double Dot(in Vector b)
        {
            return  val[0] * b[0] +
                    val[1] * b[1] +
                    val[2] * b[2] +
                    val[3] * b[3];
        }
        public Vector Cross(in Vector b)
        {
            return new Vector(val[1] * b[2] - val[2] * b[1],
                              val[2] * b[0] - val[0] * b[2],
                              val[0] * b[1] - val[1] * b[0]);
        }


        public static Vector Reflect(in Vector v, in Vector norm)
        {
            return v - norm * 2 * MatMaths.Dot(v, norm);
        }

    }
}
