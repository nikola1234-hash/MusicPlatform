using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace MusicPlatform.Models.API.SongsModels
{
    public class FavoritesModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public FavoritesModel()
        {
            
        }

        public FavoritesModel(string id, string userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
