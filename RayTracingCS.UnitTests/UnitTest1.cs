using Xunit;
using RayTracingCS;
using System;

namespace RayTracingCS.UnitTests
{
    public class TupleTests
    {
        [Fact]
        public void PointCreationTest()
        {
            var p = new Point(4, -4, 3);
            Assert.Equal("4 -4 3 1", p.ToString());
        }
        [Fact]
        public void VectorCreationTest()
        {
            var v = new Vector(4, -4, 3);
            Assert.Equal("4 -4 3 0", v.ToString());
        }



        [Fact]
        public void VectorAdditionTest()
        {
            var v = new Vector(4, -4, 3);
            Assert.Equal("8 -8 6 0", (v + v).ToString());
        }
        [Fact]
        public void VectorPointAdditionTest()
        {
            var p = new Point(3, -2, 5);
            var v = new Vector(-2, 3, 1);
            Assert.Equal("1 1 6 1", (v + p).ToString());
        }

        [Fact]
        public void VectorSubtractionTest()
        {
            var v1 = new Vector(3, 2, 1);
            var v2 = new Vector(5, 6, 7);
            Assert.Equal("-2 -4 -6 0", (v1 - v2).ToString());
        }
        [Fact]
        public void PointSubtractionTest()
        {
            var p1 = new Point(3, 2, 1);
            var p2 = new Point(5, 6, 7);
            Assert.Equal("-2 -4 -6 0", (p1 - p2).ToString());
        }
        [Fact]
        public void PointVectorSubtractionTest()
        {
            var p = new Point(3, 2, 1);
            var v = new Vector(5, 6, 7);
            Assert.Equal("-2 -4 -6 1", (p - v).ToString());
        }

        [Fact]
        public void PointNegationTest()
        {
            var p = new Point(1, -2, 3);
            Assert.Equal("-1 2 -3 -1", (-p).ToString());
        }
        [Fact]
        public void VectorNegationTest()
        {
            var v = new Vector(5, 6, 7);
            Assert.Equal("-5 -6 -7 0", (-v).ToString());
        }

        [Fact]
        public void VectorMultiplicationTest()
        {
            var v = new Vector(1, -2, 3);
            Assert.Equal("3,5 -7 10,5 0", (v * 3.5).ToString());
        }
        [Fact]
        public void VectorDivisionTest()
        {
            var v = new Vector(1, -2, 3);
            Assert.Equal("0,5 -1 1,5 0", (v / 2).ToString());
        }


