
using Microsoft.AspNetCore.Builder;
using System.Threading;
using System.Threading.Tasks;

namespace Incidents.Service.Host;

/// <summary>
/// This is the root of this service.
/// </summary>
public class Program
{
    /// <summary>
    /// This is a static entry point to this application.
    /// </summary>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ThreadPool.SetMinThreads(5, 20);

        var app = Application.BuildApp(builder);
        await app.RunAsync();
    }
}