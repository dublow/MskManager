using MskManager.Common.Exceptions;
using MskManager.Common.Test.Http;
using MskManager.Scrapper.Parsers;
using MskManager.Scrapper.Scrappers;
using MskManager.Scrapper.Test.Helpers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MskManager.Scrapper.Test.Djam
{
    [TestFixture]
    public class When_Scrap_Djam
    {
        private const string uri = "djam";
        private const string fileName = "Ressources.Djam.message.json";

        [Test]
        public void When_scrap_with_valid_message_return_success()
        {
            var message = FileLoader.Get(fileName);

            var httpClient = HttpClientBuilder
                .Create()
                .WithGet(uri, message)
                .Build();

            var scrapper = new RadioScrapper(httpClient);

            var actual = scrapper.Scrap(uri, new DjamParser());

            Assert.AreEqual("Willie Wright", actual.Artist);
            Assert.AreEqual("Love Is Expensive", actual.Title);
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

            var actual = await scrapper.ScrapAsync(uri, new DjamParser());

            Assert.AreEqual("Willie Wright", actual.Artist);
            Assert.AreEqual("Love Is Expensive", actual.Title);
        }

        [Test]
        public void When_scrap_with_null_message_return_error()
        {
            var httpClient = HttpClientBuilder
                .Create()
                .WithGet(uri, null)
                .Build();

            var scrapper = new RadioScrapper(httpClient);

            var actual = Assert.Throws<ParsorException>(() => scrapper.Scrap(uri, new DjamParser()));
            Assert.AreEqual("Djam", actual.Message);
        }

        [Test]
        public void When_scrap_async_with_null_message_return_error()
        {
            var httpClient = HttpClientBuilder
                .Create()
                .WithGetAsync(uri, null)
                .Build();

            var scrapper = new RadioScrapper(httpClient);

            var actual = Assert.Throws<ParsorException>(() => scrapper.Scrap(uri, new DjamParser()));
            Assert.AreEqual("Djam", actual.Message);
        }
    }
}
