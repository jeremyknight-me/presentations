using Bogus;
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

		l.HasData(this.GenerateData());
	}

	private IEnumerable<Lookup> GenerateData()
	{
		Randomizer.Seed = new Random(1127190);
		var id = 0;
		var fakeLookups = new Faker<Lookup>()
			.RuleFor(x => x.Id, f => ++id)
			.RuleFor(x => x.Name, f => $"{f.Hacker.Adjective()} {f.Hacker.Noun()}")
			.RuleFor(x => x.IsDeleted, f => f.Random.Bool());
		var lookups = fakeLookups.Generate(100);
		return lookups;
	}
}
