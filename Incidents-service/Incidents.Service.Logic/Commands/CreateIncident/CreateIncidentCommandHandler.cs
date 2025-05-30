using Incidents.Service.Core.Commands;
using Incidents.Service.Data.Repositories;

namespace Incidents.Service.Logic.Commands.CreateIncident;

public class CreateIncidentCommandHandler(IIncidentRepository incidentRepository) : ICommandHandler<CreateIncidentCommand>
{
    private readonly TimeSpan timeoutAfter = TimeSpan.FromMilliseconds(3000);

    /// <inheritdoc/>
    public TimeSpan TimeoutAfter { get => this.timeoutAfter; }

    public Task ExecuteAsync(CreateIncidentCommand command, CancellationToken cancellationToken)
    {
        incidentRepository.AddIncident(command.ToIncidentDto());

        return Task.CompletedTask;
    }
}
