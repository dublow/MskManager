using System.Collections.Generic;

namespace MskManager.Common.Configurations.Scrapper
{
    public interface IScrapperConfiguration
    {
        IEnumerable<IRadioConfiguration> RadioConfigurations { get; }
    }
}
