namespace Incidents.Service.Core.Queries;

using System.Threading.Tasks;
using System.Threading;
using System;

/// <summary>
///   <para>Interface to implement Query Handler.</para>
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TResult">The type of the response.</typeparam>
public interface IQueryHandler<TQuery, TResult>
{
    /// <summary>
    /// Gets this is the duration after which a timeout should occur.
    /// </summary>
    TimeSpan TimeoutAfter { get; }

    /// <summary>Method to handle the query to be executed.</summary>
    /// <param name="query">The query type.</param>
    /// <param name="cancellationToken">The cancellation token to abort the request.</param>
    /// <returns>A <see cref="Task{TResult}" /> representing the result of the asynchronous operation.</returns>
    Task<TResult?> ExecuteAsync(TQuery query, CancellationToken cancellationToken);
}