namespace DiscountCalculator.Models
{
	public class ShipmentPrice
	{
		public Providers Provider { get; set; }
		public Size PackageSize { get; set; }
		public decimal Price { get; set; }

		public ShipmentPrice(Providers provider, Size packageSize, decimal price)
		{
			Provider = provider;
			PackageSize = packageSize;
			Price = price;
		}
	}
}
