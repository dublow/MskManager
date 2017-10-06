using MskManager.Scrapper.Models;
using MskManager.Scrapper.Parsers;

namespace MskManager.Scrapper.Test.Models
{
    public class RadioModuleInfo
    {
        public readonly string Name;
        public readonly string Url;
        public readonly string Endpoint;
        public readonly IParser Parser;
        public readonly object Expected;

        public RadioModuleInfo(string name, string url, string endpoint, IParser parser, object expected)
        {
            Name = name;
            Url = url;
            Endpoint = endpoint;
            Parser = parser;
            Expected = expected;
        }
    }
}
