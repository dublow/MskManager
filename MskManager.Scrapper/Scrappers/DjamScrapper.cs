using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MskManager.Scrapper.Models;

namespace MskManager.Scrapper.Scrappers
{
    public class DjamScrapper : IScrapper
    {
        private readonly string _uri;

        public DjamScrapper(string uri)
        {
            _uri = uri;
        }

        public Song Scrap()
        {
            throw new NotImplementedException();
        }
    }
}
