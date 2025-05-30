using Incidents.Service.Core.Commands;
using Incidents.Service.Core.Queries;
using Incidents.Service.Data.DataTransferObjects;
using Incidents.Service.Logic.Commands.CreateIncident;
using Incidents.Service.Logic.Queries.GetAllIncidents;
using Incidents.Service.Logic.Queries.GetIncidentById;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Incidents.Service.Host.DependencyInjection;

/// <summary><para>Class to inject Endpoint related dependencies.</para></summary>
public class EndpointDependency
{
    /// <summary>Registers endpoint dependencies.</summary>
    /// <param name="serviceCollection">The service collection.</param>
    public static void RegisterDependencies(IServiceCollection serviceCollection)
    {
        RegisterIncidentDependencies(serviceCollection);

        serviceCollection.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }

    private static void RegisterIncidentDependencies(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IQueryHandler<GetIncidentByIdQuery, IncidentDto>, GetIncidentByIdQueryHandler>();
        serviceCollection.AddTransient<IQueryHandler<GetAllIncidentsQuery, IEnumerable<IncidentDto>>, GetAllIncidentsQueryHandler>();
        serviceCollection.AddTransient<ICommandHandler<CreateIncidentCommand>, CreateIncidentCommandHandler>();
    }
}
