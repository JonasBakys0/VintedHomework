using DiscountCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountCalculator.Services.Interfaces
{
	public interface IReadDataService
	{
		string ReadDataFromFile(string path);
		Shipment MapToShipment(string shipment);
	}
}
