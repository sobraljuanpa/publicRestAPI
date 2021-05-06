using System.Collections.Generic;

namespace Domain
{
    public class Playlist : CategoryElement
    {
        public string Description { get; set; }

        public ICollection<PlayableContent> Contents { get; set; }
    }
}
