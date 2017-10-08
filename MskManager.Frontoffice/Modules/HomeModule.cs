using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MskManager.Frontoffice.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing;

namespace MskManager.Frontoffice.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule() : base("Home")
        {
            Get["/Index", true] = async (parameters, ct) =>
            {
                return await Task.Run(() => View["Index.html"]);
            };

            Post["/Deezer", true] = async (parameters, ct) =>
            {
                return await Task.Run(() =>
                {
                    var model = this.Bind<DeezerModel>();
                    return Response.AsJson(new{success = true});
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
