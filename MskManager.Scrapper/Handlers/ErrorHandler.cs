using MskManager.Common.Nancy.HandleError;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;
using System;

namespace MskManager.Scrapper.Handlers
{
    public static class ErrorHandler
    {
        // Todo log
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
            // Log
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

        private static void LogException(NancyContext context, Exception exception)
        {
            //if (log.IsErrorEnabled)
            //{
            //    log.ErrorFormat("An exception occured during processing a request. (Exception={0}).", exception);
            //}
        }
    }
}
