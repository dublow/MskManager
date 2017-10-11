using Newtonsoft.Json;

namespace MskManager.Scrapper.Models.Nova
{
    public class Message
    {
        public Radio Radio { get; set; }
        [JsonProperty("currentTrack")]
        public Track Track { get; set; }
    }
}
