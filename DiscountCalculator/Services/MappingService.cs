using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;

namespace DiscountCalculator.Services
{
	public class MappingService : IMappingService
	{
		private readonly IValidateService _validateService;

		public MappingService(IValidateService validateService)
		{
			_validateService = validateService;
		}

		public Shipment MapToShipment(string input)
		{
			var shipment = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			if (_validateService.ValidateInputFromFile(shipment))
				return new Shipment(DateTime.Parse(shipment[0]), (Providers)Enum.Parse(typeof(Providers),
					shipment[2]), (Size)Enum.Parse(typeof(Size), shipment[1]));

			throw new InputFromFileNotValidException(input + " Ignored");
		}
	}
}
