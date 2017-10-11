using System;
using MskManager.Scrapper.Models;
using MskManager.Common.Exceptions;
using NLog;

namespace MskManager.Scrapper.Parsers
{
    public abstract class BaseParser : IParser
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public string Name { get; }
        protected abstract Func<string, Song> Parser { get; }

        protected BaseParser(string name)
        {
            Name = name;
        }

        public Song Parse(string value)
        {
            try
            {
                var song = Parser(value);
                LogSong(song);

                return song;
            }
            catch (Exception ex)
            {
                throw new ParsorException(Name, ex);
            }
        }

        private void LogSong(Song song)
        {
            if (_logger.IsTraceEnabled)
            {
                var message = song.IsEmpty
                    ? $"{Name}> Empty"
                    : $"{Name}> {nameof(song.Artist)}: {song.Artist} {nameof(song.Title)}: {song.Title}";

                _logger.Trace(message);
            }
        }
    }
}
