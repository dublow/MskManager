using System.Threading.Tasks;
using MskManager.Scrapper.Models;
using MskManager.Scrapper.Parsers;

namespace MskManager.Scrapper.Scrappers
{
    public interface IScrapper
    {
        Song Scrap(string uri, IParser parser);
        Task<Song> ScrapAsync(string uri, IParser parser);
    }
}
