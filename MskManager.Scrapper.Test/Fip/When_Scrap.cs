using MskManager.Common.Test.Http;
using MskManager.Scrapper.Scrappers;
using MskManager.Scrapper.Test.Helpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MskManager.Scrapper.Test.Fip
{
    [TestFixture]
    public class When_Scrap
    {
        private const string uri = "fip";
        private const string fileName = "Fip.Ressources.message.json";

        [Test]
        public void When_scrap_with_valid_message_return_success()
        {
            var message = FileLoader.Get(fileName);

            var httpClient = HttpClientBuilder
                .Create()
                .WithGet(uri, message)
                .Build();

            var scrapper = new FipScrapper(httpClient);

            var actual = scrapper.Scrap(uri);

            Assert.AreEqual("VINICIO CAPOSSELA", actual.Artist);
            Assert.AreEqual("L ACQUA CHIARA ALLA FONTANA", actual.Title);
        }

        [Test]
        public async Task When_scrap_async_with_valid_message_return_success()
        {
            var message = FileLoader.Get(fileName);

            var httpClient = HttpClientBuilder
                .Create()
                .WithGetAsync(uri, message)
                .Build();

            var scrapper = new FipScrapper(httpClient);

            var actual = await scrapper.ScrapAsync(uri);

            Assert.AreEqual("VINICIO CAPOSSELA", actual.Artist);
            Assert.AreEqual("L ACQUA CHIARA ALLA FONTANA", actual.Title);
        }

        [Test]
        public void When_scrap_with_null_message_return_error()
        {
            var httpClient = HttpClientBuilder
                .Create()
                .WithGet(uri, null)
                .Build();

            var scrapper = new FipScrapper(httpClient);

            Assert.Throws<ArgumentNullException>(() => scrapper.Scrap(uri));
        }

        [Test]
        public void When_scrap_async_with_null_message_return_error()
        {
            var httpClient = HttpClientBuilder
                .Create()
                .WithGetAsync(uri, null)
                .Build();

            var scrapper = new FipScrapper(httpClient);

            Assert.ThrowsAsync<ArgumentNullException>(() => scrapper.ScrapAsync(uri));
        }
    }
}
