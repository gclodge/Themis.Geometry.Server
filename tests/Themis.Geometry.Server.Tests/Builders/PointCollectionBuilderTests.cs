using System.Linq;

using Themis.Geometry.Server.Builders;
using Themis.Geometry.Server.Models.Points;

using Bogus;
using Xunit;
using Assert = Xunit.Assert;
using Newtonsoft.Json;

namespace Themis.Geometry.Server.Tests.Builders
{
    public class PointCollectionBuilderTests
    {
        const int Dimensions = 3;
        private Faker<Point> faker;

        public PointCollectionBuilderTests()
        {
            faker = new Faker<Point>()
                .RuleFor(p => p.Position, f => f.GenerateRandomVector(Dimensions))
                .RuleFor(p => p.Attributes, f => f.GenerateRandomDictionary(Dimensions));
        }

        [Fact]
        public void BuildPointCollectionFromSinglePointTest()
        {
            var point = faker.Generate();

            var pcoll = new PointCollectionBuilder().AddPoint(point)
                                                    .Build();

            Assert.Single(pcoll.Points);
            Assert.Equal(point, pcoll.Points.Single());
        }

        [Fact]
        public void BuildPointCollectionFromEnumerableTest()
        {
            int count = 5;
            var points = faker.Generate(count);

            var pcoll = new PointCollectionBuilder().AddPoints(points)
                                                    .Build();

            Assert.Equal(count, pcoll.Count);
            foreach (int i in Enumerable.Range(0, count))
            {
                Assert.Equal(points[i], pcoll.Points.ElementAt(i));
            }
        }

        [Fact]
        public void BuildPointCollectionFromPointCollectionTest()
        {
            int count = 5;
            var points = faker.Generate(count);
            var expected = new PointCollection().Add(points);

            var actual = new PointCollectionBuilder().AddPoints(expected)
                                                     .Build();

            Assert.Equal(expected.Count, actual.Count);
            foreach (int i in Enumerable.Range(0, count))
            {
                Assert.Equal(expected.Points.ElementAt(i), actual.Points.ElementAt(i));
            }
        }

        [Fact]
        public void BuildPointCollectionFromJsonTest()
        {
            int count = 5;
            var points = faker.Generate(count);

            var expected = new PointCollection().Add(points);
            var json = JsonConvert.SerializeObject(expected);

            var actual = new PointCollectionBuilder().AddPointsFromJson(json)
                                                     .Build();

            Assert.Equal(expected.Count, actual.Count);
            foreach (int i in Enumerable.Range(0, count))
            {
                Assert.Equal(expected.Points.ElementAt(i), actual.Points.ElementAt(i));
            }
        }
    }
}
