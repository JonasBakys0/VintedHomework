using DiscountCalculator.Models;
using DiscountCalculator.Services.Interfaces;
using System;

namespace DiscountCalculator.Services
{
	public class ValidateService : IValidateService
	{
		public bool ValidateInputFromFile(string[] input)
		{
			if (input.Length != 3)
				return false;

			// Try to parse first member of input array if it cant be parsed to dateTime it is not valid input
			try
			{
				DateTime.Parse(input[0]);
			}
			catch
			{
				return false;
			}

			// Check if 'Size' and 'providers' enums has these inputs values if no then input is not valid
			if (!Enum.IsDefined(typeof(Size), input[1]) || !Enum.IsDefined(typeof(Providers), input[2]))
				return false;

			return true;
		}
	}
}
