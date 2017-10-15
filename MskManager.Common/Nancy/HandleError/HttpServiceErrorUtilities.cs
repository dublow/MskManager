using MskManager.Common.Nancy.HandleError.Exceptions;
using MskManager.Common.Nancy.HandleError.Models;
using System;

namespace MskManager.Common.Nancy.HandleError
{
    public static class HttpServiceErrorUtilities
    {
        public static HttpServiceError ExtractFromException(Exception exception, HttpServiceError defaultValue)
        {
            var result = defaultValue;

            if(exception != null)
            {
                var exceptionWithServiceError = exception as IHasHttpServiceError;

                if(exceptionWithServiceError != null)
                {
                    result = exceptionWithServiceError.HttpServiceError;
                }
            }

            return result;
        }
    }
}
