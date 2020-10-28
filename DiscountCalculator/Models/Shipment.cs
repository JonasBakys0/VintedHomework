using System;
using System.Linq;

namespace DiscountCalculator.Models
{
	public class Shipment
	{
		public DateTime Date { get; set; }
		public Providers? Provider { get; set; }
		public Size? PackageSize { get; set; }
		public decimal? Price { get; set; }
		public decimal? Discount { get; set; }

		public Shipment(DateTime date, Providers provider, Size packageSize)
		{
			Date = date;
			Provider = provider;
			PackageSize = packageSize;
			SetPrice();
		}

		public void ApplyDiscount()
		{
			Price -= Discount ?? 0;
		}

		public override string ToString()
		{
			return $"{Date.ToShortDateString()} {PackageSize} {Provider} {Price:0.00} " + 
				(Discount == null || Discount == 0 ? "-" : $"{ Discount:0.00}");
		}

		private void SetPrice()
		{
			Price = Constants.ShipmentPrices.FirstOrDefault(p => p.Provider == Provider && p.PackageSize == PackageSize).Price;
		}
	}
}
