using MskManager.Common.Http;
using MskManager.Scrapper.Models;
using MskManager.Scrapper.Parsers;
using System.Threading.Tasks;
using NLog;

namespace MskManager.Scrapper.Scrappers
{
    public class RadioScrapper : IScrapper
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IHttpClient _httpClient;

        public RadioScrapper(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Song Scrap(string uri, IParser parser)
        {
            var result = _httpClient.Get(uri);
            LogScrapper(uri, result);

            return parser.Parse(result);
        }

        public async Task<Song> ScrapAsync(string uri, IParser parser)
        {
            var result = await _httpClient.GetAsync(uri);
            return parser.Parse(result);
        }

        private void LogScrapper(string uri, string value)
        {
            if (_logger.IsTraceEnabled)
            {
                _logger.Trace($"{uri} {value}");
            }
        }
    }
}
