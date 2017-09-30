using System;

namespace MskManager.Common.Exceptions
{
    public class ParsorException : Exception
    {
        public ParsorException(string parsorName, Exception exception) : base(parsorName, exception)
        { }
    }
}
