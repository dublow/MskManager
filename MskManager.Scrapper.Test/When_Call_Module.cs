using MskManager.Common.Extensions;
using MskManager.Common.Test.Configurations.Scrapper;
using MskManager.Common.Test.Http;
using MskManager.Scrapper.Models;
using MskManager.Scrapper.Modules;
using MskManager.Scrapper.Parsers;
using MskManager.Scrapper.Scrappers;
using MskManager.Scrapper.Test.Helpers;
using Nancy.Testing;
using NUnit.Framework;
using System.Collections.Generic;


namespace MskManager.Scrapper.Test
{
    [TestFixture]
    public class When_Call_Module
    {
        [Test]
        public void ScrapperModule_GetFip_ReturnSuccess()
        {
            var browser = GetBrowser("Fip", "fip-url", new FipParser());
            var response = browser.Get("/Radio/GetAsync/Fip");

            var actual = response.Body.AsString().Deserialize<Song>();

            Assert.IsFalse(actual.IsEmpty);
            Assert.AreEqual("L ACQUA CHIARA ALLA FONTANA", actual.Title);
            Assert.AreEqual("VINICIO CAPOSSELA", actual.Artist);
        }

        private Browser GetBrowser(string radioName, string radioUrl, IParser parser)
        {
            var configuration = ScrapperConfigurationBuilder.Create()
                .AddScrapper(radioName, radioUrl)
                .Build();

            var httpClient = HttpClientBuilder
                .Create()
                .WithGetAsync(radioUrl, FileLoader.Get($"Ressources.{radioName}.message.json"))
                .Build();

            var parsers = new[] { new FipParser() };

            return new Browser(cfg =>
            {
                cfg.Module<ScrapperModule>();
                cfg.Dependency(configuration);
                cfg.Dependency(httpClient);
                cfg.Dependency<IScrapper>(typeof(RadioScrapper));
                cfg.Dependency<IEnumerable<IParser>>(parsers);
            });
        }
    }
}
