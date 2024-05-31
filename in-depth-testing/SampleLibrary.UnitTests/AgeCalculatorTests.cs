using FakeItEasy;
using JK.Common.DateTimeProviders;

namespace SampleLibrary.UnitTests;

public class AgeCalculatorTests
{
    #region Simple

    [Fact]
    public void CalculateAge_DoesNotAlwaysWork()
    {
        var sut = new SimpleAgeCalculator();
        var actual = sut.Calculate(new DateOnly(1983, 03, 15));
        Assert.Equal(41, actual);
    }

    #endregion

    #region Custom Provider

    [Theory]
    [InlineData(29, "1983-03-15", "2012-06-12")]
    [InlineData(8, "2000-02-29", "2009-02-28")] // leap year not reached
    [InlineData(9, "2000-02-29", "2009-03-01")] // leap year reached
    public void CalculateAge_Custom_Theories(int expected, string birthday, string now)
    {
        var birthdayDate = DateOnly.Parse(birthday);
        var nowDate = DateTime.Parse(now);

        var dateTimeProvider = A.Fake<IDateTimeProvider>();
        A.CallTo(() => dateTimeProvider.Today).Returns(nowDate);

        var actual = new CustomProviderAgeCalculator(dateTimeProvider).Calculate(birthdayDate);
        Assert.Equal(expected, actual);
    }

    #endregion

    #region Built-In Provider

    [Theory]
    [InlineData(29, "1983-03-15", "2012-06-12")]
    public void CalculateAge_BuiltIn_Theories(int expected, string birthday, string now)
    {
        var birthdayDate = DateOnly.Parse(birthday);
        var nowDate = DateTime.Parse(now);

        TimeProvider timeProvider = new MockTimeProvider(nowDate);
        var actual = new BuiltInProviderAgeCalculator(timeProvider).Calculate(birthdayDate);
        Assert.Equal(expected, actual);
    }

    private sealed class MockTimeProvider : TimeProvider
    {
        private readonly DateTimeOffset date;

        public MockTimeProvider(DateTime dateTime)
        {
            var offset = this.LocalTimeZone.GetUtcOffset(dateTime);
            this.date = new DateTimeOffset(dateTime, offset);
        }

        public override DateTimeOffset GetUtcNow() => this.date;
    }

    #endregion
}
