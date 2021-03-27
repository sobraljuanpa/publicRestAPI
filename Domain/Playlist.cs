using System.Collections.Generic;

namespace Domain
{
    public class Playlist : CategoryElement
    {
        public string Description { get; set; }

        public List<PlayableContent> Contents { get; set; }
    }
}
