using MskManager.Scrapper.Models;

namespace MskManager.Scrapper.Parsers
{
    public interface IParser
    {
        string Name { get; }
        Song Parse(string value);
    }
}
