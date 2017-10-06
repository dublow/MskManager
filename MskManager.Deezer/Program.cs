using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MskManager.Common.Bus.Commands;
using MskManager.Common.Bus.Utils;
using NServiceBus;

namespace MskManager.Deezer
{
    class Program
    {
        static void Main(string[] args)
        {

            AsyncMain().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        static async Task AsyncMain()
        {
            Console.Title = "MskManager.Deezer";

            var busInfo = await BusUtils.CreateBus("mskmanager.deezer");

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();

            await busInfo.EndpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
