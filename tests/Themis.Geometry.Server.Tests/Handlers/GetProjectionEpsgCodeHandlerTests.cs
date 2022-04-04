using System.Threading;

using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Handlers;
using Themis.Geometry.Server.Services;

using Bogus;
using Xunit;
using Assert = Xunit.Assert;

namespace Themis.Geometry.Server.Tests.Handlers
{
    public class GetProjectionEpsgCodeHandlerTests
    {
        private readonly Faker faker;

        public GetProjectionEpsgCodeHandlerTests()
        {
            faker = new();
        }

        [Fact]
        public void GetProjectionServiceEpsgCodeTest()
        {
            int expected = faker.Random.Int(0, 5000);

            var opt = Helper.GetMockProjectionConfigOptions(epsg: expected);
            var proj = new ProjectionService(opt);

            var command = new GetProjectionEpsgCodeCommand(ref Helper.Lock);
            var handler = new GetProjectionEpsgCodeHandler(proj);
            var actual = handler.Handle(command, CancellationToken.None).Result;

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
    }
}
