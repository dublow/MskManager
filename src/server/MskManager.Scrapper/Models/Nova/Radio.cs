using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Scrapper.Models.Nova
{
    public class Radio
    {
        public string Name { get; set; }
        [JsonProperty("is_on_air_activated")]
        public string IsOnAirActivated { get; set; }
        [JsonProperty("high_def_stream_url")]
        public string HighDefStreamUrl { get; set; }
        [JsonProperty("low_def_stream_url")]
        public string LowDefStreamUrl { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        [JsonProperty("background_image")]
        public string BackgroundImage { get; set; }
    }
}
