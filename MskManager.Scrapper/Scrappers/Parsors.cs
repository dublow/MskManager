using MskManager.Common.Extensions;
using MskManager.Scrapper.Models;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace MskManager.Scrapper.Scrappers
{
    public static class Parsors
    {
        public static Song Djam(string value)
        {
            var message = value.Deserialize<Models.Djam.Message>();
            var track = message.Tracks.First();

            return new Song(track.Title, track.Artist);
        }

        public static Song Fip(string value)
        {
            var jObject = JObject.Parse(value);

            var level = jObject["levels"].First();
            var position = level["position"].Value<int>();

            var trackAsJson = jObject["steps"]
                .ElementAt(position)
                .First().ToString();

            var track = trackAsJson.Deserialize<Models.Fip.Track>();

            return new Song(track.Title, track.Authors);
        }

        public static Song Nova(string value)
        {
            var message = value.Deserialize<Models.Nova.Message>();

            return new Song(message.Track.Title, message.Track.Artist);
        }
    }
}
