using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using MskManager.Common.Bus.Utils;
using System.Threading.Tasks;
using MskManager.Common.Bus.Commands;

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
            
            var endpointInstance = await BusUtils.CreateBus("mskmanager.frontoffice", route => {
                route.RouteToEndpoint(typeof(AddDeezerUser), "mskmanager.storage");
            });
            container.Register(endpointInstance);
        }
    }
}
