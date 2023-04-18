namespace DataPersistence;

public sealed class Lookup
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public bool IsDeleted { get; set; }
}
