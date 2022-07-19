using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Artist
    {
        public Artist()
        {
            Musics = new List<Music>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Music>? Musics { get; set; }
    }
}
