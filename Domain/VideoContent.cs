using System.Collections.Generic;

namespace Domain
{
    public class VideoContent : CategoryElement
    {

        public double Duration { get; set; }

        public string Author { get; set; }

        public string videoURL { get; set; }

        public ICollection<Playlist> Playlists { get; set; }
    }
}
