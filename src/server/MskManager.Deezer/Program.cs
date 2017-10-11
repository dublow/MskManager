using System;
using System.Threading.Tasks;
using MskManager.Common.Bus.Utils;

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

            var endpoint = await BusUtils.CreateBus("mskmanager.deezer");

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();

            await endpoint.Stop()
                .ConfigureAwait(false);
        }
    }
}
