using MskManager.Common.Exceptions;
using MskManager.Common.Test.Http;
using MskManager.Scrapper.Parsers;
using MskManager.Scrapper.Scrappers;
using MskManager.Scrapper.Test.Helpers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MskManager.Scrapper.Test.Fip
{
    [TestFixture]
    public class When_Scrap
    {
        private const string uri = "fip";
        private const string fileName = "Ressources.Fip.message.json";

        [Test]
        public void When_scrap_with_valid_message_return_success()
        {
            var message = FileLoader.Get(fileName);

            var httpClient = HttpClientBuilder
                .Create()
                .WithGet(uri, message)
                .Build();

            var scrapper = new RadioScrapper(httpClient);

            var actual = scrapper.Scrap(uri, new FipParser());

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

            var scrapper = new RadioScrapper(httpClient);

            var actual = await scrapper.ScrapAsync(uri, new FipParser());

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

            var scrapper = new RadioScrapper(httpClient);

            var actual = Assert.Throws<ParsorException>(() => scrapper.Scrap(uri, new FipParser()));
            Assert.AreEqual("Fip", actual.Message);
        }

        [Test]
        public void When_scrap_async_with_null_message_return_error()
        {
            var httpClient = HttpClientBuilder
                .Create()
                .WithGetAsync(uri, null)
                .Build();

            var scrapper = new RadioScrapper(httpClient);

            var actual = Assert.Throws<ParsorException>(() => scrapper.Scrap(uri, new FipParser()));
            Assert.AreEqual("Fip", actual.Message);
        }
    }
}
