using Incidents.Service.Data.DataTransferObjects;
using System.Collections.Concurrent;

namespace Incidents.Service.Data.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly ConcurrentDictionary<Guid, IncidentDto> _lockIncidents = [];

    protected IReadOnlyDictionary<Guid, IncidentDto> IncidentsCache => _lockIncidents;

    public void AddOrUpdateIncident(IncidentDto incident) => _lockIncidents[incident.IncidentId] = incident;

    public IncidentDto? GetIncident(Guid id) => IncidentsCache.FirstOrDefault(kvp => kvp.Key == id).Value;

    public IEnumerable<IncidentDto> GetIncidents() => IncidentsCache.Values;
}
