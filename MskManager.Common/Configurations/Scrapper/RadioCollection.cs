using System.Collections.Generic;
using System.Configuration;

namespace MskManager.Common.Configurations
{
    public class RadioCollection: ConfigurationElementCollection, IEnumerable<IRadioConfiguration>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RadioElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IRadioConfiguration) element).Name;
        }

        IEnumerator<IRadioConfiguration> IEnumerable<IRadioConfiguration>.GetEnumerator()
        {
            foreach (var baseGetAllKey in this.BaseGetAllKeys())
            {
                yield return (IRadioConfiguration) BaseGet(baseGetAllKey);
            }
        }
    }
}
