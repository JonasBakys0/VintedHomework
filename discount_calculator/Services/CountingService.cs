using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiscountCalculator.Services
{
	public class CountingService : ICountingService
	{
		private readonly IRulesService _rulesService;

		public CountingService(IRulesService rulesService)
		{
			_rulesService = rulesService;
		}

		public void CalculateShipmentsDiscounts()
		{
			var prices = LoadShipmentPrices();

			var shipments = LoadShipments();

			var result = _rulesService.ApplyRules(shipments, prices);

			PrintShipment(result);
			
		}

		private void InitializePrices()
		{
			throw new NotImplementedException();
		}

		private static List<Shipment> LoadShipments()
		{

			var shipments = new List<Shipment>();
			string readLine;

			using (var sr = new StreamReader("input.txt"))
			{
				// Read the stream as a string, and write the string to the console.

				while ((readLine = sr.ReadLine()) != null)
				{
					var shipment = readLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
					if (!ValidateInput(shipment))
					{
						shipments.Add(new Shipment(readLine));
						break;
					}

					shipments.Add(new Shipment(DateTime.Parse(shipment[0]), (Providers)Enum.Parse(typeof(Providers), shipment[2]), (Size)Enum.Parse(typeof(Size), shipment[1]), readLine));
				}
			}

			return shipments;
		}

		public List<ShipmentPrice> LoadShipmentPrices()
		{
			var shipmentPrices = new List<ShipmentPrice>();
			shipmentPrices.Add(new ShipmentPrice(Providers.LP, Size.S, 1.50m));
			shipmentPrices.Add(new ShipmentPrice(Providers.LP, Size.M, 4.90m));
			shipmentPrices.Add(new ShipmentPrice(Providers.LP, Size.L, 6.90m));
			shipmentPrices.Add(new ShipmentPrice(Providers.MR, Size.S, 2m));
			shipmentPrices.Add(new ShipmentPrice(Providers.MR, Size.M, 3m));
			shipmentPrices.Add(new ShipmentPrice(Providers.MR, Size.L, 4m));

			return shipmentPrices;
		}

		private static bool ValidateInput(string[] input)
		{
			if (input.Length != 3)
				return false;

			return true;
		}

		private static void PrintShipment(List<Shipment> shipments)
		{
			Console.WriteLine("Shipments:");
			foreach (var shipment in shipments)
			{
				if (shipment.IsCorupted)
				{
					Console.WriteLine($"{shipment.InputContent}");
					return;
				}

				Console.WriteLine($"{shipment.Date.ToShortDateString()} {shipment.PackageSize} {shipment.Provider} {shipment.Price} {shipment.Discount}");
			}
		}

	}
}
