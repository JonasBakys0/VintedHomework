using DiscountCalculator.Services.Interfaces;
using System.IO;

namespace DiscountCalculator
{
	class DiscountCalculator
	{
		private readonly ICountingService _service;

		public DiscountCalculator(ICountingService service)
		{
			_service = service;
		}

		public void Run()
		{
			string readLine;
			using (var sr = new StreamReader("input.txt"))
			{
				while ((readLine = sr.ReadLine()) != null)
				{
					_service.CalculateShipmentDiscount(readLine);
				}
			}
			
		}
	}
}
