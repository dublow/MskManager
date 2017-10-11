using System;
using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using MskManager.Common.Extensions;
using MskManager.Common.Nancy.Validation;
using MskManager.Common.Test.Nancy;
using MskManager.Frontoffice.Models;
using MskManager.Frontoffice.Modules;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using NServiceBus;
using NServiceBus.Testing;
using NUnit.Framework;

namespace MskManager.Frontoffice.Test
{
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

                cfg.RootPathProvider<TestingRootPathProvider<CustomBootstrapper>>();
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
