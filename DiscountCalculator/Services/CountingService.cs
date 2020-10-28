using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DiscountCalculator.Services
{
	public class CountingService : ICountingService
	{
		private readonly IRulesService _rulesService;
		private readonly IValidateService _validateService;
		private readonly IReadDataService _readDataService;
		private static DateTime currentMonth; 

		public CountingService(IRulesService rulesService, IValidateService validateService, IReadDataService readDataService)
		{
			_rulesService = rulesService;
			_validateService = validateService;
			_readDataService = readDataService;
		}

		public void CalculateShipmentsDiscounts(string readLine)
		{
			try
			{
				var shipment = _readDataService.MapToShipment(readLine);
				CalculateDiscount(shipment);
				Console.WriteLine(shipment.ToString());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void CalculateDiscount(Shipment shipment)
		{
			//shipment.SetPrice();
			_rulesService.ApplyRules(shipment, CheckIfNextMonth(shipment));
			shipment.ApplyDiscount();
		}

		private Shipment MapToShipment(string input)
		{
			var shipment = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			if (_validateService.ValidateInputFromFile(shipment))
				return new Shipment(DateTime.Parse(shipment[0]), (Providers)Enum.Parse(typeof(Providers),
					shipment[2]), (Size)Enum.Parse(typeof(Size), shipment[1]));

			throw new Exception(input + " Ignored");
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
