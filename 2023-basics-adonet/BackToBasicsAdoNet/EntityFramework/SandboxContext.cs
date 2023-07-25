using Microsoft.EntityFrameworkCore;

namespace BackToBasicsAdoNet.EntityFramework;

public class SandboxContext : DbContext
{
    public SandboxContext(DbContextOptions<SandboxContext> options)
        : base(options)
    {
    }

    public DbSet<Lookup> Lookups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var lookup = modelBuilder.Entity<Lookup>();
        lookup.ToTable("Lookups", "dbo");
        lookup.HasKey(x => x.Id);
        lookup.Property(x => x.Id).ValueGeneratedOnAdd();
        lookup.Property(x => x.Name).HasMaxLength(100).IsUnicode().IsRequired();
        base.OnModelCreating(modelBuilder);
    }
}
