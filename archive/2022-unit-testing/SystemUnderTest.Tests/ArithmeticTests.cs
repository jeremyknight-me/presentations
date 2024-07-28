using System;
using System.Collections.Generic;
using Xunit;

namespace SystemUnderTest.Tests;

public class ArithmeticTests
{
    #region Facts

    [Fact]
    public void Add_OneTwo_Three()
    {
        // arrange
        var one = 1;
        var two = 2;

        // act 
        var actual = Arithmetic.Add(one, two);

        // assert
        Assert.Equal(3, actual);
    }

    [Fact]
    public void Add_TwoTwo_Four()
    {
        // arrange
        var two = 2;

        // act 
        var actual = Arithmetic.Add(two, two);

        // assert
        Assert.Equal(4, actual);
    }

    #endregion

    #region Theories

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(2, 2, 4)]
    public void Add_Theories(decimal left, decimal right, decimal expected)
    {
        // arrange - done by theories
        // act 
        var actual = Arithmetic.Add(left, right);

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(6, 2, 3)]
    public void Divide_Theories(decimal left, decimal right, decimal expected)
    {
        // arrange - done by theories
        // act 
        var actual = Arithmetic.Divide(left, right);

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(7, 4, 28)]
    public void Multiply_Theories(decimal left, decimal right, decimal expected)
    {
        // arrange - done by theories
        // act 
        var actual = Arithmetic.Multiply(left, right);

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(SubtractData))]
    public void Subtract_Theories(decimal left, decimal right, decimal expected)
    {
        // arrange - done by theories
        // act 
        var actual = Arithmetic.Subtract(left, right);

        // assert
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> SubtractData
    {
        get
        {
            yield return new object[] { 8, 3, 5 };
            yield return new object[] { 4, 4, 0 };
        }
    }

    #endregion

    #region Exceptions

    [Fact]
    public void Divide_ZeroRightSide_DivideByZeroException()
    {
        // arrange
        var left = 1m;
        var right = 0m;

        // act 
        Action action = () =>
        {
            _ = Arithmetic.Divide(left, right);
        };

        // assert
        var exception = Assert.Throws<DivideByZeroException>(action);
        Assert.Equal("Attempted to divide by zero.", exception.Message);
    }

    #endregion
}
