using System;
using MskManager.Common.Nancy.HandleError.Models;

namespace MskManager.Common.Nancy.HandleError.Exceptions
{
    public class InternalServerErrorException : Exception, IHasHttpServiceError
    {
        public InternalServerErrorException() : base() { }
        public InternalServerErrorException(string message) : base(message) { }
        public InternalServerErrorException(string message, Exception exception) : base(message, exception) { }

        public HttpServiceError HttpServiceError => HttpServiceErrorDefinition.InternalServerError;
    }
}
