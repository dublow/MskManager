using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MskManager.Common.Configurations.Nancy;
using Nancy.Hosting.Self;

namespace MskManager.Frontoffice
{
    class Program
    {
        static void Main(string[] args)
        {
            INancyConfiguration nancyConfiguration = new NancyConfiguration();

            var uri = new Uri($"http://{nancyConfiguration.Domain}:{nancyConfiguration.Port}");
            using (var host = new NancyHost(uri))
            {
                host.Start();
                Console.WriteLine($"Running on > {uri}");
                Console.ReadLine();
            }
        }
    }
}
