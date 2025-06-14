using JK.Common.DateTimeProviders;

namespace SampleLibrary;

public sealed class SimpleAgeCalculator
{
    public int Calculate(DateOnly birthday)
    {
        var now = DateOnly.FromDateTime(DateTime.Today);
        var age = now.Year - birthday.Year;
        if (birthday > now.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}

public sealed class CustomProviderAgeCalculator
{
    private readonly IDateTimeProvider dateTime;

    public CustomProviderAgeCalculator(IDateTimeProvider dateTimeProvider)
    {
        this.dateTime = dateTimeProvider;
    }

    public CustomProviderAgeCalculator() : this(new DefaultDateTimeProvider())
    {
    }

    public int Calculate(in DateOnly birthday)
    {
        var now = DateOnly.FromDateTime(this.dateTime.Today);
        var age = now.Year - birthday.Year;
        if (birthday > now.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}

public sealed class BuiltInProviderAgeCalculator
{
    // TimeProvider was introduced in .NET 8
    private readonly TimeProvider time;

    public BuiltInProviderAgeCalculator(TimeProvider timeProvider)
    {
        this.time = timeProvider;
    }

    public BuiltInProviderAgeCalculator() : this(TimeProvider.System)
    {
    }

    public int Calculate(in DateOnly birthday)
    {
        var now = DateOnly.FromDateTime(this.time.GetLocalNow().DateTime);
        var age = now.Year - birthday.Year;
        if (birthday > now.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
