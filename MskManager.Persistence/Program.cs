using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MskManager.Common.Bus.Commands;
using MskManager.Common.Bus.Utils;
using MskManager.Persistence.Providers;
using MskManager.Persistence.Repositories;
using NServiceBus;

namespace MskManager.Persistence
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
            
        }

        static async Task MainAsync(string[] args)
        {
            var cnx = ConfigurationManager.ConnectionStrings["MskManager"];

            var container = new UnityContainer();
            container.RegisterInstance(cnx);
            container.RegisterType<IProvider, SqlProviders>();
            container.RegisterType<IDeezerUserRepository, DeezerUserRepository>();

            var endpointInstance = await BusUtils.CreateBus("mskmanager.persistence", (config, route) =>
            {
                config.UseContainer<UnityBuilder>(customizations: customizations =>
                {
                    customizations.UseExistingContainer(container);
                });
            });

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
