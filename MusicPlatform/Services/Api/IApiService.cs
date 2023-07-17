using MusicPlatform.DTO;

namespace MusicPlatform.Services.Api
{
    public interface IApiService
    {
        Task<ArtistResponseDto> GetArtistDetails(string artistName);
        Task<TopAlbumResponse> GetArtistTopAlbum(string artistName);
        Task<TopTracksResponse> GetTopTrack(string artistName);
    }
}