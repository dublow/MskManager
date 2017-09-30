using System.Collections.Generic;
using System.Configuration;

namespace MskManager.Common.Configurations
{
    public class ScrapperConfiguration : IScrapperConfiguration
    {
        private static readonly ScrapperSection Section;

        static ScrapperConfiguration()
        {
            Section = (ScrapperSection) ConfigurationManager.GetSection("scrapper");
        }

        public IEnumerable<IRadioConfiguration> RadioConfigurations => Section.Radios;
    }
}
