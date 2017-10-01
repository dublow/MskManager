using Nancy.ErrorHandling;
using Nancy;
using Nancy.Responses.Negotiation;
using MskManager.Common.Nancy.HandleError;
using MskManager.Common.Nancy.HandleError.Exceptions;
using NLog;

namespace MskManager.Scrapper.Handlers
{
    public class StatusCodeHandler404 : IStatusCodeHandler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IResponseNegotiator _responseNegotiator;

        public StatusCodeHandler404(IResponseNegotiator responseNegotiator)
        {
            _responseNegotiator = responseNegotiator;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            LogException(context);
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

        private static void LogException(NancyContext context)
        {
            if (Logger.IsErrorEnabled)
            {
                Logger.Error(new NotFoundErrorException(context.Request.Url));
            }
        }
    }
}
