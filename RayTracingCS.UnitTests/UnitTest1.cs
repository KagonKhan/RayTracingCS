using Xunit;
using RayTracingCS;
using System;
using System.Collections.Generic;
using System.Linq;


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
            Assert.Equal(20.0d, MatMaths.Dot(v1, v2));
        }
        [Fact]
        public void VectorCrossTest()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(2, 3, 4);
            Assert.Equal("-1 2 -1 0", v1.Cross(v2).ToString());
            Assert.Equal("1 -2 1 0", v2.Cross(v1).ToString());
        }

    }

    public class ColorTests
    {
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

            var b = m.Inversed();

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
            Assert.Equal(new Point(-8, 7, 3), MatMaths.Translation(5, -3, 2).Inversed() * p);
        }
        [Fact]
        public void ScalingTest()
        {
            var p = new Point(-4, 6, 8);
            var v = new Vector(-4, 6, 8);
            var ps = new Point(2, 3, 4);


            Assert.True(MatMaths.Scaling(2, 3, 4) * p == new Point(-8, 18, 32));
            Assert.True(MatMaths.Scaling(2, 3, 4) * v == new Vector(-8, 18, 32));
            Assert.True(MatMaths.Scaling(2, 3, 4).Inversed() * v == new Vector(-2, 2, 2));
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
            Assert.True(MatMaths.RotationX(Math.PI / 4).Inversed() * px == new Point(0, r22, -r22));
            Assert.True(MatMaths.RotationX(Math.PI / 2) * px == new Point(0, 0, 1));

            Assert.True(MatMaths.RotationY(Math.PI / 4) * py == new Point(r22, 0, r22));
            Assert.True(MatMaths.RotationY(Math.PI / 2) * py == new Point(1, 0, 0));

            Assert.True(MatMaths.RotationZ(Math.PI / 4) * pz == new Point(-r22, r22, 0));
            Assert.True(MatMaths.RotationZ(Math.PI / 2) * pz == new Point(-1, 0, 0));


        }
    }

    public class RayTests
    {

        public readonly double s22 = Math.Sqrt(2) / 2.0;

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
            var s = new Sphere();

            var intersection = s.IntersectionsWith(r);

            Assert.Equal(2, intersection.Count);
            Assert.Equal(4.0, intersection[0].t);
            Assert.Equal(6.0, intersection[1].t);
            Assert.IsType<Sphere>(intersection[0].obj);




            r.origin    = new Point(0, 1, -5);
            r.direction = new Vector(0, 0, 1);

            intersection = s.IntersectionsWith(r);

            Assert.Equal(2, intersection.Count);
            Assert.Equal(5.0, intersection[0].t);
            Assert.Equal(5.0, intersection[1].t);




            r.origin    = new Point(0, 2, -5);
            r.direction = new Vector(0, 0, 1);

            intersection = s.IntersectionsWith(r);

            Assert.Empty(intersection);




            r.origin    = new Point(0, 0, 0);
            r.direction = new Vector(0, 0, 1);

            intersection = s.IntersectionsWith(r);

            Assert.Equal(2, intersection.Count);
            Assert.Equal(-1.0, intersection[0].t);
            Assert.Equal(1.0, intersection[1].t);




            r.origin    = new Point(0, 0, 5);
            r.direction = new Vector(0, 0, 1);

            intersection = s.IntersectionsWith(r);

            Assert.Equal(2, intersection.Count);
            Assert.Equal(-6.0, intersection[0].t);
            Assert.Equal(-4.0, intersection[1].t);

        }


        [Fact]
        public void HitTest()
        {
            var i1 = new Intersection(new Sphere(), 1);
            var i2 = new Intersection(new Sphere(), 2);
            var list = new List<Intersection> { i1, i2 };
            Assert.Equal(i1, Intersection.Hit(list));



            i1 = new Intersection(new Sphere(), -1);
            i2 = new Intersection(new Sphere(), 1);
            list = new List<Intersection> { i1, i2 };
            Assert.Equal(i2, Intersection.Hit(list));


            i1 = new Intersection(new Sphere(), -1);
            i2 = new Intersection(new Sphere(), -2);
            list = new List<Intersection> { i1, i2 };
            Assert.Null(Intersection.Hit(list));


            i1 = new Intersection(new Sphere(), 5);
            i2 = new Intersection(new Sphere(), 7);
            var i3 = new Intersection(new Sphere(), -3);
            var i4 = new Intersection(new Sphere(), 2);
            list = new List<Intersection> { i1, i2, i3, i4 };
            Assert.Equal(i4, Intersection.Hit(list));



        }

        [Fact]
        public void RayTransformsTest()
        {
            var r = new Ray(new Point(1, 2, 3), new Vector(0, 1, 0));
            var r2 = r.Transformed(MatMaths.Translation(3, 4, 5));

            Assert.Equal(new Point(4, 6, 8), r2.origin);
            Assert.Equal(new Vector(0, 1, 0), r2.direction);



            r2 = r.Transformed(MatMaths.Scaling(2, 3, 4));

            Assert.Equal(new Point(2, 6, 12), r2.origin);
            Assert.Equal(new Vector(0, 3, 0), r2.direction);



            r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            s.Transformation = MatMaths.Scaling(2, 2, 2);

            var xs = s.IntersectionsWith(r);

            Assert.Equal(2, xs.Count);
            Assert.Equal(3, xs[0].t);
            Assert.Equal(7, xs[1].t);


        }


        [Fact]
        public void NormalTests()
        {
            var s = new Sphere();
            double s33 = Math.Sqrt(3d) / 3d;
            double s22 = Math.Sqrt(2d) / 2d;

            Assert.Equal(new Vector(1, 0, 0), s.NormalAt(new Point(1, 0, 0)));
            Assert.Equal(new Vector(0, 1, 0), s.NormalAt(new Point(0, 1, 0)));
            Assert.Equal(new Vector(0, 0, 1), s.NormalAt(new Point(0, 0, 1)));
            Assert.Equal(new Vector(s33, s33, s33), s.NormalAt(new Point(s33, s33, s33)));
            Assert.Equal(s.NormalAt(new Point(s33, s33, s33)).Normalized(), s.NormalAt(new Point(s33, s33, s33)));

            s.Transformation = MatMaths.Translation(0, 1, 0);
            Assert.Equal(new Vector(0, s22, -s22), s.NormalAt(new Point(0, 1.70711, -0.70711)));
        
            s.Transformation = MatMaths.Scaling(1, 0.5, 1) * MatMaths.RotationZ(Math.PI/5);
            Assert.Equal(new Vector(0, 0.97014, -0.24254), s.NormalAt(new Point(0, s22, -s22)));
        
        
        }

        [Fact]
        public void ReflectTests()
        {
            double s33 = Math.Sqrt(3d) / 3d;
            double s22 = Math.Sqrt(2d) / 2d;


            var v = new Vector(1, -1, 0);
            var n = new Vector(0, 1, 0);

            var r = Vector.Reflect(v, n);
            Assert.Equal(new Vector(1, 1, 0), r);

            v = new Vector(0, -1, 0);
            n = new Vector(s22, s22, 0);
            r = Vector.Reflect(v, n);
            Assert.Equal(new Vector(1, 0, 0), r);

        }
    }



    public class LightTests
    { 
        [Fact]
        public void LightCreationTests()
        {
            var light = new PointLight(new Point(0, 0, 0), new Color(1, 1, 1));

            Assert.Equal(new Point(0, 0, 0), light.pos);
            Assert.Equal(new Color(1, 1, 1), light.intensity);
        }
        
        [Fact]
        public void MaterialCreationTests()
        {
            var mat = new Material(0.1f, 0.9f, 0.9f, 200f, new Color(1,1,1));
            Assert.Equal(0.1f, mat.ambient);
            Assert.Equal(0.9f, mat.diffuse);
            Assert.Equal(0.9f, mat.specular);
            Assert.Equal(200f, mat.shininess);
            Assert.Equal(new Color(1,1,1), mat.color);

            var s = new Sphere();
            s.material = mat;

            Assert.Equal(mat, s.material);
        }

        [Fact]
        public void LightingTests()
        {
            var m = new Material();
            var pos = new Point(0, 0, 0);
            var eye = new Vector(0, 0, -1);
            var normal = new Vector(0, 0, -1);
            var l = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
           
            var res = l.Lighting(m, new Sphere(), pos, eye, normal);
            Assert.Equal(new Color(1.9, 1.9, 1.9), res);


            l = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));
            res = l.Lighting(m, new Sphere(), pos, eye, normal);
            Assert.Equal(new Color(0.7364, 0.7364, 0.7364), res);




            double s22 = Math.Sqrt(2) / 2d;
            eye = new Vector(0,s22,-s22);
            l = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));

            res = l.Lighting(m, new Sphere(), pos, eye, normal);
            Assert.Equal(new Color(1.0,1.0,1.0), res);
            




  
            eye = new Vector(0,-s22,-s22);
            l = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));

            res = l.Lighting(m, new Sphere(), pos, eye, normal);
            Assert.Equal(new Color(1.6364, 1.6364, 1.6364), res);







            eye = new Vector(0, 0, -1);
            l = new PointLight(new Point(0, 0, 10), new Color(1, 1, 1));
            res = l.Lighting(m, new Sphere(), pos, eye, normal);
            Assert.Equal(new Color(0.1, 0.1, 0.1), res);


        }
        [Fact]
        public void ShadowTests()
        {

            //There is no shadow when nothing is collinear with point and light
            var w = new World();
            var p = new Point(0, 10, 0);

            var res = w.IsShadowed(p, 0);
            Assert.False(res);



            //The shadow when an object is between the point and the light
            p = new Point(10, -10, 10);

            res = w.IsShadowed(p, 0);
            Assert.True(res);



            //There is no shadow when an object is behind the light
            p = new Point(-20, 20, -20);

            res = w.IsShadowed(p, 0);
            Assert.False(res);




            //There is no shadow when an object is behind the point
            p = new Point(-2, 2, -2);

            res = w.IsShadowed(p, 0);
            Assert.False(res);
        }

        [Fact]
        public void ShadeHits()
        {
            //shade_hit() is given an intersection in shadow
            var s1 = new Sphere();
            var s2 = new Sphere();
            s2.Transformation = Mat4.I.Translated(0, 0, 10);
            
            var l = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            var w = new World(s1,s2);
            w.lights.Add(l);


            var r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            var i = new Intersection(s2, 4);
            var comps = i.Compute(r);
            var c = w.Shading(comps, 0);

            Assert.Equal(new Color(0.1, 0.1, 0.1), c);




            //The hit should offset the point
            r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            s1 = new Sphere();
            s1.Transformation = Mat4.I.Translated(0, 0, 1);
            w.objects.Clear();
            w.objects.AddLast(s1);


            i = new Intersection(s1, 5);
            comps = i.Compute(r);

            Assert.True(comps.over_point.Z < -MatMaths.eps/2);
            Assert.True(comps.point.Z > comps.over_point.Z);
        }
    }

    public class ObjectTests
    {
        [Fact]
        public void ObjectCreationTests()
        {
            //The default transformation
            HitObject s = new Sphere();
            Assert.Equal(Mat4.I, s.Transformation);


            //Assigning a transformation
            s.Transformation = Mat4.I.Translated(2, 3, 4);
            Assert.Equal(MatMaths.Translation(2,3,4), s.Transformation);


            //The default material
            Assert.Equal(new Material(), s.material);

            // Assigning materials
            var m = new Material();
            m.ambient = 1;
            s.material = m;
            Assert.Equal(m, s.material);
        }

        [Fact]
        public void PlaneTests()
        {
            // The normal of a plane is constant everywhere
            var p = new Plane();
            var n1 = p.NormalAt(new Point(0, 0, 0));
            var n2 = p.NormalAt(new Point(10, 0, -10));
            var n3 = p.NormalAt(new Point(-5, 0, 150));

            Assert.Equal(new Vector(0, 1, 0), n1);
            Assert.Equal(new Vector(0, 1, 0), n2);
            Assert.Equal(new Vector(0, 1, 0), n3);
        }
    }

    public class SceneTests
    {
        [Fact]
        public void WorldCreationTests()
        {
            var light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
            var s1 = new Sphere();
            var s2 = new Sphere();

            s1.material.color = new Color(0.8f, 1.0f, 0.6f);
            s1.material.diffuse = 0.7f;
            s1.material.specular = 0.2f;

            s2.Transformation = MatMaths.Scaling(0.5, 0.5, 0.5);

            var w = new World(s1, s2);
            w.lights.Add(light);

            Assert.Contains(s1, w.objects);
            Assert.Contains(s2, w.objects);
            Assert.Equal(light, w.lights[0]);
        }
        [Fact]
        public void WorldIntersectionTests()
        {
            var w = new World();
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var xs = w.Intersect(r);

            Assert.Equal(4, xs.Count);

            Assert.Equal(4,   xs[0].t);
            Assert.Equal(4.5, xs[1].t);
            Assert.Equal(5.5, xs[2].t);
            Assert.Equal(6,   xs[3].t);
        }

        [Fact]
        public void PrecomputationTests()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            var i = new Intersection(s, 4);
            var comps = i.Compute(r);

            Assert.Equal(i.t, comps.t);
            Assert.Equal(i.obj, comps.obj);
            Assert.Equal(new Point (0, 0, -1), comps.point);
            Assert.Equal(new Vector(0, 0, -1), comps.eye);
            Assert.Equal(new Vector(0, 0, -1), comps.normal);
        }

        [Fact]
        public void HitTests()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            var i = new Intersection(s, 4);
            var comps = i.Compute(r);

            Assert.False(comps.inside);



            r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            i = new Intersection(s, 1);
            comps = i.Compute(r);
            Assert.Equal(new Point (0, 0,  1), comps.point);
            Assert.Equal(new Vector(0, 0, -1), comps.eye);
            Assert.Equal(new Vector(0, 0,  -1), comps.normal);
            Assert.True(comps.inside);
        }

        [Fact]
        public void ShadingTests()
        {
            var w = new World();
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = w.objects.First.Value;
            var i = new Intersection(s, 4);
            var comps = i.Compute(r);
            var shade = w.Shading(comps, 0);

            Assert.Equal(new Color(0.38066, 0.47583, 0.2855), shade);

            w.lights.Clear();

            w.lights.Add(new PointLight(new Point(0f, 0.25f, 0f), new Color(1f, 1f, 1f)));
            r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            s = w.objects.First.Next.Value;
            i = new Intersection(s, 0.5);

            comps = i.Compute(r);
            shade = w.Shading(comps, 0);

            Assert.Equal(new Color(0.90498, 0.90498, 0.90498), shade);

        }
        [Fact]
        public void ColoringTests()
        {
            var w = new World();
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 1, 0));
            var c = w.Coloring(r);

            Assert.Equal(new Color(), c);



            r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            c = w.Coloring(r);

            Assert.Equal(new Color(0.38066, 0.47583, 0.2855), c);



            var outer = w.objects.First.Value;
            outer.material.ambient = 1f;
            var inner = w.objects.First.Next.Value;
            inner.material.ambient = 1f;
            r = new Ray(new Point(0, 0, 0.75), new Vector(0, 0, -1));
            c = w.Coloring(r);

            Assert.Equal(inner.material.color, c);
        }

        [Fact]
        public void ViewTransformTests()
        {
            var from = new Point(0, 0, 0);
            var to = new Point(0, 0, -1);
            var up = new Vector(0, 1, 0);
            var t = MatMaths.ViewTransform(from, to, up);

            Assert.Equal(MatMaths.I, t);



            from = new Point(0, 0, 0);
            to = new Point(0, 0, 1);
            up = new Vector(0, 1, 0);
            t = MatMaths.ViewTransform(from, to, up);

            Assert.Equal(MatMaths.Scaling(-1, 1,-1), t);



            from = new Point(0, 0, 8);
            to = new Point(0, 0, 0);
            up = new Vector(0, 1, 0);
            t = MatMaths.ViewTransform(from, to, up);

            Assert.Equal(MatMaths.Translation(0,0,-8), t);



            from = new Point(1,3,2);
            to = new Point(4,-2,8);
            up = new Vector(1, 1, 0);
            t = MatMaths.ViewTransform(from, to, up);
            var res = new Mat4(-0.50709, 0.50709, 0.67612, -2.36643,
                                0.76772, 0.60609, 0.12122, -2.82843,
                               -0.35857, 0.59761, -0.71714, 0.00000,
                                0.00000, 0.00000, 0.00000, 1.00000);

            Assert.Equal(res, t);
        }


        [Fact]
        public void CameraCreationTests()
        {
            var h = 160;
            var v = 120;
            var pi = Math.PI;
            var fov = pi / 2d;
            var c = new Camera(h, v, fov);

            Assert.Equal(h, c.width);
            Assert.Equal(v, c.height);
            Assert.Equal(fov, c.fov);
            Assert.Equal(MatMaths.I, c.Transformation);


            c = new Camera(200, 125, pi / 2);
            Assert.Equal(0.01, Math.Round(c.pxsize, 3));

            c = new Camera(125, 200, pi / 2);
            Assert.Equal(0.01, Math.Round(c.pxsize, 3));

        }


        [Fact]
        public void CameraRayTests()
        {
            var pi = Math.PI;
            var c = new Camera(201, 101, pi / 2);
            var r = c.Ray(50, 100);

            Assert.Equal(new Point(0, 0, 0),   r.origin);
            Assert.Equal(new Vector(0, 0, -1), r.direction);



            r = c.Ray(0, 0);

            Assert.Equal(new Point(0, 0, 0), r.origin);
            Assert.Equal(new Vector(0.66519, 0.33259, -0.66851), r.direction);



            c.Transformation = MatMaths.RotationY(pi / 4) * MatMaths.Translation(0, -2, 5);
            r = c.Ray(50, 100);
            var s22 = Math.Sqrt(2) / 2d;

            Assert.Equal(new Point(0, 2, -5), r.origin);
            Assert.Equal(new Vector(s22, 0, -s22), r.direction);

        }

        [Fact]
        public void CameraRenderTests()
        {
            var w = new World();
            var c = new Camera(11,11,Math.PI/2);
            c.Transformation = MatMaths.ViewTransform(new Point(0, 0, -5), new Point(0, 0, 0), new Vector(0, 1, 0));
            var image = c.Render(w);

            Assert.Equal(new Color(0.38066, 0.47583, 0.2855), image[5, 5]);
        }
    }



    public class IntersectionTests
    {
        [Fact]
        public void TransformedTests()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new TestObject();
            s.Transformation = Mat4.I.Scaled(2, 2, 2);
            s.IntersectionsWith(r);


            //Intersecting a scaled shape with a ray
            Assert.Equal(new Point(0, 0, -2.5), s.saved_ray.origin);
            Assert.Equal(new Vector(0, 0, 0.5), s.saved_ray.direction);

            //Intersecting a translated shape with a ray
            s.Transformation = Mat4.I.Translated(5, 0, 0);
            s.IntersectionsWith(r);
            Assert.Equal(new Point(-5, 0, -5), s.saved_ray.origin);
            Assert.Equal(new Vector(0, 0, 1), s.saved_ray.direction);


            //Computing the normal on a translated shape
            s.Transformation = Mat4.I.Translated(0, 1, 0);
            var n = s.NormalAt(new Point(0, 1.70711, -0.70711));
            Assert.Equal(new Vector(0, 0.70711, -0.70711), n);



            double s22 = Math.PI / 2.0;
            
            //Computing the normal on a transformed shape
            s.Transformation = Mat4.I.Scaled(1, 0.5, 1).RotatedZ(Math.PI/5);
            n = s.NormalAt(new Point(0, s22, -s22));
            Assert.Equal(new Vector(0, 0.97014, -0.24254), n);
        }


        public void PlaneIntersectionTests()
        {
            // Intersect with a ray parallel to the plane
            var p = new Plane();
            var r = new Ray(new Point(0, 10, 0), new Vector(0, 0, 1));
            var xs = p.IntersectionsWith(r);
            Assert.Empty(xs);

            // Intersect with a coplanar ray
            r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            xs = p.IntersectionsWith(r);
            Assert.Empty(xs);


            // A ray intersecting a plane from above
            r = new Ray(new Point(0, 1, 0), new Vector(0, -1, 0));
            xs = p.IntersectionsWith(r);
            Assert.Single(xs);
            Assert.Equal(1, xs[0].t);
            Assert.Equal(p, xs[0].obj);


            // A ray intersecting a plane from below
            r = new Ray(new Point(0, -1, 0), new Vector(0, 1, 0));
            xs = p.IntersectionsWith(r);
            Assert.Single(xs);
            Assert.Equal(1, xs[0].t);
            Assert.Equal(p, xs[0].obj);

        }

    }


    public class PatternTests
    {

        Color black = Color.Black;
        Color white = Color.White;

        [Fact]
        public void CreationTests()
        {
            var pat = new StripePattern(black, white);
            Assert.Equal(black, pat.a);
            Assert.Equal(white, pat.b);
        }
        [Fact]
        public void StripePatternTests()
        {
            // A stripe pattern is constant in y
            var pattern = new StripePattern(white, black);
            Assert.Equal(white, pattern.Get(new Point(0, 0, 0)));
            Assert.Equal(white, pattern.Get(new Point(0, 1, 0)));
            Assert.Equal(white, pattern.Get(new Point(0, 2, 0)));


            // A stripe pattern is constant in z
            Assert.Equal(white, pattern.Get(new Point(0, 0, 0)));
            Assert.Equal(white, pattern.Get(new Point(0, 0, 1)));
            Assert.Equal(white, pattern.Get(new Point(0, 0, 2)));

            // A stripe pattern alternates in x
            Assert.Equal(white, pattern.Get(new Point( 0.0, 0, 0)));
            Assert.Equal(white, pattern.Get(new Point( 0.9, 0, 0)));
            Assert.Equal(black, pattern.Get(new Point( 1.0, 0, 0)));
            Assert.Equal(black, pattern.Get(new Point(-0.1, 0, 0)));
            Assert.Equal(black, pattern.Get(new Point(-1.0, 0, 0)));
            Assert.Equal(white, pattern.Get(new Point(-1.1, 0, 0)));

        }

        [Fact]
        public void StripePatternLightingTests()
        {
            // Lighting with a pattern applied
            var m = new Material();
            m.pattern = new StripePattern(white, black);
            m.ambient = 1;
            m.diffuse = 0;
            m.specular = 0;
            var eyev = new Vector(0, 0, -1);
            var normalv  = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            var c1 = light.Lighting(m, new Plane(), new Point(0.9, 0, 0), eyev, normalv, false);
            var c2 = light.Lighting(m, new Plane(), new Point(1.1, 0, 0), eyev, normalv, false);

            Assert.Equal(white, c1);
            Assert.Equal(black, c2);

        }

        [Fact]
        public void TransformedStripePatternTests()
        {
            // Stripes with an object transformation
            var s = new Sphere();
            s.Transformation = Mat4.I.Scaled(2, 2, 2);
            var pattern = new StripePattern(white, black);
            var c = pattern.GetOnObject(new Point(1.5, 0, 0), s);
            Assert.Equal(white, c);


            // Stripes with a pattern transformation
            pattern.Transformation = Mat4.I.Scaled(2, 2, 2);
            c = pattern.GetOnObject(new Point(1.5, 0, 0), s);
            Assert.Equal(white, c);


            // Stripes with both an object and a pattern transformation
            pattern.Transformation = Mat4.I.Translated(0.5, 0, 0);
            c = pattern.GetOnObject(new Point(2.5, 0, 0), s);
            Assert.Equal(white, c);    
        
        }

        // TODO: finish pattern tests
    }


    public class ReflectionAndRefractionTests
    {
        public readonly double s22 = Math.Sqrt(2) / 2.0;


        [Fact]
        public void ReflectionTests()
        {
            // Reflectivity for the default material
            var m = new Material();
            Assert.Equal(0.0, m.reflective);



            // Precomputing the reflection vector

            var plane = new Plane();
            var r = new Ray(new Point(0, 1, -1), new Vector(0, -s22, s22));
            var i = new Intersection(plane, 2*s22);
            var comps = i.Compute(r);
            Assert.Equal(new Vector(0, s22, s22), comps.reflect);



            // The reflected color for a nonreflective material
            var w = new World();
            r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            var s = w.objects.First.Next.Value;
            s.material.ambient = 1;
            i = new Intersection(s, 1);
            comps = i.Compute(r);
            var color = w.ReflectiveShading(comps, 1);

            Assert.Equal(new Color(0, 0, 0), color);





        }
        [Fact]
        public void ReflectiveTesting()
        {
            // The reflected color for a reflective material
            var plane = new Plane();
            var w = new World();
            plane.material = new Material();
            plane.material.reflective = 0.5;
            plane.Transformation = Mat4.I.Translated(0, -1, 0);
            w.objects.AddLast(plane);
            var r = new Ray(new Point(0, 0, -3), new Vector(0, -s22, s22));
            var i = new Intersection(plane, 2*s22);
            var comps = i.Compute(r);
            var color = w.ReflectiveShading(comps, 1);

            Assert.Equal(new Color(0.19032, 0.2379, 0.14274), color);


            // The reflected color at the maximum recursive depth
            color = w.ReflectiveShading(comps, 0);

            Assert.Equal(new Color(0, 0, 0), color);


            // shade_hit() with a reflective material
            color = w.Shading(comps, 1);
            Assert.Equal(new Color(0.87677, 0.92436, 0.82918), color);


            // Careful, these results are semi far from accepted (0.01)deg
        }

        [Fact]
        public void ReflectiveRecursionTesting()
        {
            // color_at() with mutually reflective surfaces
            var w = new World();
            w.lights.Clear();
            w.lights.Add(new PointLight(new Point(0, 0, 0), new Color(1, 1, 1)));
            var lower = new Plane();
            lower.material.reflective = 1;
            lower.Transformation = Mat4.I.Translated(0, -1, 0);
            w.objects.AddLast(lower);

            var upper = new Plane();
            upper.material.reflective = 1;
            upper.Transformation = Mat4.I.Translated(0, 1, 0);
            w.objects.AddLast(upper);
            var r = new Ray(new Point(0, 0, 0), new Vector(0, 1, 0));
            var col = w.Coloring(r);

            Assert.Equal(col, col);
        }

        [Fact]
        public void ReflectiveRefractingMaterialTests()
        {
            // Transparency and Refractive Index for the default material
            var m = new Material();
            Assert.Equal(0.0, m.transparency);
            Assert.Equal(1.0, m.refraction);


            // A helper for producing a sphere with a glassy material
            var glassA = new Sphere();
            glassA.material = Material.Glass();
            Assert.Equal(Mat4.I, glassA.Transformation);
            Assert.Equal(1.0, glassA.material.transparency);
            Assert.Equal(1.5, glassA.material.refraction);



        }

        [Fact]
        public void N1N2ComputationTests()
        {
            // Finding n1 and n2 at various intersections
            var glassA = new Sphere();
            glassA.material = Material.Glass();
            glassA.Transformation = Mat4.I.Scaled(2, 2, 2);
            var glassB = new Sphere();
            glassB.material = Material.Glass();
            glassB.Transformation = Mat4.I.Translated(0, 0, -0.25);
            glassB.material.refraction = 2.0;
            var glassC = new Sphere();
            glassC.material = Material.Glass();
            glassC.Transformation = Mat4.I.Translated(0, 0, 0.25);
            glassC.material.refraction = 2.5;

            var r = new Ray(new Point(0, 0, -4), new Vector(0, 0, 1));
            var xs = new List<Intersection>();
            xs.Add(new Intersection(glassA, 2.00));
            xs.Add(new Intersection(glassB, 2.75));
            xs.Add(new Intersection(glassC, 3.25));
            xs.Add(new Intersection(glassB, 4.75));
            xs.Add(new Intersection(glassC, 5.25));
            xs.Add(new Intersection(glassA, 6.00));


            var comps = xs[0].Compute(r, xs);
            Assert.Equal(1.0, comps.n1);
            Assert.Equal(1.5, comps.n2);

            comps = xs[1].Compute(r, xs);
            Assert.Equal(1.5, comps.n1);
            Assert.Equal(2.0, comps.n2);

            comps = xs[2].Compute(r, xs);
            Assert.Equal(2.0, comps.n1);
            Assert.Equal(2.5, comps.n2);

            comps = xs[3].Compute(r, xs);
            Assert.Equal(2.5, comps.n1);
            Assert.Equal(2.5, comps.n2);

            comps = xs[4].Compute(r, xs);
            Assert.Equal(2.5, comps.n1);
            Assert.Equal(1.5, comps.n2);

            comps = xs[5].Compute(r, xs);
            Assert.Equal(1.5, comps.n1);
            Assert.Equal(1.0, comps.n2);

        }

        [Fact]
        public void UnderPointTests()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            s.material = Material.Glass();
            s.Transformation = Mat4.I.Translated(0, 0, 1);
            var i = new Intersection(s, 5);
            var xs = s.IntersectionsWith(r);
            var comps = i.Compute(r, xs);

            Assert.True(comps.under_point.Z > MatMaths.eps/2);
            Assert.True(comps.point.Z < comps.under_point.Z);
        }

        [Fact]
        public void RefractedOpaqueObjectTests()
        {
            // The refracted color with an opaque surface
            var w = new World();
            var s = w.objects.First.Value;
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var xs = new List<Intersection>();
            xs.Add(new Intersection(s, 4));
            xs.Add(new Intersection(s, 6));

            var comps = xs[0].Compute(r, xs);
            var c = w.RefractiveShading(comps, 5);

            Assert.Equal(new Color(0, 0, 0), c);


            //The refracted color at the maximum recursive depth
            s.material.transparency = 1.0;
            s.material.refraction   = 1.5;
            xs = new List<Intersection>();
            xs.Add(new Intersection(s, 4));
            xs.Add(new Intersection(s, 6));
            comps = xs[0].Compute(r, xs);
            c = w.RefractiveShading(comps, 0);
            Assert.Equal(new Color(0, 0, 0), c);
        }

        [Fact]
        public void RefractionTests()
        {

            // The refracted color under total internal reflection
            var w = new World();
            var A = w.objects.First.Value;
            A.material.transparency = 1.0;
            A.material.refraction   = 1.5;

            var r = new Ray(new Point(0, 0, s22), new Vector(0, 1, 0));
            var xs = new List<Intersection>();
            xs.Add(new Intersection(A, -s22));
            xs.Add(new Intersection(A, s22));

            var comps = xs[1].Compute(r, xs);
            var c = w.RefractiveShading(comps, 5);

            Assert.Equal(new Color(0, 0, 0), c);
        }

        [Fact]
        public void RefractionTests2()
        {
            var w = new World();
            var A = w.objects.First.Value;
            A.material.ambient = 1.0f;
            A.material.pattern = new TestPattern();

            // The refracted color with a refracted ray
            var B = w.objects.First.Next.Value;
            B.material.transparency = 1.0;
            B.material.refraction = 1.5;

            var r = new Ray(new Point(0,0,0.1), new Vector(0, 1, 0));
            var xs = new List<Intersection>();
            xs.Add(new Intersection(A, -0.9899));
            xs.Add(new Intersection(B, -0.4899));
            xs.Add(new Intersection(B,  0.4899));
            xs.Add(new Intersection(A,  0.9899));

            var comps = xs[2].Compute(r, xs);
            var c = w.RefractiveShading(comps, 5);

            Assert.Equal(new Color(0, 0.99888, 0.04725), c);

        }
    }
}


