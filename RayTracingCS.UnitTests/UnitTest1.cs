using Xunit;
using RayTracingCS;


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


    }
}
