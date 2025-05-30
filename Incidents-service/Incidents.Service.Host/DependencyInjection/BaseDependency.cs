namespace Incidents.Service.Host.DependencyInjection;

using Incidents.Service.Core.Commands;
using Incidents.Service.Core.Queries;
using Incidents.Service.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

/// <summary>Class to register Application's base dependencies</summary>
public class BaseDependency
{
    /// <summary>Registers the dependencies.</summary>
    public static void RegisterDependencies(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IIncidentRepository, IncidentRepository>();

        serviceCollection.AddTransient<IQueryRunner, QueryRunner>();
        serviceCollection.AddTransient<ICommandRunner, CommandRunner>();
        serviceCollection.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        EndpointDependency.RegisterDependencies(serviceCollection);
    }
}
