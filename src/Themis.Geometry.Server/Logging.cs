using System.Diagnostics.CodeAnalysis;

using Serilog;

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
    }
}
