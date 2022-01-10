using System.Diagnostics.CodeAnalysis;
using CleanArchitecture.Infrastructure.Persistence;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Threading;

namespace CleanArchitecture.Testing.IntegrationTests.Utils;

[UsedImplicitly]
[SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "We have no unmanaged resources")]
public class IntegrationTestFixture : System.IAsyncDisposable, IDisposable
{
    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "It gets disposed trough IAsyncDisposable")]
    public IntegrationTestFixture()
    {
        App = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    // Add ApplicationDbContext using an in-memory database for testing.
                    services.ReplaceDbContext<AppDbContext>(options =>
                   {
                       options.UseInMemoryDatabase(databaseName: "CleanArchitecture");
                    });
                });
            });
    }

    internal WebApplicationFactory<Program> App { get; }

    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "It gets disposed trough IAsyncDisposable")]
    public void Dispose()
    {
        using var context = new JoinableTaskContext();
        var jtf = new JoinableTaskFactory(context);
        jtf.Run(async () => await DisposeAsync());
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        await App.DisposeAsync();
    }
}
