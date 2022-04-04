using Themis.Geometry.Server.Services.Config;
using Themis.Geometry.Server.Services.Interfaces;

using Themis.Geometry.Index.KdTree.TypeMath;
using Themis.Geometry.Index.KdTree.TypeMath.Interfaces;

using Microsoft.Extensions.Options;

using Serilog;

namespace Themis.Geometry.Server.Services
{
    public class ProjectionService : IProjectionService
    {
        public static readonly object ProjectionLock = new();

        public bool IsGeographic { get; private set; }
        public bool IsProjected => !IsGeographic;

        public int? EpsgCode { get; private set; }

        public string? WellKnownText { get; private set; }

        private readonly ProjectionServiceConfig config;

        public ProjectionService(IOptions<ProjectionServiceConfig> cfg)
        {
            config = ParseConfiguration(cfg);

            EpsgCode = config.EPSG_CODE;
            IsGeographic = config.IS_GEOGRAPHIC;
            WellKnownText = config.WELL_KNOWN_TEXT;
        }

        /// <summary>
        /// Attempt to parse the ProjectionServiceConfig as parsed from environment variable configuration
        /// </summary>
        /// <param name="opt">IOptions&lt;ProjectionServiceConfig&gt; containing available configuration</param>
        /// <returns>Fully composed ProjectionServiceConfig as configured</returns>
        static ProjectionServiceConfig ParseConfiguration(IOptions<ProjectionServiceConfig> opt)
        {
            ProjectionServiceConfig cfg;
            try
            {
                cfg = opt.Value;
                return cfg;
            }
            catch (OptionsValidationException ex)
            {
                foreach (var failure in ex.Failures) Log.Error(failure);
                throw new AppException("Failed to parse ProjectionServiceConfig", ex);
            }
        }

        public void SetEpsgCode(int epsgCode)
        {
            EpsgCode = epsgCode;
        }

        public void SetWellKnownText(string wkt)
        {
            WellKnownText = wkt;
        }

        public ITypeMath<double> GetTypeMath()
        {
            return IsGeographic ? new GeographicMath() : new DoubleMath();
        }
    }
}
