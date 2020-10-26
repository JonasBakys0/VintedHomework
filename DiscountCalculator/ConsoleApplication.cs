using DiscountCalculator.Services.Interfaces;

namespace DiscountCalculator
{
	class ConsoleApplication
	{
		private readonly ICountingService _service;
		public ConsoleApplication(ICountingService service)
		{
			_service = service;
		}

		public void Run()
		{
			_service.CalculateShipmentsDiscounts();
		}
	}
}
