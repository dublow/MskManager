using System;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using MskManager.Common.Bus.Utils;
using System.Threading.Tasks;
using FluentValidation;
using MskManager.Common.Bus.Commands;
using MskManager.Common.Nancy.Validation;
using Nancy.Responses.Negotiation;

namespace MskManager.Frontoffice
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            ApplicationStartupAsync(container, pipelines).GetAwaiter().GetResult();
        }

        private async Task ApplicationStartupAsync(TinyIoCContainer container, IPipelines pipelines)
        {
            var endpointInstance = await BusUtils.CreateBus("mskmanager.frontoffice", (config, route) => {
                route.RouteToEndpoint(typeof(AddDeezerUser), "mskmanager.persistence");
            });
            container.Register(endpointInstance);


            var type = typeof(IValidator);
            var validatorTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains("MskManager.Frontoffice"))
                .SelectMany(a => a.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            container.RegisterMultiple(type, validatorTypes);
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(config => {
                    config.ResponseProcessors = new[] { typeof(JsonProcessor), typeof(ViewProcessor) };
                });
            }
        }
    }
}
