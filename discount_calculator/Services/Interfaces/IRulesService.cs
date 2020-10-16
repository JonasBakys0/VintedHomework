using DiscountCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountCalculator.Services.Interfaces
{
	public interface IRulesService
	{
		/// <summary>
		/// Rule - All S shipments should always match the lowest S package price among the providers.
		/// </summary>
		/// <returns>Lowest price</returns>
		public void GetLowestSmallShipmentPrice(Shipment shipment, List<ShipmentPrice> prices);

		/// <summary>
		/// Rule - Third L shipment via LP should be free, but only once a calendar month.
		/// </summary>
		/// <returns>Is this shipment are third large shipment in LP</returns>
		public void IsThirdLargeLPShipment(Shipment shipment);

		/// <summary>
		/// Rule - Accumulated discounts cannot exceed 10 € in a calendar month.
		/// If there are not enough funds to fully cover a discount this calendar month, it should be covered partially.
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public decimal MothlyDiscounsLeft(Shipment shipment);

		public List<Shipment> ApplyRules(List<Shipment> shipments, List<ShipmentPrice> prices);
	}
}
