using System;
using MskManager.Scrapper.Models;
using MskManager.Common.Extensions;
using System.Linq;

namespace MskManager.Scrapper.Parsers
{
    public class DjamParser : BaseParser
    {
        public DjamParser() : base("Djam")
        { }

        protected override Func<string, Song> Parser => (value) =>
        {
            var message = value.Deserialize<Models.Djam.Message>();

            if(message == null || !message.Tracks.Any())
            {
                return Song.Empty;
            }

            var track = message.Tracks.FirstOrDefault();
            return new Song(track.Title, track.Artist);
        };
    }
}
