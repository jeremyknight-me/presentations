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
        lookup.ToTable("Lookup", "dbo");
        lookup.HasKey(x => x.Id);
        lookup.Property(x => x.Name).HasColumnType("nvarchar(100)").IsRequired();
        base.OnModelCreating(modelBuilder);
    }
}
