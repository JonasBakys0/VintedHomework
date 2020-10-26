using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
				// Read the stream as a string, and write the string to the console.
				while ((readLine = sr.ReadLine()) != null)
				{
					var shipment = readLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
					if (!_validateService.ValidateInputFromFile(shipment))
					{
						shipments.Add(new Shipment(readLine));
						continue;
					}

					shipments.Add(new Shipment(DateTime.Parse(shipment[0]), (Providers)Enum.Parse(typeof(Providers),
						shipment[2]), (Size)Enum.Parse(typeof(Size), shipment[1]), readLine));
				}
			}

			return shipments;
		}

		//dar perziuretis pritna
		public void PrintShipments(List<Shipment> shipments)
		{
			Console.WriteLine("Shipments:");
			foreach (var shipment in shipments)
			{
				if (shipment.IsCorupted)
				{
					Console.WriteLine($"{shipment.InputContent}");
					continue;
				}

				Console.WriteLine($"{shipment.Date.ToShortDateString()} {shipment.PackageSize} {shipment.Provider} {shipment.Price:0.00} " +
					(shipment.Discount == null || shipment.Discount == 0 ? "-" : $"{ shipment.Discount?.ToString("0.00")}"));
			}
		}
	}
}
