using System;
using Xunit;

namespace SystemUnderTest.Tests;

public class AgeCalculatorTests
{
    //[Theory]
    //[InlineData(29, "1983-03-15", "2012-06-12")]
    //[InlineData(8, "2000-02-29", "2009-02-28")] // leap year not reached
    //[InlineData(9, "2000-02-29", "2009-03-01")] // leap year reached
    //public void CalculateAge_Theories(int expected, string birthday, string now)
    //{
    //    var birthdayDate = DateTime.Parse(birthday);
    //    var nowDate = DateTime.Parse(now);
    //    var dateTimeProvider = new MockDateTimeProvider 
    //    { 
    //        Today = nowDate
    //    };
    //    var actual = new AgeCalculator(dateTimeProvider).Calculate(birthdayDate);
    //    Assert.Equal(expected, actual);
    //}

    private record MockDateTimeProvider : JK.Common.DateTimeProviders.IDateTimeProvider
    {
        public DateTime Now { get; init; }

        public DateTime Today { get; init; }

        public DateTime UtcNow { get; init; }
    }
}
