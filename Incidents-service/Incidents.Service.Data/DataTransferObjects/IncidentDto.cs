namespace Incidents.Service.Data.DataTransferObjects;

using Incidents.Service.Core.Enums;

public class IncidentDto(Guid incidentId, DateTime incidentTimestamp, IncidentSeverity incidentSeverity = IncidentSeverity.Minor, string? incidentDescription = null)
{
    public readonly Guid IncidentId = incidentId;
    public readonly DateTime IncidentTimestamp = incidentTimestamp;
    public readonly IncidentSeverity IncidentSeverity = incidentSeverity;
    public readonly string? IncidentDescription = incidentDescription;
}
