using MusicPlatform.Data;
using MusicPlatform.Data.Entities;
using MusicPlatform.DTO;

namespace MusicPlatform.Services.EnrichArtist
{
    public interface IEnrichService
    {
        void Enrich(Artist artist, ArtistResponseDto artistResponse, AppDbContext dbContext);
    }
}