        [Fact]
        public void VectorMagnitudeTest()
        {
            var v = new Vector(-1, -2, -3);
            Assert.Equal(Math.Sqrt(14), v.Magnitude());
        }
        [Fact]
        public void VectorNormalizationTest()
        {
            var v = new Vector(4, 0, 0);
            Assert.Equal("1 0 0 0", (v.Normalize()).ToString());
            Assert.Equal(1.0d, (v.Normalize()).Magnitude());
        }
        [Fact]
        public void VectorDotTest()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(2, 3, 4);
            Assert.Equal(20.0d, v1.Dot(v2));
        }
        [Fact]
        public void VectorCrossTest()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(2, 3, 4);
            Assert.Equal("-1 2 -1 0", v1.Cross(v2).ToString());
            Assert.Equal("1 -2 1 0", v2.Cross(v1).ToString());
        }




        [Fact]
        public void ColorCreationTest()
        {
            var c = new Color(0.2, 0.3, 0.4);
            Assert.Equal("0,4 0,6 0,8", (2 * c).ToString());
        }
        [Fact]
        public void ColorMultiplicationTest()
        {
            var c = new Color(-0.5, 0.4, 1.7);
            Assert.Equal("-0,5 0,4 1,7", c.ToString());
        }
        [Fact]
        public void ColorAdditionTest()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);
            Assert.Equal("1,6 0,7 1", (c1 + c2).ToString());
        }
        [Fact]
        public void ColorSubtractionTest()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);
            Assert.Equal($"{0.9d - 0.7d} 0,5 0,5", (c1 - c2).ToString());
        }




        [Fact]
        public void MatrixIndexing()
        {
            var m = new Mat4(1.0, 2.0, 3.0, 4.0,
                              5.5, 6.5, 7.5, 8.5,
                              9.0, 10, 11, 12,
                              13.5, 14.5, 15.5, 16.5);

            Assert.Equal($"1", (m[0, 0]).ToString());
            Assert.Equal($"4", (m[0, 3]).ToString());
            Assert.Equal($"5,5", (m[1, 0]).ToString());
            Assert.Equal($"7,5", (m[1, 2]).ToString());
            Assert.Equal($"11", (m[2, 2]).ToString());
            Assert.Equal($"13,5", (m[3, 0]).ToString());
            Assert.Equal($"15,5", (m[3, 2]).ToString());
        }

        [Fact]
        public void MatrixComparison()
        {
            var m1 = new Mat4(1.0, 2.0, 3.0, 4.0,
                              5.5, 6.5, 7.5, 8.5,
                              9.0, 10, 11, 12,
                              13.5, 14.5, 15.5, 16.5);
            var m2 = new Mat4(1.0, 2.0, 3.0, 4.0,
                              5.5, 6.5, 7.5, 8.5,
                              9.0, 10, 11, 12,
                              13.5, 14.5, 15.5, 16.5);

            var m3 = new Mat4(1.0, 2.0, 3.0, 4.0,
                              5.5, 6.5, 7.5, 8.5,
                              9.0, 10, 12, 12,
                              13.5, 14.5, 15.5, 16.5);

            Assert.True(m1 == m2);
            Assert.True(m1 != m3);
            Assert.False(m1 == m3);
        }

        [Fact]
        public void MatrixMultiplication()
        {
            var m1 = new Mat4(1, 2, 3, 4,
                              5, 6, 7, 8,
                              9, 8, 7, 6,
                              5, 4, 3, 2);

            var m2 = new Mat4(-2, 1, 2, 3,
                               3, 2, 1, -1,
                               4, 3, 6, 5,
                               1, 2, 7, 8);

            var res = new Mat4(20, 22, 50, 48,
                               44, 54, 114, 108,
                               40, 58, 110, 102,
                               16, 26, 46, 42);
            Assert.True((res) == (m1 * m2));
        }


        [Fact]
        public void MatrixTupleComparison()
        {
            var m = new Mat4(1, 2, 3, 4,
                             2, 4, 4, 2,
                             8, 6, 4, 1,
                             0, 0, 0, 1);

            var p = new Point(1, 2, 3);

            Assert.Equal("18 24 33 1", (m * p).ToString());
        }

        [Fact]
        public void MatrixIdentityTest()
        {
            var m = new Mat4(1, 2, 3, 4,
                             2, 4, 4, 2,
                             8, 6, 4, 1,
                             0, 0, 0, 1);


            Assert.True(m == m * Mat4.I);
        }
        [Fact]
        public void MatrixTransposeTest()
        {
            var m1 = new Mat4(0, 9, 3, 0,
                              9, 8, 0, 8,
                              1, 8, 5, 3,
                              0, 0, 5, 8);

            var m2 = new Mat4(0, 9, 1, 0,
                              9, 8, 8, 0,
                              3, 0, 5, 5,
                              0, 8, 3, 8);


            Assert.True(m1.Transpose() == m2);
            Assert.True(Mat4.I == Mat4.I.Transpose());
        }


        [Fact]
        public void Matrix4SubTest()
        {
            var m1 = new Mat4(-6, 1, 1, 6,
                              -8, 5, 8, 6,
                              -1, 0, 8, 2,
                              -7, 1, -1, 1);

            var m2 = new Mat3(-6, 1, 6,
                              -8, 8, 6,
                              -7, -1, 1);

            var m3 = new Mat2(8, 6, -1, 1);

            Assert.True(m1.Sub(2, 1) == m2);
            Assert.True(m2.Sub(0, 0) == m3);
        }
        [Fact]
        public void MatrixMinorTest()
        {


            var m2 = new Mat3(3, 5, 0,
                              2, -1, -7,
                              6, -1, 5);


            Assert.True(m2.Minor(1, 0) == 25);
            Assert.True(m2.Cofactor(1, 0) == -25);

        }

        [Fact]
        public void Matrix3DeterminantTest()
        {
            var m = new Mat3(1, 2, 6,
                             -5, 8, -4,
                             2, 6, 4);

            Assert.True(m.Cofactor(0, 0) == 56);
            Assert.True(m.Cofactor(0, 1) == 12);
            Assert.True(m.Cofactor(0, 2) == -46);
            Assert.True(m.Determinant() == -196);

        }

        [Fact]
        public void Matrix4DeterminantTest()
        {
            var m = new Mat4(-2, -8, 3, 5,
                             -3, 1, 7, 3,
                              1, 2, -9, 6,
                             -6, 7, 7, -9);

            Assert.True(m.Cofactor(0, 0) == 690);
            Assert.True(m.Cofactor(0, 1) == 447);
            Assert.True(m.Cofactor(0, 2) == 210);
            Assert.True(m.Cofactor(0, 3) == 51);
            Assert.True(m.Determinant() == -4071);
        }
        [Fact]
        public void Matrix4InverseTest()
        {
            var m = new Mat4(-5,  2,  6, -8,
                              1, -5,  1,  8,
                              7,  7, -6, -7,
                              1, -3,  7,  4);

            var b = m.Inverse();

            var res = new Mat4(0.21805 ,  0.45113,  0.24060, -0.04511,
                               -0.80827, -1.45677, -0.44361,  0.52068,
                               -0.07895, -0.22368, -0.05263,  0.19737,
                               -0.52256, -0.81391, -0.30075,  0.30639);

            Assert.Equal(532d, m.Determinant());

            Assert.Equal(-160, m.Cofactor(2, 3));
            Assert.Equal(-160 / 532d, b[3, 2]);

            Assert.Equal(105, m.Cofactor(3, 2));
            Assert.Equal(105 / 532d, b[2,3]);

            Assert.True(res == b);


        }
    }
}
