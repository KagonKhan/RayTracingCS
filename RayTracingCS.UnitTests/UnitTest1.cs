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
            Assert.Equal("1 0 0 0", (v.Normalized()).ToString());
            Assert.Equal(1.0d, (v.Normalized()).Magnitude());
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



    }

    public class MatrixTests
    {
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

            Assert.Equal(m1, m2);
            Assert.NotEqual(m1, m3);
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
            Assert.Equal((res) , (m1 * m2));
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
            Mat4 i = new Mat4(Mat4.I);

            Assert.Equal(m, i * m);
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


            Assert.Equal(m2, m1.Transposed());
            Assert.Equal(Mat4.I, Mat4.I.Transposed());
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
            
            Assert.Equal(m2, m1.Sub(2, 1));
            Assert.Equal(m3, m2.Sub(0, 0));
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
            var m = new Mat4(-5, 2, 6, -8,
                              1, -5, 1, 8,
                              7, 7, -6, -7,
                              1, -3, 7, 4);

            var b = m.GetInverse();

            var res = new Mat4(0.21805, 0.45113, 0.24060, -0.04511,
                               -0.80827, -1.45677, -0.44361, 0.52068,
                               -0.07895, -0.22368, -0.05263, 0.19737,
                               -0.52256, -0.81391, -0.30075, 0.30639);

            Assert.True(Math.Abs(532d- m.Determinant()) < MatMaths.eps);
            Assert.Equal(res, b);

            Assert.True(Math.Abs(-160 - m.Cofactor(2, 3)) < MatMaths.eps);
            Assert.True(Math.Abs(-160 / 532d- b[3, 2]) < MatMaths.eps);

            Assert.Equal(105, m.Cofactor(3, 2));
            Assert.True(Math.Abs(105 / 532d - b[2, 3]) < MatMaths.eps);



        }


        [Fact]
        public void TranslationsTest()
        {
            var p = new Point(-3, 4, 5);
            var v = new Vector(-3, 4, 5);

            Assert.Equal(new Point(2, 1, 7),  MatMaths.Translation(5, -3, 2) * p);
            Assert.Equal(v, MatMaths.Translation(5, -3, 2) * v);
            Assert.Equal(new Point(-8, 7, 3), MatMaths.Translation(5, -3, 2).GetInverse() * p);
        }
        [Fact]
        public void ScalingTest()
        {
            var p = new Point(-4, 6, 8);
            var v = new Vector(-4, 6, 8);
            var ps = new Point(2, 3, 4);


            Assert.True(MatMaths.Scaling(2, 3, 4) * p == new Point(-8, 18, 32));
            Assert.True(MatMaths.Scaling(2, 3, 4) * v == new Vector(-8, 18, 32));
            Assert.True(MatMaths.Scaling(2, 3, 4).GetInverse() * v == new Vector(-2, 2, 2));
            Assert.True(MatMaths.Scaling(-1, 1, 1) * ps == new Point(-2, 3, 4));

        }
        [Fact]
        public void RotationTest()
        {
            var px = new Point(0, 1, 0);
            var py = new Point(0, 0, 1);
            var pz = new Point(0, 1, 0);

            double r22 = Math.Sqrt(2) / 2d;

            Assert.True(MatMaths.RotationX(Math.PI / 4) * px == new Point(0, r22, r22));
            Assert.True(MatMaths.RotationX(Math.PI / 4).GetInverse() * px == new Point(0, r22, -r22));
            Assert.True(MatMaths.RotationX(Math.PI / 2) * px == new Point(0, 0, 1));

            Assert.True(MatMaths.RotationY(Math.PI / 4) * py == new Point(r22, 0, r22));
            Assert.True(MatMaths.RotationY(Math.PI / 2) * py == new Point(1, 0, 0));

            Assert.True(MatMaths.RotationZ(Math.PI / 4) * pz == new Point(-r22, r22, 0));
            Assert.True(MatMaths.RotationZ(Math.PI / 2) * pz == new Point(-1, 0, 0));


        }
    }


    public class RayTests
    {
        [Fact]
        public void RayCreationTest()
        {
            var origin = new Point(1, 2, 3);
            var direction = new Vector(4, 5, 6);
            var r = new Ray(origin, direction);

            Assert.Equal(origin, r.origin);
            Assert.Equal(direction, r.direction);
        }
        [Fact]
        public void RayBehaviorTest()
        {
            var origin = new Point(2, 3, 4);
            var direction = new Vector(1, 0, 0);

            var r = new Ray(origin, direction);

            Assert.Equal(new Point(2, 3, 4), r.position(0));
            Assert.Equal(new Point(3, 3, 4), r.position(1));
            Assert.Equal(new Point(1, 3, 4), r.position(-1));
            Assert.Equal(new Point(4.5, 3, 4), r.position(2.5));
        }
        [Fact]
        public void RaySphereIntersectionTest()
        {
            var origin      = new Point (0, 0, -5);
            var direction   = new Vector(0, 0, 1);

            var r = new Ray(origin, direction);
            var s = new Sphere(new Point(0,0,0), 1d);

            var intersection = s.intersects(r);

            Assert.Equal(2, intersection.Length);
            Assert.Equal(4.0, intersection[0].t);
            Assert.Equal(6.0, intersection[1].t);
            Assert.IsType<Sphere>(intersection[0].obj);




            r.origin    = new Point(0, 1, -5);
            r.direction = new Vector(0, 0, 1);

            intersection = s.intersects(r);

            Assert.Equal(2, intersection.Length);
            Assert.Equal(5.0, intersection[0].t);
            Assert.Equal(5.0, intersection[1].t);




            r.origin    = new Point(0, 2, -5);
            r.direction = new Vector(0, 0, 1);

            intersection = s.intersects(r);

            Assert.Equal(0, intersection.Length);




            r.origin    = new Point(0, 0, 0);
            r.direction = new Vector(0, 0, 1);

            intersection = s.intersects(r);

            Assert.Equal(2, intersection.Length);
            Assert.Equal(-1.0, intersection[0].t);
            Assert.Equal(1.0, intersection[1].t);




            r.origin    = new Point(0, 0, 5);
            r.direction = new Vector(0, 0, 1);

            intersection = s.intersects(r);

            Assert.Equal(2, intersection.Length);
            Assert.Equal(-6.0, intersection[0].t);
            Assert.Equal(-4.0, intersection[1].t);

        }


        [Fact]
        public void HitTest()
        {
            var i1 = new Intersection(new Sphere(), 1);
            var i2 = new Intersection(new Sphere(), 2);
            Assert.Equal(i1, Intersection.Hit(i1, i2));



            i1 = new Intersection(new Sphere(), -1);
            i2 = new Intersection(new Sphere(), 1);
            Assert.Equal(i2, Intersection.Hit(i1, i2));


            i1 = new Intersection(new Sphere(), -1);
            i2 = new Intersection(new Sphere(), -2);
            Assert.Equal(new Intersection(), Intersection.Hit(i1, i2));


            i1 = new Intersection(new Sphere(), 5);
            i2 = new Intersection(new Sphere(), 7);
            var i3 = new Intersection(new Sphere(), -3);
            var i4 = new Intersection(new Sphere(), 2);
            Assert.Equal(i4, Intersection.Hit(i1, i2, i3, i4));



        }

        [Fact]
        public void RayTransformsTest()
        {
            var r = new Ray(new Point(1, 2, 3), new Vector(0, 1, 0));
            var r2 = r.Transform(MatMaths.Translation(3, 4, 5));

            Assert.Equal(new Point(4, 6, 8), r2.origin);
            Assert.Equal(new Vector(0, 1, 0), r2.direction);



            r2 = r.Transform(MatMaths.Scaling(2, 3, 4));

            Assert.Equal(new Point(2, 6, 12), r2.origin);
            Assert.Equal(new Vector(0, 3, 0), r2.direction);



            r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            s.Transformation = MatMaths.Scaling(2, 2, 2);

            var xs = s.intersects(r);

            Assert.Equal(2, xs.Length);
            Assert.Equal(3, xs[0].t);
            Assert.Equal(7, xs[1].t);


        }

    }
}


