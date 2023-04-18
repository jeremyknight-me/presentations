namespace TestingYourTests;

public sealed class Calculator
{
	public int Add(int a, int b) => a + b;

	public int Subtract(int a, int b) => a - b;

	public int Multiply(int a, int b) => a * b;

	public (int Result, int Remainder) Divide(int a, int b)
	{
		if (b == 0)
		{
			throw new DivideByZeroException();
		}

		var result = a / b;
		var remainder = a % b;
		return (result, remainder);
	}
}
