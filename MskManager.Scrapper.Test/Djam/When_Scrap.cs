using MskManager.Common.Test.Http;
using MskManager.Scrapper.Scrappers;
using MskManager.Scrapper.Test.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Scrapper.Test.Djam
{
    [TestFixture]
    public class When_Scrap
    {
        private const string uri = "djam";
        private const string fileName = "Djam.Ressources.message.json";

        [Test]
        public void When_scrap_with_valid_message_must_be_success()
        {
            var message = FileLoader.Get(fileName);

            var httpClient = HttpClientBuilder
                .Create()
                .WithGet(uri, message)
                .Build();

            var scrapper = new DjamScrapper(httpClient);

            var actual = scrapper.Scrap(uri);

            Assert.AreEqual("Willie Wright", actual.Artist);
            Assert.AreEqual("Love Is Expensive", actual.Title);
        }

        [Test]
        public async Task When_scrap_async_with_valid_message_must_be_success()
        {
            var message = FileLoader.Get(fileName);

            var httpClient = HttpClientBuilder
                .Create()
                .WithGetAsync(uri, message)
                .Build();

            var scrapper = new DjamScrapper(httpClient);

            var actual = await scrapper.ScrapAsync(uri);

            Assert.AreEqual("Willie Wright", actual.Artist);
            Assert.AreEqual("Love Is Expensive", actual.Title);
        }
    }
}
