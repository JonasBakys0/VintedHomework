using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountCalculator.Services.Interfaces
{
	public interface IValidateService
	{
		bool ValidateInputFromFile(string[] input);
	}
}
