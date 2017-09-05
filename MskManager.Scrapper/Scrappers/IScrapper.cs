using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MskManager.Scrapper.Models;

namespace MskManager.Scrapper.Scrappers
{
    public interface IScrapper
    {
        Song Scrap();
    }
}
