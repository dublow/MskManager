using Nancy.ErrorHandling;
using Nancy;
using Nancy.Responses.Negotiation;
using MskManager.Common.Nancy.HandleError;

namespace MskManager.Scrapper.Handlers
{
    public class StatusCodeHandler500 : IStatusCodeHandler
    {
        private readonly IResponseNegotiator _responseNegotiator;

        public StatusCodeHandler500(IResponseNegotiator responseNegotiator)
        {
            _responseNegotiator = responseNegotiator;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            context.NegotiationContext = new NegotiationContext();

            Negotiator negotiator = new Negotiator(context)
                .WithStatusCode(HttpServiceErrorDefinition.InternalServerError.HttpStatusCode)
                .WithModel(HttpServiceErrorDefinition.InternalServerError.ServiceErrorModel);

            context.Response = _responseNegotiator.NegotiateResponse(negotiator, context);
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError;
        }
    }
}
