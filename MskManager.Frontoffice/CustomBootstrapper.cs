using System;
using System.Linq;
using MskManager.Common.Configurations.Scrapper;
using MskManager.Common.Http;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;

namespace MskManager.Frontoffice
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            
        }
    }
}
