using System.Threading.Tasks;
using System.Threading;
using System;

namespace Incidents.Service.Core.Commands;

/// inheritdoc
public class CommandRunner(IServiceProvider serviceProvider) : ICommandRunner
{
    /// inheritdoc
    public async Task RunAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
    {
        if (serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) is not ICommandHandler<TCommand> handler)
        {
            throw new InvalidOperationException(
                $"No handler registered for query type {typeof(TCommand).Name}");
        }

        await handler.ExecuteAsync(command, cancellationToken);
    }
}
