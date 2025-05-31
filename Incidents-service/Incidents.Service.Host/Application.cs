using Incidents.Service.Host.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Incidents.Service.Host;

/// <summary>    
/// This is the root of this service.    
/// </summary>    
public static class Application
{
    private const string applicationName = "Incidents Service";

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

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(ApiInfo?.Version, ApiInfo);
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Incident API v1");
            c.RoutePrefix = string.Empty; // Makes Swagger available at https://localhost:8082/
        });

        app.UseHttpsRedirection();
        app.UseCors();
        // TODO: Add authentication if needed.    
        //app.UseAuthentication();
        app.UseAuthorization();

        app.UseRouting();
        app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });

        return app;
    }
}
