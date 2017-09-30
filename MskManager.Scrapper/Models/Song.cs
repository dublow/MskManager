namespace MskManager.Scrapper.Models
{
    public class Song
    {
        public readonly string Title;
        public readonly string Artist;

        public Song(string title, string artist)
        {
            Title = title;
            Artist = artist;
        }
    }
}
