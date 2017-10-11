using System.Configuration;

namespace MskManager.Common.Configurations.Scrapper
{
    public class ScrapperSection : ConfigurationSection
    {
        [ConfigurationProperty("radios")]
        public RadioCollection Radios
        {
            get => (RadioCollection) this["radios"];
            set => this["radios"] = value;
        }
    }
}
