namespace SampleWeb.Persistence;

public sealed class Lookup
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
    public bool IsDeleted { get; set; } = false;

    public static Lookup Create(string name)
        => new()
        {
            Name = name,
            IsDeleted = false
        };

}
