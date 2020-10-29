using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;

namespace DiscountCalculator.Services
{
	public class CountingService : ICountingService
	{
		private readonly IRulesService _rulesService;
		private readonly IMappingService _readDataService;
		private static DateTime currentMonth; 

		public CountingService(IRulesService rulesService, IMappingService readDataService)
		{
			_rulesService = rulesService;
			_readDataService = readDataService;
		}

		public void CalculateShipmentDiscount(string readLine)
		{
			try
			{
				var shipment = _readDataService.MapToShipment(readLine);
				CalculateDiscount(shipment);
				Console.WriteLine(shipment.ToString());
			}
			catch (InputFromFileNotValidException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void CalculateDiscount(Shipment shipment)
		{
			_rulesService.ApplyRules(shipment, CheckIfNextMonth(shipment));
			shipment.ApplyDiscount();
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
