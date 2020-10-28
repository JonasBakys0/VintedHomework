using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiscountCalculator.Services
{
	public class ReadDataService : IReadDataService
	{
		private readonly IValidateService _validateService;

		public ReadDataService(IValidateService validateService)
		{
			_validateService = validateService;
		}

		public string ReadDataFromFile(string path)
		{
			return "test";
		}

		public Shipment MapToShipment(string input)
		{
			var shipment = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			if (_validateService.ValidateInputFromFile(shipment))
				return new Shipment(DateTime.Parse(shipment[0]), (Providers)Enum.Parse(typeof(Providers),
					shipment[2]), (Size)Enum.Parse(typeof(Size), shipment[1]));

			throw new Exception(input + " Ignored");
		}
	}
}
