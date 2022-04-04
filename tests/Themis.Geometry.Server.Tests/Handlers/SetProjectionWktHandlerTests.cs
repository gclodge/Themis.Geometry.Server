using System.Threading;

using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Handlers;
using Themis.Geometry.Server.Services;

using Bogus;
using Xunit;
using Assert = Xunit.Assert;

namespace Themis.Geometry.Server.Tests.Handlers
{
    public class SetProjectionWktHandlerTests
    {
        private readonly Faker faker;

        public SetProjectionWktHandlerTests()
        {
            faker = new();
        }

        [Fact]
        public void UpdateProjectionServiceWktTest()
        {
            string expected = faker.Lorem.Sentence(3);

            var opt = Helper.GetMockProjectionConfigOptions();
            var proj = new ProjectionService(opt);

            var command = new SetProjectionWktCommand(expected, ref Helper.Lock);
            var handler = new SetProjectionWktHandler(proj);
            handler.Handle(command, CancellationToken.None);

            Assert.Equal(expected, proj.WellKnownText);
        }
    }
}
