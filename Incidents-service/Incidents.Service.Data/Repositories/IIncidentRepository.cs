namespace Incidents.Service.Data.Repositories;

using Incidents.Service.Data.DataTransferObjects;

public interface IIncidentRepository
{
    /// <summary>Get all incidents from the repository.</summary>
    IEnumerable<IncidentDto> GetIncidents();

    /// <summary>Get incident by id from the repository.</summary>
    IncidentDto? GetIncident(Guid id);

    /// <summary>Add an incident to the repository.</summary>
    void AddIncident(IncidentDto incident);
}
