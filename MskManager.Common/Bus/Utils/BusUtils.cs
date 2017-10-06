using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;

namespace MskManager.Common.Bus.Utils
{
    public static class BusUtils
    {
        public static async Task<BusInfo> CreateBus(string endpoint, Action<RoutingSettings> configureRoute = null)
        {
            var queueEndpoint = QueueCreationUtils.CreateQueuesForEndpoint(endpoint, Environment.UserName);
            var endpointConfiguration = new EndpointConfiguration(endpoint);

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.SendFailedMessagesTo(queueEndpoint.Error);
            endpointConfiguration.DisableFeature<MessageDrivenSubscriptions>();
            endpointConfiguration.DisableFeature<TimeoutManager>();

            var routing = transport.Routing();
            configureRoute?.Invoke(routing);
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            return new BusInfo(endpointInstance, transport);
        }
    }
}
