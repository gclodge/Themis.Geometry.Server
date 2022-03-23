using System.Linq;

using Themis.Geometry.Server.Models.Points;

using Bogus;
using Xunit;
using Assert = Xunit.Assert;

namespace Themis.Geometry.Server.Tests.Models.Points
{
    public class PointTest
    {
        const int Dimensions = 3;
        private Faker<Point> faker;

        public PointTest()
        {
            faker = new Faker<Point>()
                .RuleFor(p => p.Position, f => f.GenerateRandomVector(Dimensions))
                .RuleFor(p => p.Attributes, f => f.GenerateRandomDictionary(Dimensions));
        }

        [Fact]
        public void PointEqualsSuccessTest()
        {
            var Expected = faker.Generate();

            var Actual = Point.FromPoint(Expected);

            Assert.True(Expected.Equals(Actual));
            Assert.True(Expected.Equals(Actual as object));

            foreach (var key in Expected.Attributes.Keys)
            {
                Assert.True(Actual.Attributes.ContainsKey(key));
                Assert.Equal(Expected.Attributes[key], Actual.Attributes[key]);
            }
        }

        [Fact]
        public void PointEqualsFailByAttributeTest()
        {
            var Expected = faker.Generate();

            var Actual = Point.FromPoint(Expected)
                              .WithAttributes(faker.Generate());

            Assert.False(Expected.Equals(Actual));
            Assert.False(Expected.Equals(Actual as object));
        }

        [Fact]
        public void PointEqualsFailByPositionTest()
        {
            var Expected = faker.Generate();

            var Actual = Point.FromPoint(Expected)
                              .WithPosition(faker.Generate());

            Assert.False(Expected.Equals(Actual));
            Assert.False(Expected.Equals(Actual as object));
        }

        [Fact]
        public void PointCoordinateValuesTest()
        {
            var Source = faker.Generate();

            var Expected2D = Point.FromPoint(Source)
                                  .WithPosition(Source.Position.Take(2));
            var Expected3D = Point.FromPoint(Source)
                                  .WithPosition(Source.Position.Take(3));

            Assert.Equal(Source.X, Expected2D.X);
            Assert.Equal(Source.Y, Expected2D.Y);
            Assert.True(double.IsNaN(Expected2D.Z));

            Assert.True(Expected3D.Position.All(x => !double.IsNaN(x)));
        }
    }
}
