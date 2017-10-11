using System;
using MskManager.Common.Nancy.HandleError.Models;

namespace MskManager.Common.Nancy.HandleError.Exceptions
{
    public class NotFoundErrorException : Exception, IHasHttpServiceError
    {
        public NotFoundErrorException() : base() { }
        public NotFoundErrorException(string message) : base(message) { }
        public NotFoundErrorException(string message, Exception exception) : base(message, exception) { }

        public HttpServiceError HttpServiceError => HttpServiceErrorDefinition.NotFoundError;
    }
}
