namespace Examples.Data;

public class Blog
{
    public int Id { get; private set; }
    public BlogHeader? Header { get; set; }
    public ICollection<Post> Posts { get; } = new List<Post>();
}

public class BlogHeader
{
    public int Id { get; private set; }
    public Blog? Blog { get; set; }
}

public class Post
{
    public int Id { get; private set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
    public List<Tag> Tags { get; } = new();
}

public class Tag
{
    public int Id { get; private set; }
    public List<Post> Posts { get; } = new();
}
