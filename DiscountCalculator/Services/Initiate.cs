using DiscountCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountCalculator.Services
{
	public class Initiate
	{
		public List<ShipmentPrice> LoadShipmentPrices()
		{
			var shipmentPrices = new List<ShipmentPrice>();
			shipmentPrices.Add(new ShipmentPrice(Providers.LP, Size.S, 1.50m));
			shipmentPrices.Add(new ShipmentPrice(Providers.LP, Size.M, 4.90m));
			shipmentPrices.Add(new ShipmentPrice(Providers.LP, Size.L, 6.90m));
			shipmentPrices.Add(new ShipmentPrice(Providers.MR, Size.S, 2m));
			shipmentPrices.Add(new ShipmentPrice(Providers.MR, Size.M, 3m));
			shipmentPrices.Add(new ShipmentPrice(Providers.MR, Size.L, 4m));

			return shipmentPrices;
		}
	}
}
