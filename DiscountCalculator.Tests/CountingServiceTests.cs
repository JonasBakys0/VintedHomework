using DiscountCalculator.Models;
using DiscountCalculator.Services;
using DiscountCalculator.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscountCalculator.Tests
{
	public class CountingServiceTests
	{
		private static CountingService _countingService;
		public readonly RulesService _rulesService = new RulesService();
		public readonly Mock<IInputOutputService> _inputOutputService = new Mock<IInputOutputService>();

		[SetUp]
		public void Setup()
		{

			_countingService = new CountingService(_rulesService, _inputOutputService.Object);
		}

		// First rule - All S shipments should always match the lowest S package price among the providers.
		[Test]
		public void WhenProviderMRAndSizeSmallShouldApplyFirstRule()
		{
			// Arrange
			var testInput = new List<Shipment>
			{
				new Shipment(DateTime.Parse("2015-02-01"), Providers.MR, Size.S, "test")
			};
			_inputOutputService.Setup(s => s.LoadShipments()).Returns(testInput);

			// Act
			_countingService.CalculateShipmentsDiscounts();

			// Assert
			Assert.AreEqual(Constants.ShipmentPrices.OrderBy(p => p.Price).First().Price, 
				testInput.First().Price);
		}

		// Second rule - Third L shipment via LP should be free, but only once a calendar month.
		[Test]
		public void WhenProviderLPAndItsThirdLargeShipmentShouldApplySecondRule()
		{
			// Arrange
			var testInput = new List<Shipment>
			{
				new Shipment(DateTime.Parse("2015-02-01"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-02-02"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-02-03"), Providers.LP, Size.L, "test")

			};
			_inputOutputService.Setup(s => s.LoadShipments()).Returns(testInput);

			// Act
			_countingService.CalculateShipmentsDiscounts();

			// Assert
			Assert.AreEqual(0, testInput.Last().Price);
		}

		// Second rule - Third L shipment via LP should be free, but only once a calendar month.
		[Test]
		public void SecondRuleShouldApplyOnlyOncePerMonth()
		{
			// Arrange
			var testInput = new List<Shipment>
			{
				new Shipment(DateTime.Parse("2015-02-01"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-02-02"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-02-03"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-02-03"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-02-04"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-02-05"), Providers.LP, Size.L, "test")

			};
			_inputOutputService.Setup(s => s.LoadShipments()).Returns(testInput);

			// Act
			_countingService.CalculateShipmentsDiscounts();

			// Assert
			Assert.AreEqual(Constants.ShipmentPrices.First(p => p.Provider.Equals(Providers.LP) &&
				p.PackageSize.Equals(Size.L)).Price, testInput.Last().Price);
		}

		// Second rule - Third L shipment via LP should be free, but only once a calendar month.
		[Test]
		public void SecondRuleShouldApplyEveryMonth()
		{
			// Arrange
			var testInput = new List<Shipment>
			{
				new Shipment(DateTime.Parse("2015-02-01"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-02-02"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-02-03"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-03-03"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-03-04"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-03-05"), Providers.LP, Size.L, "test")

			};
			_inputOutputService.Setup(s => s.LoadShipments()).Returns(testInput);

			// Act
			_countingService.CalculateShipmentsDiscounts();

			// Assert
			Assert.AreEqual(0, testInput.Last().Price);
		}

		// Third rule - Accumulated discounts cannot exceed 10 € in a calendar month. If there are not 
		// enough funds to fully cover a discount this calendar month, it should be covered partially.
		[Test]
		public void WhenDiscountsExceedMonthlyLimitItShouldBeCoveredPartially()
		{
			// Arrange
			var testInput = new List<Shipment>
			{
				new Shipment(DateTime.Parse("2015-03-01"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-03-01"), Providers.MR, Size.S, "test")
			};
			_inputOutputService.Setup(s => s.LoadShipments()).Returns(testInput);
			testInput.First().Discount = Constants.MonthlyDiscouts + 2;

			// Act
			_countingService.CalculateShipmentsDiscounts();

			// Assert
			Assert.AreEqual(Constants.MonthlyDiscouts, testInput.First().Discount);
			Assert.AreEqual(0, testInput.Last().Discount);
		}

		[Test]
		public void MonthlyDiscountsResetsEveryMonth ()
		{
			// Arrange
			var testInput = new List<Shipment>
			{
				new Shipment(DateTime.Parse("2015-03-01"), Providers.LP, Size.L, "test"),
				new Shipment(DateTime.Parse("2015-04-01"), Providers.MR, Size.S, "test")
			};
			_inputOutputService.Setup(s => s.LoadShipments()).Returns(testInput);
			testInput.First().Discount = Constants.MonthlyDiscouts + 2;

			// Act
			_countingService.CalculateShipmentsDiscounts();

			// Assert
			Assert.AreEqual(Constants.MonthlyDiscouts, testInput.First().Discount);
			Assert.AreEqual(Constants.ShipmentPrices.OrderBy(p => p.Price).First().Price, testInput.Last().Price);
		}

		[Test]
		public void WhenInpusIsCoruptedShouldNotApllyRules()
		{
			// Arrange
			var testInput = new List<Shipment>
			{
				new Shipment("corupted")
			};
			_inputOutputService.Setup(s => s.LoadShipments()).Returns(testInput);

			// Act
			_countingService.CalculateShipmentsDiscounts();

			// Assert
			Assert.IsNull(testInput.First().Price);
			Assert.IsNull(testInput.First().Discount);
			Assert.IsTrue(testInput.First().IsCorupted);
		}

		[Test]
		public void ToStringFormatTest()
		{
			// Arrange
			var testInput = new List<Shipment>
			{
				new Shipment(DateTime.Parse("2015-04-01"), Providers.LP, Size.L, "test")
			};
			_inputOutputService.Setup(s => s.LoadShipments()).Returns(testInput);

			// Act
			_countingService.CalculateShipmentsDiscounts();

			// Assert
			Assert.AreEqual("2015-04-01 L LP 6,90 -", testInput.First().ToString());
		}
	}
}
