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
        private readonly IScrapper _scrapper;
        private readonly IEnumerable<IParser> _parsers;

        public ScrapperModule(IScrapperConfiguration scrapperConfiguration, IScrapper scrapper, IEnumerable<IParser> parsers) : base("Radio")
        {
            _scrapperConfiguration = scrapperConfiguration;
            _scrapper = scrapper;
            _parsers = parsers;

            Get["/Get/{radio}"] = parameters =>
            {
                if(!TryGetRadio((string)parameters.radio, out IRadioConfiguration radioConfiguration))
                {
                    throw new NotFoundErrorException($"Radio {(string)parameters.radio} not found");
                }
                
                var parser = GetParser(radioConfiguration.Name);

                var song = _scrapper.Scrap(radioConfiguration.Uri, parser);

                return Response.AsJson(song);
            };
        }

        private bool TryGetRadio(string radioName, out IRadioConfiguration radioConfiguration)
        {
            radioConfiguration = null;
            var exists =_scrapperConfiguration.RadioConfigurations.Any(x => x.Name.ToLower() == radioName.ToLower());
            
            if(exists)
            {
                radioConfiguration = _scrapperConfiguration.RadioConfigurations.Single(x => x.Name.ToLower() == radioName.ToLower());
            }

            return exists;
        }

        private IParser GetParser(string radioName)
        {
            return _parsers.Single(p => p.Name.ToLower() == radioName.ToLower());
        }
    }
}
