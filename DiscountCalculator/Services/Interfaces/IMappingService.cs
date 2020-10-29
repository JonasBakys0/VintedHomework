using DiscountCalculator.Models;

namespace DiscountCalculator.Services.Interfaces
{
	public interface IMappingService
	{
		Shipment MapToShipment(string shipment);
	}
}
