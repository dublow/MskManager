﻿using System.Threading.Tasks;
using MskManager.Scrapper.Models;

namespace MskManager.Scrapper.Scrappers
{
    public interface IScrapper
    {
        Song Scrap(string uri);
        Task<Song> ScrapAsync(string uri);
    }
}
