using Bogus;

namespace SampleWeb.Persistence;

public static class LookupFakerFactory
{
    public static IReadOnlyList<Lookup> Make(int seed, int count)
    {
        Randomizer.Seed = new Random(seed);
        var id = 0;
        var fakeLookups = new Faker<Lookup>()
            .RuleFor(x => x.Id, f => ++id)
            .RuleFor(x => x.Name, f => $"{f.Hacker.Adjective()} {f.Hacker.Noun()}")
            .RuleFor(x => x.IsDeleted, f => f.Random.Bool());
        var lookups = fakeLookups.Generate(count);
        return lookups;
    }
}
