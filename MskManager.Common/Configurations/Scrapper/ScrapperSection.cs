using System.Configuration;

namespace MskManager.Common.Configurations
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
