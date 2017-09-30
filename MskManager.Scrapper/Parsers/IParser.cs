using MskManager.Scrapper.Models;
using System;

namespace MskManager.Scrapper.Parsers
{
    public interface IParser
    {
        string Name { get; }
        Song Parse(string value);
    }
}
