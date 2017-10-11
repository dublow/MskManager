using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MskManager.Common.Bus;
using MskManager.Common.Bus.Commands;
using MskManager.Common.Bus.Utils;
using MskManager.Common.Http;
using NServiceBus;
namespace MskManager.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "MskManager.Main";

            var endpoint = await BusUtils.CreateBus("mskmanager.main", routing =>
            {
                routing.RouteToEndpoint(typeof(AddSong), "mskmanager.deezer");
            });




            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();

            await endpoint.Stop()
                .ConfigureAwait(false);
        }
    }
}
