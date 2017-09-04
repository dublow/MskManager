using System.Collections.Generic;

namespace MskManager.Scrapper.Configurations
{
    public interface IScrapperConfiguration
    {
        IEnumerable<IRadioConfiguration> RadioConfigurations { get; }
    }
}
