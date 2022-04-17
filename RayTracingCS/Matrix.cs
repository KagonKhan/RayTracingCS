using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{
    public abstract class Mat
    {
        protected double[,] mat;
        public int dim;
        public double this[int row, int col]
        {
            get => mat[row, col];
            set => mat[row, col] = value;
        }

        protected Mat(int dim)
        {
            mat = new double[dim, dim];
            this.dim = dim;


            for (int col = 0; col < dim; col++)
                for (int row = 0; row < dim; row++)
                    mat[col, row] = 0;
        }
        protected Mat() { }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int row = 0; row < dim; row++)
            {
                for (int col = 0; col < dim; col++)
                {
                    sb.Append(mat[row, col] + " ");
                }
                sb.Append('\n');
            }

            return sb.ToString();
        }


        public static bool operator==(Mat a, Mat b)
        {
            for (int row = 0; row < a.dim; row++)
                for (int col = 0; col < a.dim; col++)
                    if (!double.Equals(a[row, col], b[row, col]))
                        return false;

            return true;
        }
        public static bool operator!=(Mat a, Mat b)
        {
            for (int row = 0; row < a.dim; row++)
                for (int col = 0; col < a.dim; col++)
                    if (!double.Equals(a[row, col], b[row, col]))
                        return true;

            return false;
        }

        

    }

    public class Mat2 : Mat
    {
        public readonly static Mat2 I = new Mat2(1.0d, 0.0d,
                                                 0.0d, 1.0d);

        public Mat2() : base(2) { }
        public Mat2(double a, double b,
                    double c, double d)
        {
            dim = 2;
            mat = new double[2, 2]
            {
                { a, b },
                { c, d }
            };
        }

        public static Mat2 operator *(Mat2 a, Mat2 b)
        {
            Mat2 retVal = new Mat2();

            for (int row = 0; row < 2; row++)
                for (int col = 0; col < 2; col++)
                    retVal[row, col] = a[row, 0] * b[0, col] +
                                       a[row, 1] * b[1, col];

            return retVal;
        }

        public double Determinant()
        {
            return mat[0, 0] * mat[1, 1] - mat[0, 1] * mat[1, 0];
        }
    }

    public class Mat3 : Mat
    {
        public readonly static Mat3 I = new Mat3(1.0d, 0.0d, 0.0d,
                                                 0.0d, 1.0d, 0.0d,
                                                 0.0d, 0.0d, 1.0d);

        public Mat3() : base(3) { }
        public Mat3(double a, double b, double c,
                    double d, double e, double f,
                    double g, double h, double i)
        {
            dim = 3;
            mat = new double[3, 3]
            {
                { a, b, c },
                { d, e, f },
                { g, h, i }
            };
        }

        public static Mat3 operator *(Mat3 a, Mat3 b)
        {
            Mat3 retVal = new Mat3();

            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    retVal[row, col] = a[row, 0] * b[0, col] +
                                       a[row, 1] * b[1, col] +
                                       a[row, 2] * b[2, col];

            return retVal;
        }

        public Mat2 Sub(int r, int c)
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

                    retVal[rIndex, cIndex] = mat[row, col];

                    cIndex++;
                }
                rIndex++;
            }

            return retVal;
        }

        public double Minor(int r, int c)
        {
            return Sub(r, c).Determinant();
        }

        public double Cofactor(int r, int c)
        {
            if ((r + c) % 2 == 0)
                return Minor(r, c);
            else
                return -Minor(r, c);

        }

        public double Determinant()
        {
            return mat[0, 0] * Cofactor(0, 0) + mat[0, 1] * Cofactor(0, 1) + mat[0, 2] * Cofactor(0, 2);
        }
    }

    public class Mat4 : Mat
    {
        public readonly static Mat4 I = new Mat4(1.0d, 0.0d, 0.0d, 0.0d,
                                                 0.0d, 1.0d, 0.0d, 0.0d,
                                                 0.0d, 0.0d, 1.0d, 0.0d,
                                                 0.0d, 0.0d, 0.0d, 1.0d);

        public Mat4() : base(4) { }
        public Mat4(double a, double b, double c, double d,
                    double e, double f, double g, double h,
                    double i, double j, double k, double l,
                    double m, double n, double o, double p)
        {
            dim = 4;
            mat = new double[4, 4]
            {
                { a, b, c, d },
                { e, f, g, h },
                { i, j, k, l },
                { m, n, o, p }
            };
        }


        public static Mat4 operator *(Mat4 a, Mat4 b)
        {
            Mat4 retVal = new Mat4();

            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    retVal[row, col] = a[row, 0] * b[0, col] +
                                       a[row, 1] * b[1, col] +
                                       a[row, 2] * b[2, col] +
                                       a[row, 3] * b[3, col];

            return retVal;
        }

        public static Vector operator *(Mat4 a, Vector b)
        {
            Vector retVal = new Vector();

            for (int row = 0; row < 4; row++)
                retVal[row] = a[row, 0] * b[0] +
                              a[row, 1] * b[1] +
                              a[row, 2] * b[2] +
                              a[row, 3] * b[3];

            return retVal;
        }
        public static Point operator *(Mat4 a, Point b)
        {
            Point retVal = new Point();

            for (int row = 0; row < 4; row++)
                retVal[row] = a[row, 0] * b[0] +
                              a[row, 1] * b[1] +
                              a[row, 2] * b[2] +
                              a[row, 3] * b[3];

            return retVal;
        }

        public Mat4 Transpose()
        {
            Mat4 retVal = new();
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    retVal[col, row] = mat[row, col];

            return retVal;
        }

        public Mat3 Sub(int r, int c)
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
                    
                    retVal[rIndex, cIndex] = mat[row, col];

                    cIndex++;
                }
                rIndex++;
            }

            return retVal;
        }

        public double Minor(int r, int c)
        {
            return Sub(r, c).Determinant();
        }

        public double Cofactor(int r, int c)
        {
            if ((r + c) % 2 == 0)
                return Minor(r, c);
            else
                return -Minor(r, c);

        }

        public double Determinant()
        {
            return mat[0, 0] * Cofactor(0, 0) + mat[0, 1] * Cofactor(0, 1) + mat[0, 2] * Cofactor(0, 2) + mat[0,3] * Cofactor(0,3);
        }

        public bool Inversible()
        {
            return Determinant() != 0;
        }

        public Mat4 Inverse()
        {
            if (!Inversible())
                throw new ArithmeticException("Matrix non inversible");

            Mat4 retVal = new Mat4();

            double det = Determinant();

            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                {
                    retVal[col, row] = Cofactor(row, col) / det;
                }

            return retVal;
        }
    }


}