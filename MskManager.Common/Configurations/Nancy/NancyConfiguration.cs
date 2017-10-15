using System.Configuration;

namespace MskManager.Common.Configurations.Nancy
{
    public class NancyConfiguration : INancyConfiguration
    {
        private static readonly NancySection Section;

        static NancyConfiguration()
        {
            Section = (NancySection)ConfigurationManager.GetSection("nancy");
        }

        public string Domain => Section.Domain;
        public int Port => int.Parse(Section.Port);
    }
}
