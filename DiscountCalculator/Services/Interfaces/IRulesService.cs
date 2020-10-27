using DiscountCalculator.Models;

namespace DiscountCalculator.Services.Interfaces
{
	public interface IRulesService
	{
		public void ApplyRules(Shipment shipment, bool isNextMonth);
	}
}
