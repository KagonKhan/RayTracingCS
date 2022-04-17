﻿using System;
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
