using Incidents.Service.Host.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Incidents.Service.Host;

/// <summary>    
/// This is the root of this service.    
/// </summary>    
public static class Application
{
    private const string applicationName = "incidents-service";
    private static Action<WebApplicationBuilder>? _customConfigHook;

    /// <summary>    
    /// Test usage method to inject configuration for the application.    
    /// </summary>    
    public static void UseCustomConfiguration(Action<WebApplicationBuilder> configHook)
    {
        _customConfigHook = configHook;
    }

    /// <summary>    
    /// This is the OpenAPI information for the service.    
    /// </summary>    
    public static OpenApiInfo? ApiInfo { get; } = new OpenApiInfo
    {
        Version = "v1",
        Title = applicationName,
        Description = "Microservice to manage incidents.",
        Contact = new OpenApiContact
        {
            Name = "Miguel Siesto",
            Url = new Uri("https://github.com/miguel-siesto")
        }
    };

    /// <summary>    
    /// Build start point for this application.    
    /// </summary>    
    /// <param name="builder"></param>    
    /// <returns></returns>    
    /// <exception cref="Exception"></exception>    
    public static WebApplication BuildApp(WebApplicationBuilder builder)
    {
        BaseDependency.RegisterDependencies(builder.Services);
        builder.Services.AddLogging();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc(ApiInfo?.Version, ApiInfo);
            o.EnableAnnotations();
        }); // Ensure Swagger services are added    

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        var app = builder.Build();

        // Configure HTTP request pipeline    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            // Enable Swagger middleware
            app.UseSwagger();

            // Configure Swagger UI
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApiInfo?.Title} - {ApiInfo?.Version}");
                options.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root (e.g., https://localhost:8082)
                options.DocumentTitle = ApiInfo?.Title;
                options.DisplayRequestDuration();
            });
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseRouting();

        // TODO: Add authentication and authorization if needed.    
        //app.UseAuthentication();    
        //app.UseAuthorization();

        app.Urls.Clear();
        app.Urls.Add("https://localhost:8082");

        app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });

        return app;
    }
}
