namespace Incidents.Service.Logic.Queries.GetIncidentById;

using Incidents.Service.Core.Queries;
using Incidents.Service.Data.DataTransferObjects;
using Incidents.Service.Data.Repositories;

public class GetIncidentByIdQueryHandler(IIncidentRepository incidentRepository) : IQueryHandler<GetIncidentByIdQuery, IncidentDto>
{
    private readonly TimeSpan timeoutAfter = TimeSpan.FromMilliseconds(3000);

    /// <inheritdoc/>
    public TimeSpan TimeoutAfter { get => this.timeoutAfter; }

    public Task<IncidentDto?> ExecuteAsync(GetIncidentByIdQuery query, CancellationToken cancellationToken)
    {
        var incident = incidentRepository.GetIncident(query.IncidentId) 
            ?? throw new HttpRequestException($"Incident with ID {query.IncidentId} not found.", null, System.Net.HttpStatusCode.NotFound);

        return Task.FromResult(incident ?? null);
    }
}
