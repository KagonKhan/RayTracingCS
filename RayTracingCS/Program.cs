using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;



namespace RayTracingCS
{
    class Program
    {

        public class Canvas
        {
            public readonly int width, height;
            Color[,] canvas;
            public Canvas(int width, int height)
            {
                this.width = width;
                this.height = height;

                canvas = new Color[width, height];

                for (int row = 0; row < height; row++)
                    for (int col = 0; col < width; col++)
                        canvas[row, col] = new Color();

            }

            public void Flush(Color color)
            {
                for (int row = 0; row < height; row++)
                    for (int col = 0; col < width; col++)
                        canvas[row, col] = color;
            }

            public void WritePixel(int x, int y, Color color)
            {
#if DEBUG
                Console.WriteLine($"Writing to ({x},{y})");
#endif

                if (x >= width || x < 0 || y >= height || y < 0)
                    return;

                canvas[y, x] = color;
            }

            public void ToPPM()
            {
                var sb = new System.Text.StringBuilder();
                sb.Append($"P3\n{width} {height}\n255\n");

                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        sb.Append(canvas[row, col].ToString() + " ");
                    }
                    sb.Append('\n');
                }


                System.IO.File.WriteAllText("canvas.ppm", sb.ToString());
            }
        }

        public readonly struct Color
        {
            public readonly double r, g, b;

            #region constant colors
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

            public Color(double r, double g, double b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly string ToString()
            {
                return r + " " + g + " " + b + " ";
            }

            #region operator overloads
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator +(Color a, Color b)
            {
                return new(a.r + b.r, a.g + b.g, a.b + b.b);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator -(Color a, Color b)
            {
                return new(a.r - b.r, a.g - b.g, a.b - b.b);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator *(Color a, double scalar)
            {
                return new(a.r * scalar, a.g * scalar, a.b * scalar);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator *(double scalar, Color a)
            {
                return a * scalar;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator *(Color a, Color b)
            {
                return new(a.r * b.r, a.g * b.g, a.b * b.b);
            }

            #endregion
        }

        public ref struct Mat
        {
            public Mat2 Mat2;
            public Mat3 Mat3;
            public Mat4 Mat4;

            public void init()
            {
                Mat2 = new Mat2(1, 0,
                                0, 1);

                Mat3 = new Mat3(1, 0, 0,
                                0, 1, 0,
                                0, 0, 1);

                Mat4 = new Mat4(1, 0, 0, 0,
                                0, 1, 0, 0,
                                0, 0, 1, 0,
                                0, 0, 0, 1);
            }

            public static Mat4 Translation(double x, double y, double z)
            {
                return new Mat4(1, 0, 0, x,
                                0, 1, 0, y,
                                0, 0, 1, z,
                                0, 0, 0, 1);
            }
            public Mat4 Translate(in Mat4 a, double x, double y, double z)
            {
                return Maths.Mult(a, new Mat4(1, 0, 0, x,
                                       0, 1, 0, y,
                                       0, 0, 1, z,
                                       0, 0, 0, 1));
            }
            public static Mat4 Scaling(double x, double y, double z)
            {
                return new Mat4(x, 0, 0, 0,
                                0, y, 0, 0,
                                0, 0, z, 0,
                                0, 0, 0, 1);
            }
            public Mat4 Scale(in Mat4 a, double x, double y, double z)
            {
                return Maths.Mult(a, new Mat4(x, 0, 0, 0,
                                       0, y, 0, 0,
                                       0, 0, z, 0,
                                       0, 0, 0, 1));
            }

            public static Mat4 Shearing(double Xy = 0, double Xz = 0, double Yx = 0, double Yz = 0, double Zx = 0, double Zy = 0)
            {
                return new Mat4(1, Xy, Xz, 0,
                                Yx, 1, Yz, 0,
                                Zx, Zy, 1, 0,
                                0, 0, 0, 1);
            }
            public Mat4 Shear(in Mat4 a, double Xy = 0, double Xz = 0, double Yx = 0, double Yz = 0, double Zx = 0, double Zy = 0)
            {
                return Maths.Mult(a, new Mat4(1, Xy, Xz, 0,
                                       Yx, 1, Yz, 0,
                                       Zx, Zy, 1, 0,
                                       0, 0, 0, 1));
            }

            public static Mat4 RotationX(double r)
            {
                double c = Math.Cos(r), s = Math.Sin(r);

                return new Mat4(1, 0, 0, 0,
                                0, c, -s, 0,
                                0, s, c, 0,
                                0, 0, 0, 1);
            }
            public Mat4 RotateX(in Mat4 a, double r)
            {
                double c = Math.Cos(r), s = Math.Sin(r);

                return Maths.Mult(a, new Mat4(1, 0, 0, 0,
                                       0, c, -s, 0,
                                       0, s, c, 0,
                                       0, 0, 0, 1));
            }
            public static Mat4 RotationY(double r)
            {
                double c = Math.Cos(r), s = Math.Sin(r);

                return new Mat4(c, 0, s, 0,
                                 0, 1, 0, 0,
                                -s, 0, c, 0,
                                 0, 0, 0, 1);
            }
            public Mat4 RotateY(in Mat4 a, double r)
            {
                double c = Math.Cos(r), s = Math.Sin(r);

                return Maths.Mult(a, new Mat4(c, 0, s, 0,
                                        0, 1, 0, 0,
                                       -s, 0, c, 0,
                                        0, 0, 0, 1));
            }
            public static Mat4 RotationZ(double r)
            {
                double c = Math.Cos(r), s = Math.Sin(r);

                return new Mat4(c, -s, 0, 0,
                                s, c, 0, 0,
                                0, 0, 0, 0,
                                0, 0, 0, 1);
            }
            public Mat4 RotateZ(in Mat4 a, double r)
            {
                double c = Math.Cos(r), s = Math.Sin(r);

                return Maths.Mult(a, new Mat4(c, -s, 0, 0,
                                       s, c, 0, 0,
                                       0, 0, 0, 0,
                                       0, 0, 0, 1));
            }

        }
        public ref struct Vector
        {
            public Span<double> mat;

            public Vector(double x, double y, double z, double w = 0) : this()
            {
                mat = new Span<double>(new double[sizeof(double) * 4]);

                mat[0] = x;
                mat[1] = y;
                mat[2] = z;
                mat[3] = w;
            }
            public Vector(bool zero)
            {
                mat = new Span<double>(new double[sizeof(double) * 4]);
            }
            public double this[int index]
            {
                get => mat[index];
                set => mat[index] = value;
            }
        }
        public ref struct Point
        {
            public Span<double> mat;

            public Point(double x, double y, double z, double w = 1) : this()
            {
                mat = new Span<double>(new double[sizeof(double) * 4]);

                mat[0] = x;
                mat[1] = y;
                mat[2] = z;
                mat[3] = w;
            }
            public Point(bool zero)
            {
                mat = new Span<double>(new double[sizeof(double) * 4]);
                mat[3] = 1;
            }
            public double this[int index]
            {
                get => mat[index];
                set => mat[index] = value;
            }
        }
        public ref struct Ray
        {
            public Point origin;
            public Vector direction;

            public Ray(Point o, Vector d)
            {
                origin = o;
                direction = d;
            }
        }

        public ref struct Mat2
        {
            public Span<double> mat;

            public Mat2(double a, double b, double c, double d) : this()
            {
                mat = new Span<double>(new double[sizeof(double) * 4]);

                mat[0] = a;
                mat[1] = b;
                mat[2] = c;
                mat[3] = d;
            }
            public Mat2(bool zero)
            {
                mat = new Span<double>(new double[sizeof(double) * 4]);
            }
            public double this[int row, int col]
            {
                get => mat[row * 2 + col];
                set => mat[row * 2 + col] = value;
            }
        }
        public ref struct Mat3
        {
            public Span<double> mat;

            public Mat3(double a, double b, double c,
                        double d, double e, double f,
                        double g, double h, double i) : this()
            {
                mat = new Span<double>(new double[sizeof(double) * 9]);

                mat[0] = a;
                mat[1] = b;
                mat[2] = c;

                mat[3] = d;
                mat[4] = e;
                mat[5] = f;

                mat[6] = g;
                mat[7] = h;
                mat[8] = i;
            }

            public Mat3(bool zero)
            {
                mat = new Span<double>(new double[sizeof(double) * 9]);
            }
            public double this[int row, int col]
            {
                get => mat[row * 3 + col];
                set => mat[row * 3 + col] = value;
            }




        }
        public ref struct Mat4
        {
            public Span<double> mat;

            public Mat4(double a, double b, double c, double d,
                        double e, double f, double g, double h,
                        double i, double j, double k, double l,
                        double m, double n, double o, double p) : this()
            {
                mat = new Span<double>(new double[sizeof(double) * 9]);

                mat[0] = a;
                mat[1] = b;
                mat[2] = c;
                mat[3] = d;

                mat[4] = e;
                mat[5] = f;
                mat[6] = g;
                mat[7] = h;

                mat[8] = i;
                mat[9] = j;
                mat[10] = k;
                mat[11] = l;

                mat[12] = m;
                mat[13] = n;
                mat[14] = o;
                mat[15] = p;
            }

            public Mat4(bool zero)
            {
                mat = new Span<double>(new double[sizeof(double) * 16]);
            }
            public double this[int row, int col]
            {
                get => mat[row * 4 + col];
                set => mat[row * 4 + col] = value;
            }
        }
        
        public struct Intersection
        {
            public enum Type { Sphere = 1, };

            Type obj;
            public double t;
            public Intersection(Type obj, double t)
            {
                this.obj = obj;
                this.t = t;
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


        public ref struct Sphere
        {
            public Point position;
            double radius;

            public Mat4 transformation;

            public Sphere(in Point pos, double r, in Mat4 tform)
            {
                radius = r;
                position = pos;
                transformation = tform;
            }


            public Intersection[] intersects(Ray ray)
            {

                ray = Maths.Transform(ray, Maths.Inverse(transformation));

                var sphereToRay = Maths.Sub(ray.origin, new Point(0, 0, 0));

                var a = Maths.Dot(ray.direction, ray.direction);
                var b = 2 * Maths.Dot(ray.direction, sphereToRay);
                var c = Maths.Dot(sphereToRay, sphereToRay) - 1;

                var disc = Math.Pow(b, 2) - 4 * a * c;

                if (disc < 0)
                    return Array.Empty<Intersection>();
                else
                {
                    var t1 = (-b - Math.Sqrt(disc)) / (2 * a);
                    var t2 = (-b + Math.Sqrt(disc)) / (2 * a);

                    var o1 = new Intersection(Intersection.Type.Sphere, t1);
                    var o2 = new Intersection(Intersection.Type.Sphere, t2);

                    return new Intersection[2] { o1, o2 };
                }
            }
        }

        public class Maths
        {

            #region Mat2 functions
            public static void Print(in Mat2 a)
            {
                for (int row = 0; row < 2; row++)
                {
                    for (int col = 0; col < 2; col++)
                    {
                        Console.Write(a[row, col] + " ");
                    }
                    Console.Write('\n');
                }
            }
            public static Mat2 Mult(in Mat2 a, in Mat2 b)
            {
                double v1 = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0];
                double v2 = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1];
                double v3 = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0];
                double v4 = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1];

                return new Mat2(v1, v2, v3, v4);
            }
            public static double Det(in Mat2 a)
            {
                return a[0, 0] * a[1, 1] - a[0, 1] * a[1, 0];
            }
            #endregion

            #region Mat3 functions

            public static void Print(in Mat3 a)
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        Console.Write(a[row, col] + " ");
                    }
                    Console.Write('\n');
                }
            }
            public static Mat3 Mult(in Mat3 a, in Mat3 b)
            {

                double v1 = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0] + a[0, 2] * b[2, 0];
                double v2 = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1] + a[0, 2] * b[2, 1];
                double v3 = a[0, 0] * b[0, 2] + a[0, 1] * b[1, 2] + a[0, 2] * b[2, 2];

                double v4 = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0] + a[1, 2] * b[2, 0];
                double v5 = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1] + a[1, 2] * b[2, 1];
                double v6 = a[1, 0] * b[0, 2] + a[1, 1] * b[1, 2] + a[1, 2] * b[2, 2];

                double v7 = a[2, 0] * b[0, 0] + a[2, 1] * b[1, 0] + a[2, 2] * b[2, 0];
                double v8 = a[2, 0] * b[0, 1] + a[2, 1] * b[1, 1] + a[2, 2] * b[2, 1];
                double v9 = a[2, 0] * b[0, 2] + a[2, 1] * b[1, 2] + a[2, 2] * b[2, 2];

                return new Mat3(v1, v2, v3, v4, v5, v6, v7, v8, v9);
            }
            public static double Det(in Mat3 a)
            {
                return a[0, 0] * Cofactor(a, 0, 0) + a[0, 1] * Cofactor(a, 0, 1) + a[0, 2] * Cofactor(a, 0, 2);
            }
            public static Mat2 Sub(in Mat3 a, int r, int c)
            {
                Mat2 retVal = new Mat2(true);

                for (int row = 0, rIndex = 0; row < 3; row++)
                {
                    if (row == r)
                        continue;

                    for (int col = 0, cIndex = 0; col < 3; col++)
                    {
                        if (col == c)
                            continue;

                        retVal[rIndex, cIndex] = a[row, col];

                        cIndex++;
                    }
                    rIndex++;
                }

                return retVal;
            }
            public static double Minor(in Mat3 a, int r, int c)
            {
                return Det(Sub(a, r, c));
            }
            public static double Cofactor(in Mat3 a, int r, int c)
            {
                return (r + c) % 2 == 0 ? Minor(a, r, c) : -Minor(a, r, c);
            }

            #endregion

            #region Mat4 functions

            public static void Print(in Mat4 a)
            {
                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        Console.Write(a[row, col] + " ");
                    }
                    Console.Write('\n');
                }
            }
            public static Mat4 Mult(in Mat4 a, in Mat4 b)
            {

                double v1 = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0] + a[0, 2] * b[2, 0] + a[0, 3] * b[3, 0];
                double v2 = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1] + a[0, 2] * b[2, 1] + a[0, 3] * b[3, 1];
                double v3 = a[0, 0] * b[0, 2] + a[0, 1] * b[1, 2] + a[0, 2] * b[2, 2] + a[0, 3] * b[3, 2];
                double v4 = a[0, 0] * b[0, 3] + a[0, 1] * b[1, 3] + a[0, 2] * b[2, 3] + a[0, 3] * b[3, 3];

                double v5 = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0] + a[1, 2] * b[2, 0] + a[1, 3] * b[3, 0];
                double v6 = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1] + a[1, 2] * b[2, 1] + a[1, 3] * b[3, 1];
                double v7 = a[1, 0] * b[0, 2] + a[1, 1] * b[1, 2] + a[1, 2] * b[2, 2] + a[1, 3] * b[3, 2];
                double v8 = a[1, 0] * b[0, 3] + a[1, 1] * b[1, 3] + a[1, 2] * b[2, 3] + a[1, 3] * b[3, 3];

                double v9 = a[2, 0] * b[0, 0] + a[2, 1] * b[1, 0] + a[2, 2] * b[2, 0] + a[2, 3] * b[3, 0];
                double v10 = a[2, 0] * b[0, 1] + a[2, 1] * b[1, 1] + a[2, 2] * b[2, 1] + a[2, 3] * b[3, 1];
                double v11 = a[2, 0] * b[0, 2] + a[2, 1] * b[1, 2] + a[2, 2] * b[2, 2] + a[2, 3] * b[3, 2];
                double v12 = a[2, 0] * b[0, 3] + a[2, 1] * b[1, 3] + a[2, 2] * b[2, 3] + a[2, 3] * b[3, 3];

                double v13 = a[3, 0] * b[0, 0] + a[3, 1] * b[1, 0] + a[3, 2] * b[2, 0] + a[3, 3] * b[3, 0];
                double v14 = a[3, 0] * b[0, 1] + a[3, 1] * b[1, 1] + a[3, 2] * b[2, 1] + a[3, 3] * b[3, 1];
                double v15 = a[3, 0] * b[0, 2] + a[3, 1] * b[1, 2] + a[3, 2] * b[2, 2] + a[3, 3] * b[3, 2];
                double v16 = a[3, 0] * b[0, 3] + a[3, 1] * b[1, 3] + a[3, 2] * b[2, 3] + a[3, 3] * b[3, 3];


                return new Mat4(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16);
            }
            public static Point Mult(Mat4 a, Point b)
            {
                double v1 = a[0, 0] * b[0] + a[0, 1] * b[1] + a[0, 2] * b[2] + a[0, 3] * b[3];
                double v2 = a[1, 0] * b[0] + a[1, 1] * b[1] + a[1, 2] * b[2] + a[1, 3] * b[3];
                double v3 = a[2, 0] * b[0] + a[2, 1] * b[1] + a[2, 2] * b[2] + a[2, 3] * b[3];
                double v4 = a[3, 0] * b[0] + a[3, 1] * b[1] + a[3, 2] * b[2] + a[3, 3] * b[3];

                return new Point(v1, v2, v3, v4);
            }
            public static Vector Mult(Mat4 a, Vector b)
            {
                double v1 = a[0, 0] * b[0] + a[0, 1] * b[1] + a[0, 2] * b[2] + a[0, 3] * b[3];
                double v2 = a[1, 0] * b[0] + a[1, 1] * b[1] + a[1, 2] * b[2] + a[1, 3] * b[3];
                double v3 = a[2, 0] * b[0] + a[2, 1] * b[1] + a[2, 2] * b[2] + a[2, 3] * b[3];
                double v4 = a[3, 0] * b[0] + a[3, 1] * b[1] + a[3, 2] * b[2] + a[3, 3] * b[3];

                return new Vector(v1, v2, v3, v4);
            }

            public static Mat4 Transpose(in Mat4 a)
            {
                Mat4 retVal = new Mat4(true);

                for (int row = 0; row < 4; row++)
                    for (int col = 0; col < 4; col++)
                        retVal[col, row] = a[row, col];

                return retVal;
            }
            public static double Det(in Mat4 a)
            {
                return a[0, 0] * Cofactor(a, 0, 0) + a[0, 1] * Cofactor(a, 0, 1) + a[0, 2] * Cofactor(a, 0, 2) + a[0, 3] * Cofactor(a, 0, 3);
            }
            public static Mat3 Sub(in Mat4 a, int r, int c)
            {
                Mat3 retVal = new Mat3(true);

                for (int row = 0, rIndex = 0; row < 4; row++)
                {
                    if (row == r)
                        continue;

                    for (int col = 0, cIndex = 0; col < 4; col++)
                    {
                        if (col == c)
                            continue;

                        retVal[rIndex, cIndex] = a[row, col];

                        cIndex++;
                    }
                    rIndex++;
                }

                return retVal;
            }
            public static double Minor(in Mat4 a, int r, int c)
            {
                return Det(Sub(a, r, c));
            }
            public static double Cofactor(in Mat4 a, int r, int c)
            {
                return (r + c) % 2 == 0 ? Minor(a, r, c) : -Minor(a, r, c);
            }
            public static Mat4 Inverse(in Mat4 a)
            {
                double det = Det(a);

                if (det == 0)
                    throw new ArithmeticException("Matrix non inversible");

                Mat4 retVal = new Mat4(true);

                for (int row = 0; row < 4; row++)
                    for (int col = 0; col < 4; col++)
                    {
                        retVal[col, row] = Cofactor(a, row, col) / det;
                    }

                return retVal;

            }

            #endregion

            #region Ray functions


            public static Ray Transform(in Ray r, in Mat4 tform)
            {
                return new Ray(Maths.Mult(tform, r.origin), Maths.Mult(tform, r.direction));
            }


            #endregion

            #region Vector and Point functions
            public static Point Add(in Vector a, in Point b)
            {
                return new Point(a[0] + b[0], a[1] + b[1], a[2] + b[2], a[3] + b[3]);
            }
            public static Vector Add(in Vector a, in Vector b)
            {
                return new Vector(a[0] + b[0], a[1] + b[1], a[2] + b[2], a[3] + b[3]);
            }
            public static Vector Sub(in Vector a, in Vector b)
            {
                return new Vector(a[0] - b[0], a[1] - b[1], a[2] - b[2], a[3] - b[3]);
            }
            public static Vector Sub(in Point a, in Point b)
            {
                return new Vector(a[0] - b[0], a[1] - b[1], a[2] - b[2], a[3] - b[3]);
            }
            public static Point Sub(in Point a, in Vector b)
            {
                return new Point(a[0] - b[0], a[1] - b[1], a[2] - b[2], a[3] - b[3]);
            }
            public static Point Sub(in Point a)
            {
                return new Point(-a[0], -a[1], -a[2], -a[3]);
            }
            public static Vector Sub(in Vector a)
            {
                return new Vector(-a[0], -a[1], -a[2], -a[3]);
            }

            public static Vector Mult(in Vector a, double scalar)
            {
                return new Vector(a[0] * scalar, a[1] * scalar, a[2] * scalar, a[3] * scalar);
            }
            public static Vector Mult(double scalar, in Vector a)
            {
                return Mult(a, scalar);
            }
            public static Vector Div(in Vector a, double scalar)
            {
                return new Vector(a[0] / scalar, a[1] / scalar, a[2] / scalar, a[3] / scalar);
            }
            public static Vector Div(double scalar, in Vector a)
            {
                return Mult(a, scalar);
            }
            #endregion

            public static double Mag(in Vector a)
            {
                double sum = Math.Pow(a[0], 2) + Math.Pow(a[1], 2) + Math.Pow(a[2], 2) + Math.Pow(a[3], 2);
                return Math.Sqrt(sum);
            }
            public static Vector Norm(in Vector a)
            {
                double mag = 1d / Mag(a);
                return Mult(a, mag);
            }
            public static void Norm(ref Vector a, bool overwrite)
            {
                double mag = 1d / Mag(a);
                a[0] /= mag;
                a[1] /= mag;
                a[2] /= mag;
                a[3] /= mag;
            }

            public static double Dot(in Vector a, in Vector b)
            {
                return a[0] * b[0] + a[1] * b[1] + a[2] * b[2] + a[3] * b[3];
            }
            public static Vector Cross(in Vector a, in Vector b)
            {
                return new Vector(a[1] * b[2] - a[2] * b[1],
                                  a[2] * b[0] - a[0] * b[2],
                                  a[1] * b[1] - a[1] * b[0]);
            }
        }

        [Benchmark]
        static void Main(string[] args)
        {

            Mat I = new Mat();
            I.init();

            var canvas_pixels = 10000;
            Canvas canvas = new(canvas_pixels, canvas_pixels);
            canvas.Flush(Color.Black);


            Point ray_origin = new Point(0, 0, -5);
            double wall_z = 10.0d;
            double wall_size = 7.0d;
            double pixel_size = wall_size / canvas_pixels;
            double half = wall_size / 2;

            var s = new Sphere(new Point(0, 0, 0), 1d, I.Mat4);
            //s.Transformation = Mat4.Scaling(250, 250, 1);


            var empty_xs = new Intersection();
            Vector v = Maths.Sub(new Point(0, 0, 0), ray_origin);
            Maths.Norm(ref(v), true);

            var r = new Ray(ray_origin, v);


            var watch = System.Diagnostics.Stopwatch.StartNew();

            var stack = new System.Diagnostics.StackTrace();

            for (int x = 0; x < canvas_pixels; x++)
            {
                for (int y = 0; y < canvas_pixels; y++)
                {
                    var world_y = half - pixel_size * y;
                    var world_x = -half + pixel_size * x;
                    var pos = new Point(world_x, world_y, wall_z);

                    r.direction = Maths.Norm(Maths.Sub(pos, ray_origin));


                    var xs = s.intersects(r);
                    if (!Intersection.Hit(xs).Equals(empty_xs))
                        canvas.WritePixel(x, y, Color.Red);


                    if ((x * canvas_pixels + y) % 5000 == 0)
                        Console.WriteLine($"{(x * canvas_pixels + y)} out of {canvas_pixels * canvas_pixels}");

                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);

            canvas.ToPPM();

        }
    }
}


/*                      Benchmarking v1
 *                      
 * Canvas size     10000    1000    500     250     100     50
 * Time[ms]        35min    12042   4100    900     160     50
 * 
 * 
 * 
 *                      Benchmarking v2
 * Canvas size     10000    1000    500     250     100     50
 * Time[ms]         7min    5299   1700     430      86     40                        
 *                      Benchmarking v3
 * Canvas size
 * Time
 *                      Benchmarking v4
 * Canvas size
 * Time
 *                      Benchmarking v5
 * Canvas size
 * Time
 *                      Benchmarking v6
 * Canvas size
 * Time
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * */