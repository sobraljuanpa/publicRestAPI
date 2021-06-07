using System.Collections.Generic;

namespace Domain
{
    public class PlayableContent : CategoryElement
    {
        public string ImageURL { get; set; }

        public double Duration { get; set; }

        public string Author { get; set; }

        public string ContentURL { get; set; }

        public ICollection<Playlist> Playlists { get; set; }
    }
}
