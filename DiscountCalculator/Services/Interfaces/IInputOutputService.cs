using DiscountCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountCalculator.Services.Interfaces
{
	public interface IInputOutputService
	{
		List<Shipment> LoadShipments();
		void PrintShipments(List<Shipment> shipments);
	}
}
