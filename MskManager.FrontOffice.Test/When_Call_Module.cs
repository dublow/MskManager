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
using Nancy;
using MskManager.Common.Extensions;
using FluentValidation.Results;

namespace MskManager.FrontOffice.Test
{
    public class TestingRootPathProvider : IRootPathProvider
    {
        private static readonly string RootPath;

        static TestingRootPathProvider()
        {
            var directoryName = Path.GetDirectoryName(typeof(Frontoffice.CustomBootstrapper).Assembly.CodeBase);

            if (directoryName != null)
            {
                var assemblyPath = directoryName.Replace(@"file:\", string.Empty);
                RootPath = Path.Combine(assemblyPath, "..", "..", "..", "MskManager.Frontoffice");
            }
        }

        public string GetRootPath()
        {
            return RootPath;
        }
    }
    [TestFixture]
    public class When_Call_Module
    {
        [Test]
        public void HomeModule_Index_ReturnOk()
        {
            var endpointInstance = new TestableEndpointInstance();

            var browser = GetBrowser(endpointInstance);
            var response = browser.Get("/Home/Index", context => {
                context.HttpRequest();
            });
            
            Assert.AreEqual("text/html", response.ContentType);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Index", response.GetViewName());

            response
                .Body["#login"]
                .ShouldExistOnce()
                .And.ShouldContain("Login to deezer");
        }

        [Test]
        public void HomeModule_Deezer_WhenPostModel_ReturnOk()
        {
            var endpointInstance = new TestableEndpointInstance();
                
            var browser = GetBrowser(endpointInstance, true);
            var response = browser.Post("/Home/Deezer", context =>
            {
                context.JsonBody(new DeezerUserModel{Id = 1, AccessToken = "12345"});
            });

            var result = response.Body.AsString();

            Assert.AreEqual("application/json", response.ContentType);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsEmpty(result);
        }

        [Test]
        public void HomeModule_Deezer_WhenPostNullModel_ReturnBadRequest()
        {
            var endpointInstance = new TestableEndpointInstance();

            var browser = GetBrowser(endpointInstance, true);
            var response = browser.Post("/Home/Deezer");

            var result = response.Body.AsString().Deserialize<ValidationResult>();

            Assert.AreEqual("application/json", response.ContentType);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(2, result.Errors.Count);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("NotEmptyValidator", result.Errors[0].ErrorCode);
            Assert.AreEqual("NotEmptyValidator", result.Errors[1].ErrorCode);
        }

        [Test]
        public void HomeModule_Deezer_WhenPostNullIdModel_ReturnBadRequest()
        {
            var endpointInstance = new TestableEndpointInstance();

            var browser = GetBrowser(endpointInstance, true);
            var response = browser.Post("/Home/Deezer", context =>
            {
                context.JsonBody(new DeezerUserModel { AccessToken = "12345" });
            });

            var result = response.Body.AsString().Deserialize<ValidationResult>();

            Assert.AreEqual("application/json", response.ContentType);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("NotEmptyValidator", result.Errors[0].ErrorCode);
        }

        [Test]
        public void HomeModule_Deezer_WhenPostNullAccessTokenModel_ReturnBadRequest()
        {
            var endpointInstance = new TestableEndpointInstance();

            var browser = GetBrowser(endpointInstance, true);
            var response = browser.Post("/Home/Deezer", context =>
            {
                context.JsonBody(new DeezerUserModel { Id = 1 });
            });

            var result = response.Body.AsString().Deserialize<ValidationResult>();

            Assert.AreEqual("application/json", response.ContentType);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("NotEmptyValidator", result.Errors[0].ErrorCode);
        }

        private Browser GetBrowser(IEndpointInstance endpointInstance, bool isJsonRequest = false)
        {
            return new Browser(cfg =>
            {
                var validators = AssemblyScanner
                    .FindValidatorsInAssembly(Assembly.Load("MskManager.Frontoffice"))
                    .Select(x => (IValidator)Activator.CreateInstance(x.ValidatorType));

                cfg.RootPathProvider<TestingRootPathProvider>();
                cfg.ViewFactory<TestingViewFactory>();
                cfg.Module<HomeModule>();
                cfg.Dependency(new ValidatorHelper(validators));
                cfg.Dependency(endpointInstance);

                if(isJsonRequest)
                    cfg.ResponseProcessors(new[] { typeof(JsonProcessor) });
            });
        }
    }
}
