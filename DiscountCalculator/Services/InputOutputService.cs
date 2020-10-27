using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace DiscountCalculator.Services
{
	public class InputOutputService : IInputOutputService
	{
		private readonly IValidateService _validateService;

		public InputOutputService(IValidateService validateService)
		{
			_validateService = validateService;
		}

		public List<Shipment> LoadShipments()
		{
			var shipments = new List<Shipment>();
			string readLine;
			using (var sr = new StreamReader("input.txt"))
			{
				while ((readLine = sr.ReadLine()) != null)
				{
					var shipment = readLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
					if (_validateService.ValidateInputFromFile(shipment))
					{
						shipments.Add(new Shipment(DateTime.Parse(shipment[0]), (Providers)Enum.Parse(typeof(Providers),
							shipment[2]), (Size)Enum.Parse(typeof(Size), shipment[1]), readLine));
						continue;
					}

					shipments.Add(new Shipment(readLine));
				}
			}

			return shipments;
		}

		public void PrintShipment(Shipment shipment)
		{
			if (shipment.IsCorupted)
			{
				Console.WriteLine($"{shipment.InputContent}");
				return;
			}

			Console.WriteLine(shipment.ToString());
		}
	}
}
