using System.Collections.Generic;

namespace DiscountCalculator.Models
{
	public static class Constants
	{
		public static List<ShipmentPrice> ShipmentPrices = new List<ShipmentPrice>
		{
			new ShipmentPrice(Providers.LP, Size.S, 1.50m),
			new ShipmentPrice(Providers.LP, Size.M, 4.90m),
			new ShipmentPrice(Providers.LP, Size.L, 6.90m),
			new ShipmentPrice(Providers.MR, Size.S, 2m),
			new ShipmentPrice(Providers.MR, Size.M, 3m),
			new ShipmentPrice(Providers.MR, Size.L, 4m)
		};

		public static decimal MonthlyDiscouts = 10;
	}
}
