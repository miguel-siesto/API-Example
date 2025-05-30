using Incidents.Service.Core.Commands;
using Incidents.Service.Core.Enums;
using Incidents.Service.Data.DataTransferObjects;

namespace Incidents.Service.Logic.Commands.CreateIncident;

public class CreateIncidentCommand : ICommand
{
    public Guid IncidentId { get; set; }
    public DateTime IncidentTimestamp { get; set; }
    public IncidentSeverity IncidentSeverity { get; set; }
    public string? IncidentDescription { get; set; }

    public IncidentDto ToIncidentDto() => new(IncidentId, IncidentTimestamp, IncidentSeverity, IncidentDescription);
}
