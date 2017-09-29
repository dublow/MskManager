using System;
using MskManager.Scrapper.Models;
using MskManager.Common.Http;
using System.Threading.Tasks;
using MskManager.Scrapper.Models.Djam;
using System.Linq;

namespace MskManager.Scrapper.Scrappers
{
    public class DjamScrapper : IScrapper
    {
        private readonly IHttpClient _httpClient;

        public DjamScrapper(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Song Scrap(string uri)
        {
            var result = _httpClient.Get(uri);

            var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(result);

            var track = message.Tracks.First();

            return new Song(track.Title, track.Artist);
        }

        public async Task<Song> ScrapAsync(string uri)
        {
            var result = await _httpClient.GetAsync(uri);

            var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(result);

            var track = message.Tracks.First();

            return new Song(track.Title, track.Artist);
        }
    }
}
