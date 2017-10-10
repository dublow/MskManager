using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MskManager.Common.Nancy.Validation;
using MskManager.Frontoffice.Models;
using MskManager.Frontoffice.Modules;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using NServiceBus;
using NServiceBus.Testing;
using NUnit.Framework;

namespace MskManager.FrontOffice.Test
{
    [TestFixture]
    public class When_Call_Module
    {
        [Test]
        public void Home_module_Deezer()
        {

            var endpointInstance = new TestableEndpointInstance();
                
            var browser = GetBrowser(endpointInstance);
            var response = browser.Post("/Home/Deezer", context =>
            {
                context.JsonBody(new DeezerUserModel{Id = 1, AccessToken = "12345"});
            });

            var result = response.Body.AsString();

        }

        private Browser GetBrowser(IEndpointInstance endpointInstance)
        {
            return new Browser(cfg =>
            {
                var validators = AssemblyScanner
                    .FindValidatorsInAssembly(Assembly.Load("MskManager.Frontoffice"))
                    .Select(x => (IValidator)Activator.CreateInstance(x.ValidatorType));

                cfg.Module<HomeModule>();
                cfg.Dependency(new ValidatorHelper(validators));
                cfg.Dependency(endpointInstance);
                cfg.ResponseProcessors(new[] { typeof(JsonProcessor) });
            });
        }
    }
}
