using MskManager.Common.Nancy.HandleError;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;
using System;
using NLog;

namespace MskManager.Scrapper.Handlers
{
    public static class ErrorHandler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Enable(IPipelines pipelines, IResponseNegotiator responseNegotiator)
        {
            if(pipelines == null)
            {
                throw new ArgumentNullException(nameof(pipelines));
            }

            if(responseNegotiator == null)
            {
                throw new ArgumentNullException(nameof(responseNegotiator));
            }

            pipelines.OnError += (context, exception) => HandleException(context, exception, responseNegotiator);
        }

        private static Response HandleException(NancyContext context, Exception exception, IResponseNegotiator responseNegotiator)
        {
            LogException(exception);

            return CreateNegociatedResponse(context, exception, responseNegotiator);
        }

        private static Response CreateNegociatedResponse(NancyContext context, Exception exception, IResponseNegotiator responseNegotiator)
        {
            var httpServiceError = HttpServiceErrorUtilities
                .ExtractFromException(exception, HttpServiceErrorDefinition.GeneralError);

            var negociator = new Negotiator(context)
                .WithStatusCode(httpServiceError.HttpStatusCode)
                .WithModel(httpServiceError.ServiceErrorModel);

            return responseNegotiator.NegotiateResponse(negociator, context);
        }

        private static void LogException(Exception exception)
        {
            if (Logger.IsErrorEnabled)
            {
                Logger.Error(exception);
            }
        }
    }
}
