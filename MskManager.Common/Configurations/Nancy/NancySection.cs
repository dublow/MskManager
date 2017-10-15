using System.Configuration;

namespace MskManager.Common.Configurations.Nancy
{
    public class NancySection : ConfigurationSection
    {
        [ConfigurationProperty("domain", IsRequired = true)]
        public string Domain => (string)this["domain"];

        [ConfigurationProperty("port", IsRequired = true)]
        public string Port => (string)this["port"];
    }
}
