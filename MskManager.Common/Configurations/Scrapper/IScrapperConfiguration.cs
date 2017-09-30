using System.Collections.Generic;

namespace MskManager.Common.Configurations
{
    public interface IScrapperConfiguration
    {
        IEnumerable<IRadioConfiguration> RadioConfigurations { get; }
    }
}
