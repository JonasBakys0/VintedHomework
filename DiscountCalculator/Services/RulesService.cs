using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;
using System.Linq;

namespace DiscountCalculator.Services
{
	public class RulesService : IRulesService
	{
		public static int lpLargeShipmentsCount = 0;
		public static decimal monthlyDiscouts = Constants.MonthlyDiscouts;

		public void ApplyRules(Shipment shipment, bool isNextMonth)
		{
			if (isNextMonth)
				ResetMonthlyLimits();

				GetLowestSmallShipmentPrice(shipment);
				IsThirdLargeLPShipment(shipment);
				MothlyDiscounsLeft(shipment);
		}

		/// <summary>
		/// Rule - All S shipments should always match the lowest S package price among the providers.
		/// </summary>
		/// <param name="shipment">shipment object to witch we try to apply the rule</param>
		private void GetLowestSmallShipmentPrice(Shipment shipment)
		{
			if (shipment.PackageSize.Equals(Size.S))
				shipment.Discount = shipment.Price - Constants.ShipmentPrices.OrderBy(p => p.Price).FirstOrDefault(p => p.PackageSize == Size.S).Price;
		}

		/// <summary>
		/// Rule - Third L shipment via LP should be free, but only once a calendar month.
		/// </summary>
		/// <param name="shipment">shipment object to witch we try to apply the rule</param>
		private void IsThirdLargeLPShipment(Shipment shipment)
		{
			if (shipment.Provider.Equals(Providers.LP) && shipment.PackageSize.Equals(Size.L) && ++lpLargeShipmentsCount == 3)
				shipment.Discount = shipment.Price;
		}

		/// <summary>
		/// Rule - Accumulated discounts cannot exceed 10 € in a calendar month.
		/// If there are not enough funds to fully cover a discount this calendar month, it should be covered partially.
		/// </summary>
		/// <param name="shipment">shipment object to witch we try to apply the rule</param>
		private void MothlyDiscounsLeft(Shipment shipment)
		{
			if (shipment.Discount > monthlyDiscouts)
				shipment.Discount = monthlyDiscouts;

			if (shipment.Discount != null)
				monthlyDiscouts -= shipment.Discount.Value;
		}

		private void ResetMonthlyLimits()
		{
			lpLargeShipmentsCount = 0;
			monthlyDiscouts = Constants.MonthlyDiscouts;
		}
	}
}
