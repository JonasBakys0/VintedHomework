using DiscountCalculator.Services;
using NUnit.Framework;

namespace DiscountCalculator.Tests
{
	public class ValidateSeviceTests
	{
		private static ValidateService _service;

		[SetUp]
		public void Setup()
		{
			_service = new ValidateService();
		}

		[Test]
		public void ValidationWithGoodDataShouldBeSuccessful()
		{
			// Arrange
			var testInput = new string[]{ "2015-04-08", "L", "LP"};

			// Act
			var result = _service.ValidateInputFromFile(testInput);

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void InputWithMemberCountNotThreeShouldFail()
		{
			// Arrange
			var testInput = new string[] { "2015-04-08", "LP" };

			// Act
			var result = _service.ValidateInputFromFile(testInput);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void InputWithBadShipmentSizeShouldFail()
		{
			// Arrange
			var testInput = new string[] { "2015-04-08", "T", "LP" };

			// Act
			var result = _service.ValidateInputFromFile(testInput);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void InputWithBadShipmentProviderShouldFail()
		{
			// Arrange
			var testInput = new string[] { "2015-04-08", "L", "TT" };

			// Act
			var result = _service.ValidateInputFromFile(testInput);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void InputWithBadDateFormatShouldFail()
		{
			// Arrange
			var testInput = new string[] { "2015-13-08", "L", "LP" };

			// Act
			var result = _service.ValidateInputFromFile(testInput);

			// Assert
			Assert.IsFalse(result);
		}
	}
}
