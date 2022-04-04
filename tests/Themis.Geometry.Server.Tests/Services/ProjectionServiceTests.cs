using Microsoft.Extensions.Options;

using Themis.Geometry.Server.Services;
using Themis.Geometry.Server.Services.Config;

using Themis.Geometry.Index.KdTree.TypeMath;

using Bogus;
using Xunit;
using NSubstitute;
using Assert = Xunit.Assert;

namespace Themis.Geometry.Server.Tests.Services
{
    public class ProjectionServiceTests
    {
        private readonly Faker faker;

        public ProjectionServiceTests()
        {
            faker = new();
        }

        static IOptions<ProjectionServiceConfig> GetMockOptions(bool isGeographic = false, int? epsg = null, string? wkt = null)
        {
            var cfg = new ProjectionServiceConfig
            {
                IS_GEOGRAPHIC = isGeographic,
                EPSG_CODE = epsg,
                WELL_KNOWN_TEXT = wkt
            };

            var configMock = Substitute.For<IOptions<ProjectionServiceConfig>>();
            configMock.Value.Returns(cfg);

            return configMock;
        }

        [Fact]
        public void CreateWithConfigurationFromOptionsTest()
        {
            bool expectedGeographic = faker.Random.Bool();
            int expectedEpsg = faker.Random.Int(0, 10000);
            string expectedWkt = faker.Lorem.Sentence(4);

            var opt = GetMockOptions(expectedGeographic, expectedEpsg, expectedWkt);
            var proj = new ProjectionService(opt);
            
            Assert.Equal(expectedGeographic, proj.IsGeographic);
            Assert.Equal(!expectedGeographic, proj.IsProjected);
            Assert.Equal(expectedEpsg, proj.EpsgCode);
            Assert.Equal(expectedWkt, proj.WellKnownText);
        }

        [Fact]
        public void GetTypeMathGeographicTest()
        {
            bool expected = true;

            var opt = GetMockOptions(expected);
            var proj = new ProjectionService(opt);
            var math = proj.GetTypeMath();

            Assert.Equal(expected, proj.IsGeographic);
            Assert.Equal(!expected, proj.IsProjected);

            Assert.IsType<GeographicMath>(math);
        }

        [Fact]
        public void GetTypeMathProjectedTest()
        {
            bool expected = false;

            var opt = GetMockOptions(expected);
            var proj = new ProjectionService(opt);
            var math = proj.GetTypeMath();

            Assert.Equal(expected, proj.IsGeographic);
            Assert.Equal(!expected, proj.IsProjected);

            Assert.IsType<DoubleMath>(math);
        }
    }
}
