using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;

namespace MskManager.Common.Bus.Utils
{
    public static class BusUtils
    {
        public static async Task<IEndpointInstance> CreateBus(string endpoint, Action<RoutingSettings> configureRoute = null)
        {
            QueueCreationUtils.CreateQueuesForEndpoint(endpoint, Environment.UserName);

            var endpointConfiguration = new EndpointConfiguration(endpoint);
            endpointConfiguration.EnableWindowsPerformanceCounters();
            endpointConfiguration.SendFailedMessagesTo($"{endpoint}.error");
            endpointConfiguration.DisableFeature<MessageDrivenSubscriptions>();
            endpointConfiguration.DisableFeature<TimeoutManager>();

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
           
            var routing = transport.Routing();
            configureRoute?.Invoke(routing);

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            return endpointInstance;
        }
    }
}
