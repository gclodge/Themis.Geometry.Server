using Themis.Geometry.Server;
using Themis.Geometry.Server.Services;
using Themis.Geometry.Server.Services.Interfaces;

using MediatR;
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
    builder.Services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());
    //< TODO :: Configure IPointProviderService w/ IOptions
    builder.Services.AddSingleton<IPointProviderService, PointProviderService>();

    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    //< Chuck them environment variables up in this
    builder.Configuration.AddEnvironmentVariables();
    builder.Configuration.AddEnvironmentVariables(Constants.ENV_VAR_PREFIX);

    //< TODO :: Actually implement PointProviderService..
    //< TODO :: Tests, you animal

    var app = builder.Build();

    app.UseSerilogRequestLogging();

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