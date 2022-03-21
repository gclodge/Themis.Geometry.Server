using System.Diagnostics.CodeAnalysis;

using Themis.Geometry.Server.Services;
using Themis.Geometry.Server.Services.Interfaces;

using MediatR;

namespace Themis.Geometry.Server.Registrations
{
    [ExcludeFromCodeCoverage]
    public class ServiceRegistration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            //< Add & Configure Singletons
            services.AddSingleton<IFileSystemService, FileSystemService>();
            services.AddOptions<PointProviderServiceConfig>()
                            .Bind(configuration.GetSection(PointProviderServiceConfig.Name))
                            .ValidateDataAnnotations();
            services.AddSingleton<IPointProviderService, PointProviderService>();

            //< Adding MediatR as the 'last' step
            services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}
