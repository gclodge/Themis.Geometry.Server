using Themis.Geometry.Server.HealthChecks;
using Themis.Geometry.Server.Services.Interfaces;

namespace Themis.Geometry.Server.Services
{
    public class StartupBackgroundService : BackgroundService
    {
        private readonly IPointProviderService pps;
        private readonly StartupHealthCheck healthCheck;

        public StartupBackgroundService(StartupHealthCheck healthCheck, IPointProviderService pps)
        {
            this.pps = pps;
            this.healthCheck = healthCheck;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //< Load any existing data into IPointProviderService
            pps.LoadExistingData();

            //< Set the readiness check as 'healthy'
            healthCheck.StartupCompleted = true;

            return Task.CompletedTask;
        }
    }
}
