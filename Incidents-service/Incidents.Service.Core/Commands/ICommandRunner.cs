namespace Incidents.Service.Core.Commands;

using System.Threading.Tasks;
using System.Threading;

/// <summary> Command runner interface. </summary>
public interface ICommandRunner
{
    /// <summary> Run the command. </summary>
    /// <param name="command"> The command to be executed. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns>The command execution Task. </returns>

    Task RunAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default);
}
