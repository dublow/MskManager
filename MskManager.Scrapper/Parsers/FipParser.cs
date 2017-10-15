using System;
using System.Linq;
using MskManager.Scrapper.Models;
using Newtonsoft.Json.Linq;
using MskManager.Common.Extensions;

namespace MskManager.Scrapper.Parsers
{
    public class FipParser : BaseParser
    {
        public FipParser() : base("Fip")
        { }

        protected override Func<string, Song> Parser =>  value =>
        {
            var jObject = JObject.Parse(value);

            var level = jObject["levels"].First();
            var position = level["position"].Value<int>();

            var trackAsJson = jObject["steps"]
                .ElementAt(position)
                .First().ToString();

            var track = trackAsJson.Deserialize<Models.Fip.Track>();

            return track == null 
                ? Song.Empty 
                : new Song(track.Title, track.Authors);
        };
    }
}
