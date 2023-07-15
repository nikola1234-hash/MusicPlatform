using MusicPlatform.DTO;

namespace MusicPlatform.Data.Repository
{
    public interface ISongRepository
    {
        SongDto Add(SongDto songDto, Guid artistId);
        Task<SongDto> AddSongWithArtist(SongDto songDto, string artistName);
        void Delete(Guid id);
        List<SongDto> GetAll();
        List<SongDto> GetByArtistName(string name);
        SongDto GetById(Guid songId);
        SongDto GetById(string id);
        SongDto GetBySongName(string name);
        Task<List<SongDto>> GetSongByLyrics(string lyrics);
        SongDto Update(SongDto songDto);
    }
}