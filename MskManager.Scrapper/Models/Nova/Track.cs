using Newtonsoft.Json;
using System;

namespace MskManager.Scrapper.Models.Nova
{
    public class Track
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        [JsonProperty("diffusion_date")]
        public DateTime Diffusion { get; set; }
        public string Duration { get; set; }
        [JsonProperty("spotify_url")]
        public string SpotifyUrl { get; set; }
        [JsonProperty("deezer_url")]
        public string DeezerUrl { get; set; }
        [JsonProperty("itunes_url")]
        public string ItunesUrl { get; set; }
    }
}
