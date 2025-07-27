namespace SampleWeb.IntegrationTests;

[CollectionDefinition(CollectionName)]
public class AspireCollection : ICollectionFixture<AspireFixture>
{
    public const string CollectionName = nameof(AspireCollection);
}
