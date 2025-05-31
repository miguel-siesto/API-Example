using Incidents.Service.Core.Enums;

namespace Incidents.Service.Data.DataTransferObjects;

public class IncidentDto(Guid incidentId, DateTime timestamp, IncidentSeverity severity = IncidentSeverity.Minor, string? description = null)
{
    public readonly Guid IncidentId = incidentId;
    public readonly DateTime Timestamp = timestamp;
    public readonly IncidentSeverity Severity = severity;
    public readonly string? Description = description;
}
