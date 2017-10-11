using System;
using System.Messaging;

namespace MskManager.Common.Bus.Utils
{
    public static class QueueDeletionUtils
    {
        public static void DeleteAllQueues()
        {
            var machineQueues = MessageQueue.GetPrivateQueuesByMachine(".");
            foreach (var q in machineQueues)
            {
                MessageQueue.Delete(q.Path);
            }
        }

        public static void DeleteQueue(string queueName)
        {
            var path = $@"{Environment.MachineName}\private$\{queueName}";
            if (MessageQueue.Exists(path))
            {
                MessageQueue.Delete(path);
            }
        }

        public static void DeleteQueuesForEndpoint(string endpointName)
        {
            // main queue
            DeleteQueue(endpointName);

            // timeout queue
            DeleteQueue($"{endpointName}.timeouts");

            // error queue
            DeleteQueue($"{endpointName}.error");

            // timeout dispatcher queue
            DeleteQueue($"{endpointName}.timeoutsdispatcher");

            // retries queue
            // TODO: Only required in Versions 5 and below
            DeleteQueue($"{endpointName}.retries");
        }
    }
}