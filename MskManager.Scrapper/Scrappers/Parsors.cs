using MskManager.Common.Exceptions;
using MskManager.Common.Extensions;
using MskManager.Scrapper.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace MskManager.Scrapper.Scrappers
{
    public static class Parsors
    {
        public static Song Djam(string value)
        {
            return Create("Djam", value, v => {
                var message = value.Deserialize<Models.Djam.Message>();
                var track = message.Tracks.First();
                return new Song(track.Title, track.Artist);
            });
        }

        public static Song Fip(string value)
        {
            return Create("Fip", value, v => {
                var jObject = JObject.Parse(value);

                var level = jObject["levels"].First();
                var position = level["position"].Value<int>();

                var trackAsJson = jObject["steps"]
                    .ElementAt(position)
                    .First().ToString();

                var track = trackAsJson.Deserialize<Models.Fip.Track>();
                return new Song(track.Title, track.Authors);
            });
        }

        public static Song Nova(string value)
        {
            return Create("Nova", value, v => {
                var message = value.Deserialize<Models.Nova.Message>();
                return new Song(message.Track.Title, message.Track.Artist);
            });
        }

        private static Song Create(string parsorName, string value, Func<string, Song> parser)
        {
            try
            {
                return parser(value);
            }
            catch (Exception ex)
            {
                throw new ParsorException(parsorName, ex);
            }
        }
    }
}
