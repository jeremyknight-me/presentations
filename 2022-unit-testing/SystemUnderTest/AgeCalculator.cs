using JK.Common.DateTimeProviders;

namespace SystemUnderTest;

#region No Provider

public class AgeCalculator
{
    public int Calculate(DateTime birthday)
    {
        var now = DateTime.Today;
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

//public class AgeCalculator
//{
//    private readonly IDateTimeProvider dateTime;

//    public AgeCalculator(IDateTimeProvider dateTimeProvider)
//    {
//        this.dateTime = dateTimeProvider;
//    }

//    public AgeCalculator() : this(new DefaultDateTimeProvider())
//    {
//    }

//    public int Calculate(in DateTime birthday)
//    {
//        var now = this.dateTime.Today;
//        var age = now.Year - birthday.Year;
//        if (birthday > now.AddYears(-age))
//        {
//            age--;
//        }

//        return age;
//    }
//}

#endregion

