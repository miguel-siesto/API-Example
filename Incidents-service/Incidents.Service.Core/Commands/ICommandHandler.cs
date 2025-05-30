using System;
using System.Threading;
using System.Threading.Tasks;

namespace Incidents.Service.Core.Commands;

/// <summary>
///   <para>Interface to implement Command Handler.</para>
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public interface ICommandHandler<TCommand>
{
    /// <summary>
    /// Gets this is the duration after which a timeout should occur.
    /// </summary>
    TimeSpan TimeoutAfter { get; }

    /// <summary>Method to handle the command to be executed.</summary>
    /// <param name="command">The command type.</param>
    /// <param name="cancellationToken">The cancellation token to abort the request.</param>
    /// <returns>A <see cref="Task{TResult}" /> representing the result of the asynchronous operation.</returns>
    Task ExecuteAsync(TCommand command, CancellationToken cancellationToken);
}
