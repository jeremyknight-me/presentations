using System;
using JK.Common.DateTimeProviders;

namespace SystemUnderTest;

#region No Provider

public class AgeCalculatorOriginal
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

#endregion

#region Provider

public class AgeCalculatorUpdated
{
    private readonly IDateTimeProvider dateTime;

    public AgeCalculatorUpdated(IDateTimeProvider dateTimeProvider)
    {
        this.dateTime = dateTimeProvider;
    }

    public AgeCalculatorUpdated() 
        : this(new DefaultDateTimeProvider())
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

#endregion

