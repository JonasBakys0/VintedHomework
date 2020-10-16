using DiscountCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
