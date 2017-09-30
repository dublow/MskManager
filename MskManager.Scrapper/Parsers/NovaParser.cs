using System;
using MskManager.Scrapper.Models;
using MskManager.Common.Extensions;

namespace MskManager.Scrapper.Parsers
{
    public class NovaParser : BaseParser
    {
        public NovaParser() : base("Nova")
        { }

        protected override Func<string, Song> Parser => (value) =>
        {
            var message = value.Deserialize<Models.Nova.Message>();

            if(message.Track == null)
            {
                return Song.Empty;
            }
            return new Song(message.Track.Title, message.Track.Artist);
        };
    }
}
