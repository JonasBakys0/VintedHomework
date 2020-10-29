using System;

namespace DiscountCalculator.Models
{
	public class InputFromFileNotValidException : Exception
	{
        public InputFromFileNotValidException()
        {
        }

        public InputFromFileNotValidException(string message)
            : base(message)
        {
        }

        public InputFromFileNotValidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
