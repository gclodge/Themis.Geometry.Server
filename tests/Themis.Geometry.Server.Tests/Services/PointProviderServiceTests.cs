using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Extensions.Options;

using Themis.Geometry.Server.Services;
using Themis.Geometry.Server.Services.Interfaces;
using Themis.Geometry.Server.Models.Points;

using Bogus;
using Xunit;
using NSubstitute;
using Assert = Xunit.Assert;

namespace Themis.Geometry.Server.Tests.Services
{
    public class PointProviderServiceTests
    {
        //< (0,0),(-1,0),(1,0),(-1,1),(1,1),(-1,-1),(1,-1)
        private static readonly string FixtureDir = Path.Combine(".", "Fixtures");
        private static readonly string PointFile = Path.Combine(FixtureDir, "Points.json");

        private const int PointFileAttributes = 1;
        private const int PointFileDimensions = 2;
        private const int PointFileCount = 7;

        private readonly Faker faker;
        private readonly Faker<Point> pointFaker;
        private readonly IFileSystemService fss;

        public PointProviderServiceTests()
        {
            this.faker = new();
            this.pointFaker = new Faker<Point>()
                .RuleFor(p => p.Position, f => f.GenerateRandomVector(PointFileDimensions))
                .RuleFor(p => p.Attributes, f => f.GenerateRandomDictionary(PointFileAttributes));

            this.fss = new FileSystemService();
        }

        static IOptions<PointProviderServiceConfig> GetMockOptions(int dimensions = 2, string? file = null)
        {
            var cfg = new PointProviderServiceConfig
            {
                DIMENSIONS = dimensions,
                POINT_DATA_FILE = file
            };

            var configMock = Substitute.For<IOptions<PointProviderServiceConfig>>();
            configMock.Value.Returns(cfg);
            return configMock;
        }

        [Fact]
        public void CreateWithConfigurableDimensionalityTest()
        {
            int expectedCount = 0;
            int expectedDims = faker.Random.Int(min: 2, max: 10);

            var opt = GetMockOptions(expectedDims);
            var pps = new PointProviderService(fss, opt);

            Assert.Equal(expectedCount, pps.Count);
            Assert.Equal(expectedDims, pps.Dimensionality);
        }

        [Fact]
        public void CreateWithDataFromExistingFileTest()
        {
            var opt = GetMockOptions(PointFileDimensions, PointFile);
            var pps = new PointProviderService(fss, opt).LoadExistingData();

            Assert.Equal(PointFileCount, pps.Count);
            Assert.Equal(PointFileDimensions, pps.Dimensionality);
        }

        [Fact]
        public void CreateWithNonExistentFileStillEmptyTest()
        {
            var file = Path.Combine(Path.GetTempPath(), faker.Lorem.Word() + "-unique");
            var opt = GetMockOptions(PointFileDimensions, file);
            var pps = new PointProviderService(fss, opt).LoadExistingData();

            Assert.Equal(0, pps.Count);
            Assert.Equal(PointFileDimensions, pps.Dimensionality);
        }

        [Fact]
        public void AddPointToIndexTest()
        {
            var opt = GetMockOptions(PointFileDimensions);
            var point = pointFaker.Generate();

            var pps = new PointProviderService(fss, opt).Add(point);

            Assert.Equal(1, pps.Count);
        }

        [Fact]
        public void AddPointsToIndexTest()
        {
            int expectedCount = 5;

            var opt = GetMockOptions(PointFileDimensions);
            var points = pointFaker.Generate(expectedCount);

            var pps = new PointProviderService(fss, opt).Add(points);

            Assert.Equal(expectedCount, pps.Count);
        }

        [Fact]
        public void RemovePointFromIndexTest()
        {
            var opt = GetMockOptions(PointFileDimensions);
            var point = pointFaker.Generate();

            var pps = new PointProviderService(fss, opt).Add(point).Remove(point);

            Assert.Equal(0, pps.Count);
        }

        [Fact]
        public void RemovePointsFromIndexTest()
        {
            int count = 5;

            var opt = GetMockOptions(PointFileDimensions);
            var points = pointFaker.Generate(count);

            var pps = new PointProviderService(fss, opt).Add(points).Remove(points);

            Assert.Equal(0, pps.Count);
        }

        [Fact]
        public void GetNearestPointTest()
        {
            var opt = GetMockOptions(PointFileDimensions, PointFile);
            var pps = new PointProviderService(fss, opt).LoadExistingData();

            var pcoll = new PointCollection().AddFromJson(fss.ReadFileContents(PointFile));

            var poi = faker.PickRandom(pcoll.Points);

            var nearest = pps.GetNearest(poi);

            Assert.Equal(poi, nearest);
        }

        [Fact]
        public void GetNearestNPointsTest()
        {
            int expected = 3;

            var opt = GetMockOptions(PointFileDimensions, PointFile);
            var pps = new PointProviderService(fss, opt).LoadExistingData();

            var pcoll = new PointCollection().AddFromJson(fss.ReadFileContents(PointFile));

            var poi = faker.PickRandom(pcoll.Points);

            var nearest = pps.GetNearest(poi, expected);

            Assert.Equal(3, nearest.Count());
            Assert.Contains(poi, nearest);
        }

        [Fact]
        public void GetAllWithinTest()
        {
            int expected = 3;
            double searchDist = 1.0;

            var opt = GetMockOptions(PointFileDimensions, PointFile);
            var pps = new PointProviderService(fss, opt).LoadExistingData();

            var orig = Point.Create(new[] { 0.0, 0.0 });
            var poi = pps.GetNearest(orig);

            //< Expect to only get (0,0),(1,0),(-1,0) back
            var nearby = pps.GetAllWithin(poi, searchDist);

            Assert.Equal(expected, nearby.Count());
            Assert.Contains(poi, nearby);
        }
    }
}
