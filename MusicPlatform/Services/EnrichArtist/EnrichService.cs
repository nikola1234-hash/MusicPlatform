using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data;
using MusicPlatform.Data.Entities;
using MusicPlatform.DTO;

namespace MusicPlatform.Services.EnrichArtist
{
    public class EnrichService : IEnrichService
    {
        public void Enrich(Artist artist, ArtistResponseDto artistResponse, AppDbContext dbContext)
        {
            foreach (var image in artistResponse.Artist.Image)
            {
                var newImage = new ArtistImages(image.Url, image.Size);
                var entity = dbContext.ArtistImages.Add(newImage);
                artist.Images.Add(entity.Entity);
            }

            artist.Url = artistResponse.Artist.Url;
            artist.Listeners = artistResponse.Artist.Stats.Listeners.ToString();
            artist.Plays = artistResponse.Artist.Stats.Plays.ToString();
        }
    }
}
