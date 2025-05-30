namespace Incidents.Service.Core.Queries;

using System.Threading.Tasks;
using System.Threading;
using System;

/// inheritdoc
public class QueryRunner(IServiceProvider serviceProvider) : IQueryRunner
{
    /// inheritdoc
    public async Task<TResult?> RunAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
    {
        if (serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>)) is not IQueryHandler<TQuery, TResult> handler)
        {
            throw new InvalidOperationException(
                $"No handler registered for query type {typeof(TQuery).Name}");
        }

        return await handler.ExecuteAsync(query, cancellationToken);
    }
}
