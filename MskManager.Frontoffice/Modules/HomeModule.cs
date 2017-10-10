using System;
using System.Threading.Tasks;
using MskManager.Frontoffice.Models;
using Nancy;
using Nancy.ModelBinding;
using NServiceBus;
using MskManager.Common.Bus.Commands;
using MskManager.Common.Nancy.Validation;

namespace MskManager.Frontoffice.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule(IEndpointInstance endpointInstance, ValidatorHelper validatorHelper) : base("Home")
        {
            Get["/Index", true] = async (parameters, ct) =>
            {
                return await Task.Run(() => View["Index.html"]);
            };

            Post["/Deezer", true] = async (parameters, ct) =>
            {
                return await Task.Run(() =>
                {
                    var model = this.Bind<DeezerUserModel>();
                    var validationResult = validatorHelper.Validate(model);

                    if(!validationResult.IsValid)
                    {

                        return Negotiate
                            .WithModel(validationResult)
                            .WithStatusCode(HttpStatusCode.BadRequest)
                            .WithContentType("application/json");
                    }

                    endpointInstance.Send(new AddDeezerUser { Id = model.Id, AccessToken = model.AccessToken });

                    return Negotiate
                            .WithModel(new { success = true })
                            .WithContentType("application/json");
                });

            };
            Get["/Channel", true] = async (parameters, ct) =>
            {
                return await Task.Run(() =>
                {
                    var cacheExpire = 60 * 60 * 24 * 365;

                    return Negotiate
                        .WithHeader("Pragma", "public")
                        .WithHeader("Cache-Control", $"maxage={cacheExpire}")
                        .WithHeader("Expires", DateTime.UtcNow.AddHours(cacheExpire).ToString("R"))
                        .WithView("Channel.html");
                });
            };
        }
    }
}
