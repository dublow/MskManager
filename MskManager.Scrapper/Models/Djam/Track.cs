namespace MskManager.Scrapper.Models.Djam
{
    public class Track
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public int TimeToPlay { get; set; }
        public string[] Pictures { get; set; }
        public Spotify Spotify { get; set; }
        public Deezer Deezer { get; set; }
        public string Edito { get; set; }
        public int Trackpoints { get; set; }
        public int ArtistPoints { get; set; }
        public string Votable { get; set; }
    }
}
