using Microsoft.EntityFrameworkCore;

namespace Examples.Data;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public DbSet<Blog> Blogs => this.Set<Blog>();
    public DbSet<BlogHeader> BlogHeaders => this.Set<BlogHeader>();
    public DbSet<Post> Posts => this.Set<Post>();
    public DbSet<Tag> Tags => this.Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1:M
        modelBuilder.Entity<Blog>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.Blog)
            .HasForeignKey(e => e.BlogId)
            .IsRequired();

        // 1:1
        modelBuilder.Entity<Blog>()
            .HasOne(e => e.Header)
            .WithOne(e => e.Blog)
            .HasForeignKey<BlogHeader>();

        // M:N
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Posts)
            .UsingEntity(
                "PostTag",
                l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagsId").HasPrincipalKey(nameof(Tag.Id)),
                r => r.HasOne(typeof(Post)).WithMany().HasForeignKey("PostsId").HasPrincipalKey(nameof(Post.Id)),
                j => j.HasKey("PostsId", "TagsId"));
    }
}
