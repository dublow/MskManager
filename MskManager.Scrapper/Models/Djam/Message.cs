using Newtonsoft.Json;
using System.Collections.Generic;

namespace MskManager.Scrapper.Models.Djam
{
    public class Message
    {
        public IEnumerable<Track> Tracks { get; set; }

        [JsonProperty("nb_listeners")]
        public int Listeners { get; set; }
    }
}
