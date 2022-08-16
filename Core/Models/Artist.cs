namespace Core.Models
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Music> Musics { get; set; } = new List<Music>();
    }
}
