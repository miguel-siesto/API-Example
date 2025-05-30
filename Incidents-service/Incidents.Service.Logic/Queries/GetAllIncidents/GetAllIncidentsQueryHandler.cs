namespace Incidents.Service.Logic.Queries.GetAllIncidents;

using Incidents.Service.Core.Queries;
using Incidents.Service.Data.DataTransferObjects;
using Incidents.Service.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GetAllIncidentsQueryHandler(IIncidentRepository incidentRepository) : IQueryHandler<GetAllIncidentsQuery, IEnumerable<IncidentDto>>
{
    private readonly TimeSpan timeoutAfter = TimeSpan.FromMilliseconds(3000);

    /// <inheritdoc/>
    public TimeSpan TimeoutAfter { get => this.timeoutAfter; }

    public Task<IEnumerable<IncidentDto>?> ExecuteAsync(GetAllIncidentsQuery query, CancellationToken cancellationToken)
    {
        var incidents = incidentRepository.GetIncidents();

        return Task.FromResult(incidents ?? null);
    }
}