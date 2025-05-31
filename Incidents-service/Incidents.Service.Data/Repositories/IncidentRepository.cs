namespace Incidents.Service.Data.Repositories;

using Incidents.Service.Data.DataTransferObjects;

public class IncidentRepository : IIncidentRepository
{
    private readonly Dictionary<Guid, IncidentDto> incidents = [];

    public void AddOrUpdateIncident(IncidentDto incident) => incidents[incident.IncidentId] = incident;

    public IncidentDto? GetIncident(Guid id) => incidents.FirstOrDefault(kvp => kvp.Key == id).Value;

    public IEnumerable<IncidentDto> GetIncidents() => incidents.Values;
}
