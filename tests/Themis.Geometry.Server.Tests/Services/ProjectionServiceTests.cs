using Themis.Geometry.Server.Services;

using Themis.Geometry.Index.KdTree.TypeMath;

using Bogus;
using Xunit;
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

        [Fact]
        public void CreateWithConfigurationFromOptionsTest()
        {
            bool expectedGeographic = faker.Random.Bool();
            int expectedEpsg = faker.Random.Int(0, 10000);
            string expectedWkt = faker.Lorem.Sentence(4);

            var opt = Helper.GetMockProjectionConfigOptions(expectedGeographic, expectedEpsg, expectedWkt);
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

            var opt = Helper.GetMockProjectionConfigOptions(expected);
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

            var opt = Helper.GetMockProjectionConfigOptions(expected);
            var proj = new ProjectionService(opt);
            var math = proj.GetTypeMath();

            Assert.Equal(expected, proj.IsGeographic);
            Assert.Equal(!expected, proj.IsProjected);

            Assert.IsType<DoubleMath>(math);
        }
    }
}
