using Nancy;
using System;
using System.Linq;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using MskManager.Common.Configurations.Scrapper;
using MskManager.Common.Http;
using MskManager.Scrapper.Scrappers;
using MskManager.Scrapper.Parsers;
using Nancy.Responses.Negotiation;
using MskManager.Scrapper.Handlers;

namespace MskManager.Scrapper
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IScrapperConfiguration, ScrapperConfiguration>();
            container.Register<IHttpClient, HttpClient>();
            container.Register<IScrapper, RadioScrapper>();

            var type = typeof(IParser);

            var parserTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            container.RegisterMultiple<IParser>(parserTypes);
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(config => {
                    config.StatusCodeHandlers = new[] { typeof(StatusCodeHandler404), typeof(StatusCodeHandler500) };
                    config.ResponseProcessors = new[] { typeof(JsonProcessor) };
                });
            }
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            ErrorHandler.Enable(pipelines, container.Resolve<IResponseNegotiator>());
        }
    }
}
