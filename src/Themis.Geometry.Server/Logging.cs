using System.Diagnostics.CodeAnalysis;

using Serilog;
using Serilog.Events;

namespace Themis.Geometry.Server
{
    [ExcludeFromCodeCoverage]
    public static class Logging
    {
        public static Serilog.ILogger GetBootstrap()
        {
            return new LoggerConfiguration()
                        .WriteTo.Console()
                        .CreateBootstrapLogger();
        }

        public static Serilog.ILogger GetLogger(bool isDevelopment)
        {
            var config = new LoggerConfiguration()
                              .MinimumLevel.Information()
                              .Enrich.FromLogContext()
                              .WriteTo.Console();

            config.MinimumLevel.Override("Microsoft", isDevelopment ? LogEventLevel.Debug : LogEventLevel.Information);

            return config.CreateLogger();
        }
    }
}
