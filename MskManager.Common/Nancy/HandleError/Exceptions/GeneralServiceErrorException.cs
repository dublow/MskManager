using System;
using MskManager.Common.Nancy.HandleError.Models;

namespace MskManager.Common.Nancy.HandleError.Exceptions
{
    public class GeneralServiceErrorException : Exception, IHasHttpServiceError
    {
        public GeneralServiceErrorException() : base() { }
        public GeneralServiceErrorException(string message) : base(message) { }
        public GeneralServiceErrorException(string message, Exception exception) : base(message, exception) { }

        public HttpServiceError HttpServiceError => HttpServiceErrorDefinition.GeneralError;
    }
}
