using DiscountCalculator.Services;
using Moq;
using NUnit.Framework;

namespace DiscountCalculator.Tests
{
	public class Tests
	{
		private static CountingService countingService;

		[SetUp]
		public void Setup()
		{
			//countingService = new CountingService(new Mock<RulesService>().Object);
		}

		[Test]
		public void Test1()
		{
			var testInput = "test data";

			//countingService.
			Assert.Pass();
		}
	}
}