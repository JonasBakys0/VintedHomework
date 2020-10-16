using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscountCalculator.Services
{
	public class RulesService : IRulesService
	{
		public static int lpLargeShipmentsCount = 0;
		/// <summary>
		/// Rule - All S shipments should always match the lowest S package price among the providers.
		/// </summary>
		/// <returns>Lowest price</returns>
		public void GetLowestSmallShipmentPrice(this Shipment shipment, List<ShipmentPrice> prices)
		{
			var lowestPrice = prices.OrderByDescending(p => p.Price).FirstOrDefault(p => p.PackageSize == Size.S).Price;
			if (shipment.Price > lowestPrice)
			{
				shipment.Discount = shipment.Price - lowestPrice;
				shipment.Price = lowestPrice;
			}
		}

		/// <summary>
		/// Rule - Third L shipment via LP should be free, but only once a calendar month.
		/// </summary>
		/// <returns>Is this shipment are third large shipment in LP</returns>
		public void IsThirdLargeLPShipment(this Shipment shipment)
		{
			if(lpLargeShipmentsCount == 3)
			{
				shipment.Discount = shipment.Price;
				shipment.Price = 0;
				lpLargeShipmentsCount = 0;
			}
		}

		/// <summary>
		/// Rule - Accumulated discounts cannot exceed 10 € in a calendar month.
		/// If there are not enough funds to fully cover a discount this calendar month, it should be covered partially.
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public decimal MothlyDiscounsLeft(Shipment shipment)
		{
			return 0;
		}

		public List<Shipment> ApplyRules(List<Shipment> shipments, List<ShipmentPrice> prices)
		{
			foreach(var shipment in shipments)
			{
				shipment.Price = GetShipmetPrice(shipment, prices);
				// First rule
				if (shipment.PackageSize.Equals(Size.S))
					GetLowestSmallShipmentPrice(shipment, prices);

				// Second rule
				if (shipment.Provider.Equals(Providers.LP) && shipment.PackageSize.Equals(Size.L))
					IsThirdLargeLPShipment(shipment);


			}

			return shipments;
		}

		private decimal GetShipmetPrice(Shipment shipment, List<ShipmentPrice> prices)
		{
			return prices.FirstOrDefault(p => p.Provider == shipment.Provider && p.PackageSize == shipment.PackageSize).Price;
		}

		private void print
	}
}
