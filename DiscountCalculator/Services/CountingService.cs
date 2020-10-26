using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DiscountCalculator.Services
{
	public class CountingService : ICountingService
	{
		private readonly IRulesService _rulesService;
		private readonly IInputOutputService _inputOutputService;
		private readonly Prices _shipmentsPrices = new Prices();
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
				SetShipmetPrice(shipment);
				_rulesService.ApplyRules(shipment, CheckIfNextMonth(shipment));
				shipment.ApplyDiscount();
			}

			_inputOutputService.PrintShipments(shipments);
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

		private void SetShipmetPrice(Shipment shipment)
		{
			shipment.Price = _shipmentsPrices.ShipmentPrices.FirstOrDefault(p => p.Provider == shipment.Provider && p.PackageSize == shipment.PackageSize).Price;
		}
	}
}
