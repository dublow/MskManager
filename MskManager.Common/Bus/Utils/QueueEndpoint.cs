namespace MskManager.Common.Bus.Utils
{
    public class QueueEndpoint
    {
        public readonly string Main;
        public readonly string Timeouts;
        public readonly string Error;
        public readonly string Timeoutsdispatcher;
        public readonly string Retries;

        public QueueEndpoint(string endpoint)
        {
            Main = endpoint;
            Timeouts = $"{endpoint}.timeouts";
            Error = $"{endpoint}.error";
            Timeoutsdispatcher = $"{endpoint}.timeoutsdispatcher";
            Retries = $"{endpoint}.retries";
        }
    }
}
