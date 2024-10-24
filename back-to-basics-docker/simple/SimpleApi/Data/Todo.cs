namespace SimpleApi.Data;

public sealed class Todo
{
    public int Id { get; private set; }
    public string Text { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
