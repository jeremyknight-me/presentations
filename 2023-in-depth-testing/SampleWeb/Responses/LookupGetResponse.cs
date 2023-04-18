namespace SampleWeb.Responses;

public sealed class LookupGetResponse
{
	public required int Id { get; init; }
	public required string Name { get; init; }
	public required bool IsDeleted { get; init; }
}
