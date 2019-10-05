using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SystemUnderTest.Tests.MSTest
{
    [TestClass]
    public class ArithmeticTests
    {
        [DataTestMethod]
        [DynamicData(nameof(AddData), DynamicDataSourceType.Property)]
        public void Add_GivenData(decimal left, decimal right, decimal expected)
        {
            // arrange - done by data rows

            // act 
            decimal actual = Arithmetic.Add(left, right);

            // assert
            Assert.AreEqual(expected, actual);

            // CollectionAssert.
            // StringAssert.
        }

        public static IEnumerable<object[]> AddData
        {
            get
            {
                yield return new object[] { 1m, 2m, 3m };
                yield return new object[] { 2m, 2m, 4m };
            }
        }

        [TestMethod]
        public void Divide_ValidNumber_CalculatedValue()
        {
            // arrange
            decimal left = 6;
            decimal right = 2;

            // act 
            decimal actual = Arithmetic.Divide(left, right);

            // assert
            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void Divide_ZeroRightSide_DivideByZeroException()
        {
            // arrange
            decimal left = 1;
            decimal right = 0;

            // act 
            Action action = () => Arithmetic.Divide(left, right);

            // assert
            var exception = Assert.ThrowsException<DivideByZeroException>(action);
            Assert.AreEqual("Attempted to divide by zero.", exception.Message);
        }

        [TestMethod]
        public void Multiply_ValidNumber_CalculatedValue()
        {
            // arrange
            decimal left = 7;
            decimal right = 4;

            // act 
            decimal actual = Arithmetic.Multiply(left, right);

            // assert
            Assert.AreEqual(28, actual);
        }

        [TestMethod]
        public void Subtract_ValidNumber_CalculatedValue()
        {
            // arrange
            decimal left = 8;
            decimal right = 3;

            // act 
            decimal actual = Arithmetic.Subtract(left, right);

            // assert
            Assert.AreEqual(5, actual);
        }
    }
}
