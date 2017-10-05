using MskManager.Common.Extensions;
using MskManager.Common.Test.Configurations.Scrapper;
using MskManager.Common.Test.Http;
using MskManager.Scrapper.Models;
using MskManager.Scrapper.Modules;
using MskManager.Scrapper.Parsers;
using MskManager.Scrapper.Scrappers;
using MskManager.Scrapper.Test.Helpers;
using MskManager.Scrapper.Test.Models;
using Nancy.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MskManager.Scrapper.Test
{
    [TestFixture]
    public class When_Call_Module
    {
        [Test]
        public void ScrapperModule_GetFip_ReturnSuccess()
        {
            var radioModuleInfo = new RadioModuleInfo("Fip", "fip-url", "/Radio/GetAsync/Fip", new FipParser(),
                    new Song("L ACQUA CHIARA ALLA FONTANA", "VINICIO CAPOSSELA"));

            ScrapperModule_GetRadio<Song>(radioModuleInfo, SongAreEqual);
        }

        [Test]
        public void ScrapperModule_GetNova_ReturnSuccess()
        {
            var radioModuleInfo = new RadioModuleInfo("Nova", "nova-url", "/Radio/GetAsync/Nova", new NovaParser(),
                    new Song("SWAMP", "FUTURO PELO"));

            ScrapperModule_GetRadio<Song>(radioModuleInfo, SongAreEqual);
        }

        [Test]
        public void ScrapperModule_GetDjam_ReturnSuccess()
        {
            var radioModuleInfo = new RadioModuleInfo("Djam", "djam-url", "/Radio/GetAsync/Djam", new DjamParser(),
                    new Song("Love Is Expensive", "Willie Wright"));

            ScrapperModule_GetRadio<Song>(radioModuleInfo, SongAreEqual);
        }

        [Test]
        public void ScrapperModule_GetNotFound_ReturnNotFoundError()
        {
            var radioModuleInfo = new RadioModuleInfo("Djam", "djam-url", "/Radio/GetAsync/Uncknown", new DjamParser(),
                    new Song("Love Is Expensive", "Willie Wright"));

            var exception = Assert.Throws<Exception>(() => 
                ScrapperModule_GetRadio<object>(radioModuleInfo, null));

            Assert.IsNotNull(exception);
        }

        private void ScrapperModule_GetRadio<T>(RadioModuleInfo radioModuleInfo, Action<T, T> equal)
        {
            var browser = GetBrowser(radioModuleInfo.Name, radioModuleInfo.Url, radioModuleInfo.Parser);
            var response = browser.Get(radioModuleInfo.Endpoint);

            var actual = response.Body.AsString().Deserialize<T>();

            equal(actual, (T)Convert.ChangeType(radioModuleInfo.Expected, typeof(T)));
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

            var parsers = new[] { parser };

            return new Browser(cfg =>
            {
                cfg.Module<ScrapperModule>();
                cfg.Dependency(configuration);
                cfg.Dependency(httpClient);
                cfg.Dependency<IScrapper>(typeof(RadioScrapper));
                cfg.Dependency<IEnumerable<IParser>>(parsers);
            });
        }

        private void SongAreEqual(Song expected, Song actual)
        {
            Assert.AreEqual(expected.IsEmpty, actual.IsEmpty);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Artist, actual.Artist);
        }
    }
}
