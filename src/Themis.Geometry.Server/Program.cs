using Microsoft.AspNetCore.Diagnostics.HealthChecks;

using Themis.Geometry.Server;
using Themis.Geometry.Server.Registrations;

using Serilog;

Log.Logger = Logging.GetBootstrap();
Log.Information($" -- Starting up -- ");

try
{
    var builder = WebApplication.CreateBuilder(args);

    //< Configure Serilog from appsettings.json
    builder.Host.UseSerilog((ctx, lc) => lc
           .WriteTo.Console()
           .ReadFrom.Configuration(ctx.Configuration));

    //< Add services to the DI container
    ServiceRegistration.Register(builder.Services, builder.Configuration);
    HealthCheckRegistration.Register(builder.Services);

    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    //< Chuck them environment variables up in this
    builder.Configuration.AddEnvironmentVariables();
    builder.Configuration.AddEnvironmentVariables(Constants.ENV_VAR_PREFIX);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    //< Map health check endpoints based on readiness as determined by StartupHealthCheck
    app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
    {
        Predicate = healthCheck => healthCheck.Tags.Contains("ready")
    });
    app.MapHealthChecks("/healthz/live", new HealthCheckOptions
    {
        Predicate = _ => false
    });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception encountered!");
}
finally
{
    Log.Information(" -- Shut Down Complete --");
    Log.CloseAndFlush();
}