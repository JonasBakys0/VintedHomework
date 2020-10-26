using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountCalculator.Models
{
	public enum Size
	{
		// Small
		S,
		// Medium
		M,
		// Large
		L
	}

	public enum Providers
	{
		// La Poste
		LP,
		// Mondial Relay
		MR
	}

	public class Prices
	{
		public List<ShipmentPrice> ShipmentPrices = new List<ShipmentPrice>
		{
			new ShipmentPrice(Providers.LP, Size.S, 1.50m),
			new ShipmentPrice(Providers.LP, Size.M, 4.90m),
			new ShipmentPrice(Providers.LP, Size.L, 6.90m),
			new ShipmentPrice(Providers.MR, Size.S, 2m),
			new ShipmentPrice(Providers.MR, Size.M, 3m),
			new ShipmentPrice(Providers.MR, Size.L, 4m)
		};
	}
}
