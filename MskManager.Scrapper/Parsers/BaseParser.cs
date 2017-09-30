using System;
using MskManager.Scrapper.Models;
using MskManager.Common.Exceptions;

namespace MskManager.Scrapper.Parsers
{
    public abstract class BaseParser : IParser
    {
        private readonly string _name;
        public string Name => _name;

        protected abstract Func<string, Song> Parser { get; }

        public BaseParser(string name)
        {
            _name = name;
        }

        public Song Parse(string value)
        {
            try
            {
                return Parser(value);
            }
            catch (Exception ex)
            {
                throw new ParsorException(Name, ex);
            }
        }
    }
}
