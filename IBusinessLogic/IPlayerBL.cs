using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace IBusinessLogic
{
    public interface IPlayerBL
    {
        public List<Category> GetCategories();
        public List<CategoryElement> GetCategoryElements(int id);
        public Playlist GetPlaylist(int id);
        public PlayableContent GetPlayableContent(int id);
        public PlayableContent AddIndependentContent(PlayableContent playableContent);
        public void AddPlaylist(Playlist playlist);
        public Playlist AddContentToPlaylist(int playlistId, int contentId);
        public void DeleteContent(int id);
        public void DeletePlaylist(int id);
    }
}
