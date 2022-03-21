using System.Diagnostics.CodeAnalysis;

using Themis.Geometry.Server.HealthChecks;
using Themis.Geometry.Server.Services;

namespace Themis.Geometry.Server.Registrations
{
    [ExcludeFromCodeCoverage]
    public class HealthCheckRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddHostedService<StartupBackgroundService>();
            services.AddSingleton<StartupHealthCheck>();
            services.AddHealthChecks()
                            .AddCheck<StartupHealthCheck>("Startup", tags: new[] { "ready" });
        }
    }
}
