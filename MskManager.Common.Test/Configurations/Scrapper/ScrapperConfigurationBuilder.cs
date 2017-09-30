using Moq;
using MskManager.Common.Configurations;
using MskManager.Common.Configurations.Scrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Common.Test.Configurations.Scrapper
{
    public class ScrapperConfigurationBuilder
    {
        private readonly Mock<IScrapperConfiguration> _mock;
        private readonly Dictionary<string, string> _uris;

        private ScrapperConfigurationBuilder()
        {
            _mock = new Mock<IScrapperConfiguration>();
            _uris = new Dictionary<string, string>();
        }

        public static ScrapperConfigurationBuilder Create()
        {
            return new ScrapperConfigurationBuilder();
        }

        public ScrapperConfigurationBuilder AddScrapper(string name, string uri)
        {
            _uris.Add(name, uri);
            return this;
        }

        public IScrapperConfiguration Build()
        {
            var radioConfigurationMock = new Mock<IRadioConfiguration>();

            var radioConfigurations =_uris.Select(x =>
            {
                radioConfigurationMock.Setup(r => r.Name).Returns(x.Key);
                radioConfigurationMock.Setup(r => r.Uri).Returns(x.Value);

                return radioConfigurationMock.Object;
            });

            _mock.Setup(x => x.RadioConfigurations).Returns(radioConfigurations);

            return _mock.Object;
        }
    }
}
