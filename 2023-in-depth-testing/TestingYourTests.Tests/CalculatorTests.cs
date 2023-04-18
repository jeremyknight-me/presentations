namespace TestingYourTests.Tests;

public class CalculatorTests
{
	private readonly Calculator sut;

    public CalculatorTests()
    {
		this.sut = new();
    }

    [Theory]
	[InlineData(5, 5, 10)]
	public void Add_Theories(int first, int second, int expected)
	{
		var actual = sut.Add(first, second);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(5, 5, 0)]
	public void Subtract_Theories(int first, int second, int expected)
	{
		var actual = sut.Subtract(first, second);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(1, 1, 1)]
	public void Multiply_Theories(int first, int second, int expected)
	{
		var actual = this.sut.Multiply(first, second);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(1, 1, 1, 0)]
	public void Divide_Theories(int first, int second, int expected, int expectedRemainder)
	{
		var actual = this.sut.Divide(first, second);
		Assert.Equal(expected, actual.Result);
		Assert.Equal(expectedRemainder, actual.Remainder);
	}
}
