using Nancy.ErrorHandling;
using Nancy;
using Nancy.Responses.Negotiation;
using MskManager.Common.Nancy.HandleError;

namespace MskManager.Scrapper.Handlers
{
    public class StatusCodeHandler404 : IStatusCodeHandler
    {
        private readonly IResponseNegotiator _responseNegotiator;

        public StatusCodeHandler404(IResponseNegotiator responseNegotiator)
        {
            _responseNegotiator = responseNegotiator;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            context.NegotiationContext = new NegotiationContext();

            Negotiator negotiator = new Negotiator(context)
                .WithStatusCode(HttpServiceErrorDefinition.NotFoundError.HttpStatusCode)
                .WithModel(HttpServiceErrorDefinition.NotFoundError.ServiceErrorModel);

            context.Response = _responseNegotiator.NegotiateResponse(negotiator, context);
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound;
        }
    }
}
