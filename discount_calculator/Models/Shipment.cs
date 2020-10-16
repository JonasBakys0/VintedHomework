using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiscountCalculator.Models
{
	public class Shipment
	{
		public DateTime Date { get; set; }
		public Providers Provider { get; set; }
		public Size PackageSize { get; set; }
		public decimal Price { get; set; }
		public bool IsCorupted { get; set; }
		public string InputContent { get; set; }
		public decimal Discount { get; set; }

		public Shipment(DateTime date, Providers provider, Size packageSize, string inputContent)
		{
			Date = date;
			Provider = provider;
			PackageSize = packageSize;
			IsCorupted = false;
			InputContent = inputContent;
		}

		public Shipment(string inputContent)
		{
			InputContent = inputContent;
			IsCorupted = true;
		}
	}
}
