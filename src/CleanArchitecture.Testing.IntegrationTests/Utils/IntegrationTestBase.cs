using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Testing.IntegrationTests.Utils.Logging;
using Xunit.Abstractions;

namespace CleanArchitecture.Testing.IntegrationTests.Utils;

public abstract class IntegrationTestBase : IDisposable
{
    private readonly IServiceScope _scope;

    protected IntegrationTestBase(IntegrationTestFixture fixture, ITestOutputHelper output)
    {
        Fixture = fixture;
        Http = fixture.App.CreateClient();
        _scope = fixture.App.Services.CreateScope();
        TestOutputHelperResolver.Instance.TestOutput = output;

        // This should be set for each individual test run, if you use a real database, the data can be flushed with: https://github.com/jbogard/Respawn
        Services.GetRequiredService<AppDbContext>().Database.EnsureDeleted();
    }

    public IntegrationTestFixture Fixture { get; }

    public HttpClient Http { get; }

    public IServiceProvider Services => _scope.ServiceProvider;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            TestOutputHelperResolver.Instance.TestOutput = null;
            Http.Dispose();
            _scope.Dispose();
        }
    }
}
