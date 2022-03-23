using System.Linq;

using Themis.Geometry.Server.Models.Points;

using Bogus;
using Xunit;
using Assert = Xunit.Assert;
using Newtonsoft.Json;

namespace Themis.Geometry.Server.Tests.Models.Points
{
    public class PointCollectionTests
    {
        const int Dimensions = 3;
        private Faker<Point> faker;

        public PointCollectionTests()
        {
            faker = new Faker<Point>()
                .RuleFor(p => p.Position, f => f.GenerateRandomVector(Dimensions))
                .RuleFor(p => p.Attributes, f => f.GenerateRandomDictionary(Dimensions));
        }
        
        [Fact]
        public void AddPointToCollectionTest()
        {
            var point = faker.Generate();

            var pcoll = new PointCollection().Add(point);

            Assert.Single(pcoll.Points);
            Assert.Equal(point, pcoll.Points.Single());
            Assert.Equal(1, pcoll.Count);
        }

        [Fact]
        public void AddEnumerableToCollectionTest()
        {
            int count = 5;
            var points = faker.Generate(count);

            var pcoll = new PointCollection().Add(points);

            Assert.Equal(count, pcoll.Count);
            foreach (int i in Enumerable.Range(0, count))
            {
                Assert.Equal(points[i], pcoll.Points.ElementAt(i));
            }
        }

        [Fact]
        public void AddPointCollectionToCollectionTest()
        {
            int count = 5;
            var pointsA = faker.Generate(count);
            var pointsB = faker.Generate(count);

            var pcollA = new PointCollection().Add(pointsA);
            var pcollB = new PointCollection().Add(pointsB).Add(pcollA);

            int expectedCountA = pointsA.Count();
            int expectedCountB = expectedCountA + pointsB.Count();

            Assert.Equal(expectedCountA, pcollA.Count);
            Assert.Equal(expectedCountB, pcollB.Count);
        }

        [Fact]
        public void AddFromJsonToCollectionTest()
        {
            int count = 5;
            var points = faker.Generate(count);

            var pcollA = new PointCollection().Add(points);
            var json = JsonConvert.SerializeObject(pcollA);

            var pcollB = JsonConvert.DeserializeObject<PointCollection>(json);
            pcollB ??= new PointCollection(); //< Should never happen - but RIP null reference warnings, nerds

            Assert.NotNull(pcollB);
            Assert.Equal(count, pcollB.Count);
            foreach (int i in Enumerable.Range(0, count))
            {
                Assert.Equal(pcollA.Points.ElementAt(i), pcollB.Points.ElementAt(i));
            }
        }
    }
}
