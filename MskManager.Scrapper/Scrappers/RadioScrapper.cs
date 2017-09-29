using MskManager.Common.Extensions;
using MskManager.Common.Http;
using MskManager.Scrapper.Models;
using MskManager.Scrapper.Models.Nova;
using System;
using System.Threading.Tasks;

namespace MskManager.Scrapper.Scrappers
{
    public class RadioScrapper : IScrapper
    {
        private readonly IHttpClient _httpClient;
        private readonly Func<string, Song> _parser;

        public RadioScrapper(IHttpClient httpClient, Func<string, Song> parser)
        {
            _httpClient = httpClient;
            _parser = parser;
        }

        public Song Scrap(string uri)
        {
            var result = _httpClient.Get(uri);
            return _parser(result);
        }

        public async Task<Song> ScrapAsync(string uri)
        {
            var result = await _httpClient.GetAsync(uri);
            return _parser(result);
        }

        
    }
}
