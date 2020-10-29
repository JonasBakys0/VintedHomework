using DiscountCalculator.Models;
using DiscountCalculator.Services;
using DiscountCalculator.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace DiscountCalculator.Tests
{
	public class MappingServiceTests
	{
		private MappingService _service;
		private Mock<IValidateService> _validateService;

		[SetUp]
		public void Setup()
		{
			_validateService = new Mock<IValidateService>();
			_service = new MappingService(_validateService.Object);
		}

		[Test]
		public void ValidatedInputShouldBeMapedToShipment()
		{
			// Arrange
			var testInput = "2015-04-08 L LP";
			_validateService.Setup(s => s.ValidateInputFromFile(It.IsAny<string[]>())).Returns(true);

			// Act
			var result = _service.MapToShipment(testInput);

			// Assert
			Assert.AreEqual(new Shipment(DateTime.Parse("2015-04-08"), Providers.LP, Size.L).ToString(), result.ToString());
		}

		[Test]
		public void InvalidatedInputShouldThrow()
		{
			// Arrange
			var testInput = "2015-04-08 L LP";
			_validateService.Setup(s => s.ValidateInputFromFile(It.IsAny<string[]>())).Returns(false);

			try
			{
				// Act
				var result = _service.MapToShipment(testInput);
			} catch(Exception ex)
			{
				// Assert
				Assert.AreEqual(testInput + " Ignored", ex.Message);
			}
		}
	}
}
