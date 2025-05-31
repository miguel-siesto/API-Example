using Incidents.Service.Core.Enums;

namespace Incidents.Service.Data.DataTransferObjects;

public class IncidentDto(Guid incidentId, DateTime timestamp, IncidentSeverity severity = IncidentSeverity.Minor, string? description = null)
{
    public Guid IncidentId { get; set; } = incidentId;
    public DateTime Timestamp { get; set; } = timestamp;
    public IncidentSeverity Severity { get; set; } = severity;
    public string? Description { get; set; } = description;
}
