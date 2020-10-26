using DiscountCalculator.Services;
using NUnit.Framework;

namespace DiscountCalculator.Tests
{
	public class ValidateSeviceTest
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
			var testInput = new string[]{ "2015-04-08", "L", "LP"};

			Assert.IsTrue(_service.ValidateInputFromFile(testInput));
		}

		[Test]
		public void InputWithMemberCountNotThreeShouldFail()
		{
			var testInput = new string[] { "2015-04-08", "LP" };

			Assert.IsFalse(_service.ValidateInputFromFile(testInput));
		}

		[Test]
		public void InputWithBadShipmentSizeShouldFail()
		{
			var testInput = new string[] { "2015-04-08", "T", "LP" };

			Assert.IsFalse(_service.ValidateInputFromFile(testInput));
		}

		[Test]
		public void InputWithBadShipmentProviderShouldFail()
		{
			var testInput = new string[] { "2015-04-08", "L", "TT" };

			Assert.IsFalse(_service.ValidateInputFromFile(testInput));
		}

		[Test]
		public void InputWithBadDateFormatShouldFail()
		{
			var testInput = new string[] { "2015-13-08", "L", "LP" };

			Assert.IsFalse(_service.ValidateInputFromFile(testInput));
		}
	}
}
