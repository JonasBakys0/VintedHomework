using DiscountCalculator.Models;
using System.Collections.Generic;

namespace DiscountCalculator.Services.Interfaces
{
	public interface IInputOutputService
	{
		List<Shipment> LoadShipments();
		void PrintShipment(Shipment shipment);
	}
}
