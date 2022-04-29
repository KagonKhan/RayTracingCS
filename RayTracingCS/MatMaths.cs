using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{

    public static class MatMaths
    {
        public static int SpaceshipOp(double? a, double? b) => a > b ? 1 : a == b ? 0 : -1;




        public static readonly double eps = 0.01;
        public readonly static Mat4 I = new Mat4(1.0d, 0.0d, 0.0d, 0.0d,
                                                 0.0d, 1.0d, 0.0d, 0.0d,
                                                 0.0d, 0.0d, 1.0d, 0.0d,
                                                 0.0d, 0.0d, 0.0d, 1.0d);


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
            Mat2 retVal = new Mat2();

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
            Mat4 retVal = new Mat4();

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
            Mat3 retVal = new Mat3();

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
        public static Mat4 Inverse(Mat4 a)
        {
            Mat4 retVal = new Mat4(I);


            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (j != i)
                    {
                        double ratio = a[j, i] / a[i, i];
                        for (int k = 0; k < 4; k++)
                        {
                            a[j, k] -= a[i, k] * ratio;
                            retVal[j, k] -= retVal[i, k] * ratio;
                        }
                    }

            for (int i = 0; i < 4; i++)
            {
                double temp = 1 / a[i, i];

                for (int j = 0; j < 4; j++)
                    retVal[i, j] = retVal[i, j] * temp;

            }

            return retVal;

        }

        public static Mat4 Translation(double x, double y, double z)
        {
            return new Mat4(1, 0, 0, x,
                            0, 1, 0, y,
                            0, 0, 1, z,
                            0, 0, 0, 1);
        }

        public static Mat4 Scaling(double x, double y, double z)
        {
            return new Mat4(x, 0, 0, 0,
                            0, y, 0, 0,
                            0, 0, z, 0,
                            0, 0, 0, 1);
        }


        public static Mat4 Shearing(double Xy = 0, double Xz = 0, double Yx = 0, double Yz = 0, double Zx = 0, double Zy = 0)
        {
            return new Mat4(1, Xy, Xz, 0,
                            Yx, 1, Yz, 0,
                            Zx, Zy, 1, 0,
                            0, 0, 0, 1);
        }

        public static Mat4 RotationX(double r)
        {
            double c = Math.Cos(r), s = Math.Sin(r);

            return new Mat4(1, 0, 0, 0,
                            0, c, -s, 0,
                            0, s, c, 0,
                            0, 0, 0, 1);
        }

        public static Mat4 RotationY(double r)
        {
            double c = Math.Cos(r), s = Math.Sin(r);

            return new Mat4(c, 0, s, 0,
                             0, 1, 0, 0,
                            -s, 0, c, 0,
                             0, 0, 0, 1);
        }

        public static Mat4 RotationZ(double r)
        {
            double c = Math.Cos(r), s = Math.Sin(r);

            return new Mat4(c, -s, 0, 0,
                            s, c, 0, 0,
                            0, 0, 0, 0,
                            0, 0, 0, 1);
        }

        public static Mat4 ViewTransform(in Point from, in Point to, in Vector up)
        {
            Vector forward = (to - from).Normalized();
            Vector left = forward.Cross(up.Normalized());
            Vector trueUp = left.Cross(forward);
            Vector nul = new Vector(0, 0, 0, 1);

            Mat4 orientation = new Mat4(left, trueUp, -forward, nul);

            return orientation * Translation(-from.X, -from.Y, -from.Z);
        }

        #endregion

        #region Ray functions


        public static Ray Transform(in Ray r, in Mat4 tform)
        {
            return new Ray(MatMaths.Mult(tform, r.origin), MatMaths.Mult(tform, r.direction));
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
            a[0] *= mag;
            a[1] *= mag;
            a[2] *= mag;
            a[3] *= mag;
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
        #endregion
    }
}

