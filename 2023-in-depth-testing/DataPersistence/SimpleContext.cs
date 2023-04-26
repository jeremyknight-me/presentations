using Microsoft.EntityFrameworkCore;

namespace DataPersistence;

public class SimpleContext : DbContext
{
	public SimpleContext(DbContextOptions<SimpleContext> options)
		: base(options)
	{
	}

	public DbSet<Lookup> Lookups { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var l = modelBuilder.Entity<Lookup>();
		l.HasKey(x => x.Id);
		l.Property(x => x.Id).ValueGeneratedOnAdd();

		l.Property(x => x.Name).HasMaxLength(50).IsUnicode().IsRequired();

		l.Property(x => x.IsDeleted).HasDefaultValue(false);

		l.HasData(LookupFakerFactory.Make(1127190, 100));
	}
}
