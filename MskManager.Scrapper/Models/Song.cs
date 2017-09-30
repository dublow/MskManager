namespace MskManager.Scrapper.Models
{
    public class Song
    {
        public readonly string Title;
        public readonly string Artist;

        public static Song Empty => new Song(string.Empty, string.Empty);
        public bool IsEmpty => string.IsNullOrWhiteSpace(Title) && string.IsNullOrWhiteSpace(Artist);

        public Song(string title, string artist)
        {
            Title = title;
            Artist = artist;
        }
    }
}
