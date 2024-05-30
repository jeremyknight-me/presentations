using DataPersistence;

namespace SampleWeb.Endpoints.Lookups;

public sealed class LookupResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required bool IsDeleted { get; init; }

    public static LookupResponse Create(Lookup lookup)
        => new()
        {
            Id = lookup.Id,
            Name = lookup.Name,
            IsDeleted = lookup.IsDeleted
        };
}
