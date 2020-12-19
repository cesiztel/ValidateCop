using System;
namespace ValidateCop.Exceptions
{
    public class ValidateCopException : Exception
    {
        public ValidateCopException() { }

        public ValidateCopException(string message)
            : base(message)
        {
        }

        public ValidateCopException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
