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
            Assert.Equal("8 -8 6 0", (v+v).ToString());
        }
        [Fact]
        public void VectorPointAdditionTest()
        {
            var p = new Point ( 3, -2,  5);
            var v = new Vector(-2,  3,  1);
            Assert.Equal("1 1 6 1", (v+p).ToString());
        }

        [Fact]
        public void VectorSubtractionTest()
        {
            var v1 = new Vector(3, 2, 1);
            var v2 = new Vector(5, 6, 7);
            Assert.Equal("-2 -4 -6 0", (v1-v2).ToString());
        }
        [Fact]
        public void PointSubtractionTest()
        {
            var p1 = new Point( 3,  2,  1);
            var p2 = new Point( 5,  6,  7);
            Assert.Equal("-2 -4 -6 0", (p1-p2).ToString());
        }
        [Fact]
        public void PointVectorSubtractionTest()
        {
            var p = new Point( 3,  2,  1);
            var v = new Vector( 5,  6,  7);
            Assert.Equal("-2 -4 -6 1", (p-v).ToString());
        }

        [Fact]
        public void PointNegationTest()
        {
            var p = new Point( 1,  -2,  3);
            Assert.Equal("-1 2 -3 -1", (-p).ToString());
        }
        [Fact]
        public void VectorNegationTest()
        {
            var v = new Vector( 5,  6,  7);
            Assert.Equal("-5 -6 -7 0", (-v).ToString());
        }
        
        [Fact]
        public void VectorMultiplicationTest()
        {
            var v = new Vector( 1,  -2,  3);
            Assert.Equal("3,5 -7 10,5 0", (v * 3.5).ToString());
        }
        [Fact]
        public void VectorDivisionTest()
        {
            var v = new Vector(1, -2, 3);
            Assert.Equal("0,5 -1 1,5 0", (v/2).ToString());
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
            Assert.Equal("0,4 0,6 0,8", (2*c).ToString());
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
            Assert.Equal("1,6 0,7 1", (c1+c2).ToString());
        }
        [Fact]
        public void ColorSubtractionTest()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);
            Assert.Equal($"{0.9d-0.7d} 0,5 0,5", (c1 - c2).ToString());
        }

    }
}
