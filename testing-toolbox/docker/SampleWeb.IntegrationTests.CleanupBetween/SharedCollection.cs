namespace SampleWeb.IntegrationTests.CleanupBetween;

[CollectionDefinition(CollectionName)]
public class SharedCollection : ICollectionFixture<SampleWebApiFactory>
{
    public const string CollectionName = nameof(SharedCollection);
}
