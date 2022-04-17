using Xunit;
using RayTracingCS;


namespace RayTracingCS.UnitTests
{
    public class TupleTests
    {
        [Fact]
        public void PointCreationTest()
        {
            var vec = new Point(4, -4, 3);
            Assert.Equal("4 -4 3 1", vec.ToString());
        }

        [Fact]
        public void VectorCreationTest()
        {
            var vec = new Vector(4, -4, 3);
            Assert.Equal("4 -4 3 0", vec.ToString());
        }

    }
}
