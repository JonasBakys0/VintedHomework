using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;
using System.Linq;

namespace DiscountCalculator.Services
{
	public class CountingService : ICountingService
	{
		private readonly IRulesService _rulesService;
		private readonly IInputOutputService _inputOutputService;
		private static DateTime currentMonth; 

		public CountingService(IRulesService rulesService, IInputOutputService inputOutputService)
		{
			_rulesService = rulesService;
			_inputOutputService = inputOutputService;
		}

		public void CalculateShipmentsDiscounts()
		{
			var shipments = _inputOutputService.LoadShipments();
			foreach(var shipment in shipments)
			{
				if (!shipment.IsCorupted)
				{
					shipment.SetPrice();
					_rulesService.ApplyRules(shipment, CheckIfNextMonth(shipment));
					shipment.ApplyDiscount();
				}

				_inputOutputService.PrintShipment(shipment);
			}
		}

		private bool CheckIfNextMonth(Shipment shipment)
		{
			if(currentMonth.Month != shipment.Date.Month)
			{
				currentMonth = shipment.Date;
				return true;
			}

			return false;
		}
	}
}
