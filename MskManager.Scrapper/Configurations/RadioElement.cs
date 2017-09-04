using System.Configuration;

namespace MskManager.Scrapper.Configurations
{
    public class RadioElement : ConfigurationElement, IRadioConfiguration
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get => (string) base["name"];
            set => base["name"] = value;
        }

        [ConfigurationProperty("uri", IsRequired = true)]
        public string Uri {
            get => (string) this["uri"];
            set => base["uri"] = value;
        }
    }
}
