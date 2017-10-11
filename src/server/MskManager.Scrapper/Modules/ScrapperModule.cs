using System;
using MskManager.Common.Configurations.Scrapper;
using MskManager.Common.Nancy.HandleError.Exceptions;
using MskManager.Scrapper.Parsers;
using MskManager.Scrapper.Scrappers;
using Nancy;
using System.Collections.Generic;
using System.Linq;

namespace MskManager.Scrapper.Modules
{
    public class ScrapperModule : NancyModule
    {
        private readonly IScrapperConfiguration _scrapperConfiguration;
        private readonly IEnumerable<IParser> _parsers;

        public ScrapperModule(IScrapperConfiguration scrapperConfiguration, IScrapper scrapper,
            IEnumerable<IParser> parsers) : base("Radio")
        {
            _scrapperConfiguration = scrapperConfiguration;
            _parsers = parsers;

            Get["/Get/{radio}"] = parameters =>
            {
                if (!TryGetRadio((string) parameters.radio, out var radioConfiguration))
                {
                    throw new NotFoundErrorException($"Radio {(string) parameters.radio} not found");
                }

                var parser = GetParser(radioConfiguration.Name);

                var song = scrapper.Scrap(radioConfiguration.Uri, parser);

                return Response.AsJson(song);
            };

            Get["/GetAsync/{radio}", true] = async (parameters, ct) =>
            {
                if (!TryGetRadio((string)parameters.radio, out var radioConfiguration))
                {
                    throw new NotFoundErrorException($"Radio {(string)parameters.radio} not found");
                }

                var parser = GetParser(radioConfiguration.Name);

                var song = await scrapper.ScrapAsync(radioConfiguration.Uri, parser);

                return Response.AsJson(song);
            };
        }

        private bool TryGetRadio(string radioName, out IRadioConfiguration radioConfiguration)
        {
            radioConfiguration = null;

            var radioExists = _scrapperConfiguration.RadioConfigurations.Any(x =>
                String.Equals(x.Name, radioName, StringComparison.CurrentCultureIgnoreCase));
            
            if(radioExists)
            {
                radioConfiguration = _scrapperConfiguration.RadioConfigurations.Single(x =>
                    String.Equals(x.Name, radioName, StringComparison.CurrentCultureIgnoreCase));
            }

            return radioExists;
        }

        private IParser GetParser(string radioName)
        {
            return _parsers.Single(p => String.Equals(p.Name, radioName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
