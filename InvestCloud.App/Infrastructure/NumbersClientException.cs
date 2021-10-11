using System;

namespace InvestCloud.App.Infrastructure
{
    public class NumbersClientException : Exception
    {
        public NumbersClientException() : base()
        {
        }

        public NumbersClientException(string message) : base(message)
        {
        }

        public NumbersClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
