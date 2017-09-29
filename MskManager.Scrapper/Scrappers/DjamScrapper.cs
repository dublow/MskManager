using MskManager.Common.Extensions;
using MskManager.Common.Http;
using MskManager.Scrapper.Models;
using MskManager.Scrapper.Models.Djam;
using System.Linq;
using System.Threading.Tasks;

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
            return GetSong(result);
        }

        public async Task<Song> ScrapAsync(string uri)
        {
            var result = await _httpClient.GetAsync(uri);
            return GetSong(result);
        }

        private Song GetSong(string value)
        {
            var message = value.Deserialize<Message>();
            var track = message.Tracks.First();

            return new Song(track.Title, track.Artist);
        }
    }
}
