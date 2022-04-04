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
        public bool IsGeographic => config.IS_GEOGRAPHIC;
        public bool IsProjected => !IsGeographic;

        public int? EpsgCode => config.EPSG_CODE;

        public string? WellKnownText => config.WELL_KNOWN_TEXT;

        private readonly ProjectionServiceConfig config;

        public ProjectionService(IOptions<ProjectionServiceConfig> cfg)
        {
            config = ParseConfiguration(cfg);
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
                throw;
            }
        }

        public ITypeMath<double> GetTypeMath()
        {
            return IsGeographic ? new GeographicMath() : new DoubleMath();
        }
    }
}
