using Xunit;

namespace CleanArchitecture.Testing.IntegrationTests.Utils;

#pragma warning disable CA1711 // Should not end with Collection
[CollectionDefinition(nameof(IntegrationTestCollection))]
public class IntegrationTestCollection : ICollectionFixture<IntegrationTestFixture>
{
    /*
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
    */
}
#pragma warning restore CA1711
