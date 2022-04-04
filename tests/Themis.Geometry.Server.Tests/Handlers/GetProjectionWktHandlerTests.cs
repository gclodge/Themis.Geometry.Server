using System.Threading;

using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Handlers;
using Themis.Geometry.Server.Services;

using Bogus;
using Xunit;
using Assert = Xunit.Assert;

namespace Themis.Geometry.Server.Tests.Handlers
{
    public class GetProjectionWktHandlerTests
    {
        private readonly Faker faker;

        public GetProjectionWktHandlerTests()
        {
            faker = new();
        }

        [Fact]
        public void GetProjectionServiceWktTest()
        {
            string expected = faker.Lorem.Sentence(3);

            var opt = Helper.GetMockProjectionConfigOptions(wkt: expected);
            var proj = new ProjectionService(opt);

            var command = new GetProjectionWktCommand(ref Helper.Lock);
            var handler = new GetProjectionWktHandler(proj);
            var actual = handler.Handle(command, CancellationToken.None).Result;

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
    }
}
