using System;
using System.Collections.Generic;
using Xunit;

namespace SystemUnderTest.Tests.xUnit
{
    public class ArithmeticTests
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 2, 4)]
        public void Add_GivenTheories(decimal left, decimal right, decimal expected)
        {
            // arrange - done by theories
            // act 
            decimal actual = Arithmetic.Add(left, right);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(6, 2, 3)]
        public void Divide_GivenTheories(decimal left, decimal right, decimal expected)
        {
            // arrange - done by theories
            // act 
            decimal actual = Arithmetic.Divide(left, right);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Divide_ZeroRightSide_DivideByZeroException()
        {
            // arrange
            decimal left = 1;
            decimal right = 0;

            // act 
            Action action = () =>
            {
                _ = Arithmetic.Divide(left, right);
            };

            // assert
            var exception = Assert.Throws<DivideByZeroException>(action);
            Assert.Equal("Attempted to divide by zero.", exception.Message);
        }

        [Theory]
        [InlineData(7, 4, 28)]
        public void Multiply_GivenTheories(decimal left, decimal right, decimal expected)
        {
            // arrange - done by theories
            // act 
            decimal actual = Arithmetic.Multiply(left, right);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(SubtractData))]
        public void Subtract_GivenTheories(decimal left, decimal right, decimal expected)
        {
            // arrange - done by theories
            // act 
            decimal actual = Arithmetic.Subtract(left, right);

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

    }
}
