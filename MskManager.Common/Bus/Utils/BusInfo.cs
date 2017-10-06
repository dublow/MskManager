using NServiceBus;

namespace MskManager.Common.Bus.Utils
{
    public class BusInfo
    {
        public readonly IEndpointInstance EndpointInstance;
        public readonly TransportExtensions<MsmqTransport> Transport;

        public BusInfo(IEndpointInstance endpointInstance, TransportExtensions<MsmqTransport> transport)
        {
            EndpointInstance = endpointInstance;
            Transport = transport;
        }
    }
}
