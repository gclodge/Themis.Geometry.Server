using System.IO;

using Microsoft.Extensions.Options;

using Themis.Geometry.Server.Services.Config;

using NSubstitute;

namespace Themis.Geometry.Server.Tests
{
    internal static class Helper
    {
        public static object Lock = new();

        internal static string GetTemporaryDirectoryName()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
        }

        internal static IOptions<ProjectionServiceConfig> GetMockProjectionConfigOptions(bool isGeographic = false, int? epsg = null, string? wkt = null)
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
    }
}
