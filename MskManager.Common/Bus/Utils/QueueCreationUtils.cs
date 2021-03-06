﻿using System;
using System.Messaging;
using System.Security.Principal;

namespace MskManager.Common.Bus.Utils
{
    public static class QueueCreationUtils
    {
        public static QueueEndpoint CreateQueuesForEndpoint(string endpointName, string account)
        {
            // main queue
            CreateQueue(endpointName, account);

            // timeout queue
            CreateQueue($"{endpointName}.timeouts", account);

            // error queue
            CreateQueue($"{endpointName}.error", account);

            // timeout dispatcher queue
            CreateQueue($"{endpointName}.timeoutsdispatcher", account);

            // retries queue
            // TODO: Only required in Versions 5 and below
            CreateQueue($"{endpointName}.retries", account);

            return new QueueEndpoint(endpointName);
        }

        public static void CreateQueue(string queueName, string account)
        {
            var path = $@"{Environment.MachineName}\private$\{queueName}";
            if (!MessageQueue.Exists(path))
            {
                using (var messageQueue = MessageQueue.Create(path, true))
                {
                    SetDefaultPermissionsForQueue(messageQueue, account);
                }
            }
        }

        static void SetDefaultPermissionsForQueue(MessageQueue queue, string account)
        {
            var allow = AccessControlEntryType.Allow;
            queue.SetPermissions(AdminGroup, MessageQueueAccessRights.FullControl, allow);

            queue.SetPermissions(account, MessageQueueAccessRights.WriteMessage, allow);
            queue.SetPermissions(account, MessageQueueAccessRights.ReceiveMessage, allow);
            queue.SetPermissions(account, MessageQueueAccessRights.PeekMessage, allow);
        }

        static string AdminGroup = GetGroupName(WellKnownSidType.BuiltinAdministratorsSid);

        static string GetGroupName(WellKnownSidType wellKnownSidType)
        {
            return new SecurityIdentifier(wellKnownSidType, null)
                .Translate(typeof(NTAccount))
                .ToString();
        }

    }
}