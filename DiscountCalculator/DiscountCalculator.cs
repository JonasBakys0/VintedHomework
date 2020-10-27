using DiscountCalculator.Services.Interfaces;

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
			_service.CalculateShipmentsDiscounts();
		}
	}
}
