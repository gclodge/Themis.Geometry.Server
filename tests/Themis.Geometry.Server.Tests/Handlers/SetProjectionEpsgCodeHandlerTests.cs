using System.Threading;

using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Handlers;
using Themis.Geometry.Server.Services;

using Bogus;
using Xunit;
using Assert = Xunit.Assert;

namespace Themis.Geometry.Server.Tests.Handlers
{
    public class SetProjectionEpsgCodeHandlerTests
    {
        private readonly Faker faker;

        public SetProjectionEpsgCodeHandlerTests()
        {
            faker = new();
        }

        [Fact]
        public void UpdateProjectionServiceEpsgCodeTest()
        {
            int expected = faker.Random.Int(0, 5000);

            var opt = Helper.GetMockProjectionConfigOptions();
            var proj = new ProjectionService(opt);

            var command = new SetProjectionEpsgCodeCommand(expected, ref Helper.Lock);
            var handler = new SetProjectionEpsgCodeHandler(proj);
            handler.Handle(command, CancellationToken.None);

            Assert.Equal(expected, proj.EpsgCode);
        }
    }
}
