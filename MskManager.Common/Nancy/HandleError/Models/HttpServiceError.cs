using Nancy;

namespace MskManager.Common.Nancy.HandleError.Models
{
    public class HttpServiceError
    {
        public readonly ServiceErrorModel ServiceErrorModel;
        public readonly HttpStatusCode HttpStatusCode;

        public HttpServiceError(ServiceErrorModel serviceErrorModel, HttpStatusCode httpStatusCode)
        {
            ServiceErrorModel = serviceErrorModel;
            HttpStatusCode = httpStatusCode;
        }
    }
}
