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
	public void Add_Theories(int a, int b, int expected)
	{
		var actual = this.sut.Add(a, b);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(5, 5, 0)]
	public void Subtract_Theories(int a, int b, int expected)
	{
		var actual = this.sut.Subtract(a, b);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(1, 1, 1)]
	public void Multiply_Theories(int a, int b, int expected)
	{
		var actual = this.sut.Multiply(a, b);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(1, 1, 1, 0)]
	public void Divide_Theories(int a, int b, int expected, int expectedRemainder)
	{
		var (result, remainder) = this.sut.Divide(a, b);
		Assert.Equal(expected, result);
		Assert.Equal(expectedRemainder, remainder);
    }

    [Fact]
    public void Divide_DivideByZero_Exception()
        => Assert.Throws<DivideByZeroException>(() => this.sut.Divide(1, 0));
}
