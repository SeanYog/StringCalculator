using FluentAssertions;
using System;
using Xunit;

namespace StringCalculator.Tests
{
	public class CalculatorTests
	{
		[Theory]
		[InlineData("", 0)]
		[InlineData("1", 1)]
		[InlineData("1,2", 3)]
		public void Add_ShouldAddUpToTwoStrings_WhenStringIsValid(string calculation, int expected)
		{
			// Arrange
			var sut = new Calculator();

			// Act
			var res = sut.Add(calculation);

			// Assert
			res.Should().Be(expected);
		}

		[Theory]
		[InlineData("1,2,3", 6)]
		[InlineData("10,20,30,40,50", 150)]
		public void Add_ShouldAddUpToAnyNumber_WhenStringIsValid(string calculation, int expected)
		{
			// Arrange
			var sut = new Calculator();

			// Act
			var res = sut.Add(calculation);

			// Assert
			res.Should().Be(expected);
		}

		[Theory]
		[InlineData("1\n2,3", 6)]
		[InlineData("10\n20,30\n40,50", 150)]
		public void Add_ShouldAddUsingNewLineDelimiter_WhenStringIsValid(string calculation, int expected)
		{
			// Arrange
			var sut = new Calculator();

			// Act
			var res = sut.Add(calculation);

			// Assert
			res.Should().Be(expected);
		}

		[Theory]
		[InlineData("//;\n1;2", 3)]
		[InlineData("//;\n1;2;5;7", 15)]
		public void Add_ShouldAddUsingCustomDelimiter_WhenStringIsValid(string calculation, int expected)
		{
			// Arrange
			var sut = new Calculator();

			// Act
			var res = sut.Add(calculation);

			// Assert
			res.Should().Be(expected);
		}

		[Theory]
		[InlineData("1,2,-3", "-3")]
		[InlineData("//;\n1;2;-3;-4", "-3,-4")]
		public void Add_ShouldThrowAnException_WhenNegativeNumbersAreUsed(string calculation, string negativeNumbers)
		{
			// Arrange
			var sut = new Calculator();

			// Act
			Action action = () => sut.Add(calculation);

			// Assert
			action.Should().Throw<Exception>()
				.WithMessage("Negatives not allowed: " + negativeNumbers);
		}

		[Theory]
		[InlineData("1,1001", 1)]
		[InlineData("1,3,1002", 4)]
		public void Add_ShouldIgnoeNumbersLargerThan1000_WhenStringIsValid(string calculation, int expected)
		{
			// Arrange
			var sut = new Calculator();

			// Act
			var res = sut.Add(calculation);

			// Assert
			res.Should().Be(expected);

		}

		[Theory]
		[InlineData("//[***]\n1***2***3", 6)]
		public void Add_ShouldLetDelimatersBeOfAnyLength_WhenStringIsValid(string calculation, int expected)
		{
			// Arrange
			var sut = new Calculator();

			// Act
			var res = sut.Add(calculation);

			// Assert
			res.Should().Be(expected);

		}

		[Theory]
		[InlineData("//[*][%]\n1*2%3", 6)]
		public void Add_ShouldAllowMultipleDelimaters_WhenStringIsValid(string calculation, int expected)
		{
			// Arrange
			var sut = new Calculator();

			// Act
			var res = sut.Add(calculation);

			// Assert
			res.Should().Be(expected);

		}

		[Theory]
		[InlineData("//[*@][%]\n1*@2%3", 6)]
		public void Add_ShouldAllowMultipleDelimatersLongerThanOne_WhenStringIsValid(string calculation, int expected)
		{
			// Arrange
			var sut = new Calculator();

			// Act
			var res = sut.Add(calculation);

			// Assert
			res.Should().Be(expected);

		}
	}
}
