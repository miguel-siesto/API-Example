namespace Incidents.Service.Core.Queries;

using System.Threading.Tasks;
using System.Threading;

/// <summary>This interface defines a query runner.</summary>
public interface IQueryRunner
{
    /// <summary>
    /// This method is called to execute a query.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the query response model.</typeparam>
    /// <param name="query">The query to execute.</param>
    /// <param name="cancellationToken">The cancellation token of the query.</param>
    /// <returns>The query response.</returns>
    Task<TResult?> RunAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken);
}