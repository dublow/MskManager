using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MskManager.Scrapper.Models;
using MskManager.Common.Extensions;
using MskManager.Common.Http;
using Newtonsoft.Json.Linq;
using MskManager.Scrapper.Models.Fip;

namespace MskManager.Scrapper.Scrappers
{
    public class FipScrapper : IScrapper
    {
        private readonly IHttpClient _httpClient;

        public FipScrapper(IHttpClient httpClient)
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
            var jObject = JObject.Parse(value);

            var level = jObject["levels"].First();
            var position = level["position"].Value<int>();

            var trackAsJson = jObject["steps"]
                .ElementAt(position)
                .First().ToString();

            var track = trackAsJson.Deserialize<Track>();

            return new Song(track.Title, track.Authors);
        }
    }
}
