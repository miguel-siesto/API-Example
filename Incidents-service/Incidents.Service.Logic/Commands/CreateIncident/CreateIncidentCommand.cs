namespace Incidents.Service.Logic.Commands.CreateIncident;

using Incidents.Service.Core.Commands;
using Incidents.Service.Core.Enums;
using Incidents.Service.Data.DataTransferObjects;

public class CreateIncidentCommand : ICommand
{
    public Guid IncidentId { get; set; }
    public DateTime Timestamp { get; set; }
    public IncidentSeverity Severity { get; set; }
    public string? Description { get; set; }

    public IncidentDto ToIncidentDto() => new(IncidentId, Timestamp, Severity, Description);
}
