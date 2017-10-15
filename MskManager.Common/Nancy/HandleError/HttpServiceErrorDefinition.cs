using MskManager.Common.Nancy.HandleError.Models;
using Nancy;

namespace MskManager.Common.Nancy.HandleError
{
    public static class HttpServiceErrorDefinition
    {
        public static HttpServiceError GeneralError =
            new HttpServiceError(
                new ServiceErrorModel(
                    ServiceErrorCode.GeneralError,
                    "An error occured during processing the request."),
                HttpStatusCode.BadRequest);

        public static HttpServiceError NotFoundError =
            new HttpServiceError(
                new ServiceErrorModel(
                    ServiceErrorCode.NotFound,
                    "The requested entity was not found."),
                HttpStatusCode.NotFound);

        public static HttpServiceError InternalServerError =
            new HttpServiceError(
                new ServiceErrorModel(
                    ServiceErrorCode.InternalServerError,
                    "There was an internal server error during processing the request."),
                HttpStatusCode.InternalServerError);
    }
}
