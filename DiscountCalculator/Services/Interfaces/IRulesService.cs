using DiscountCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountCalculator.Services.Interfaces
{
	public interface IRulesService
	{
		public void ApplyRules(Shipment shipment, bool isNextMonth);
	}
}
