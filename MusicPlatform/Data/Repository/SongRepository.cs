using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data.Entities;
using MusicPlatform.Data.Repository.Infrastructure;
using MusicPlatform.DTO;
using MusicPlatform.Services.Api;
using MusicPlatform.Services.EnrichArtist;

namespace MusicPlatform.Data.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEnrichService _enrichService;
        private readonly IApiService _apiService;

        public SongRepository(AppDbContext context, IMapper mapper, IEnrichService enrichService)
        {
            _context = context;
            _mapper = mapper;
            _enrichService = enrichService;
        }

        public List<SongDto> GetAll()
        {
            var songs = _context.Songs.Include(s => s.Comments).Include(s => s.Favorites).ToList();
            var output = _mapper.Map<List<SongDto>>(songs);
            return output;
        }
        public SongDto GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(nameof(id));
            }

            bool parsed = Guid.TryParse(id, out Guid songId);
            if (!parsed)
            {
                throw new ArgumentException(nameof(songId));
            }


            var song = _context.Songs.Include(s => s.Comments).Include(s => s.Favorites).FirstOrDefault(s => s.Id == songId);
            if (song != null)
            {
                return _mapper.Map<SongDto>(song);
            }
            return null;
        }

        public SongDto GetById(Guid songId)
        {
            if (songId == Guid.Empty)
            {
                throw new ArgumentException(nameof(songId));
            }

            var song = _context.Songs.Include(s => s.Comments).Include(s => s.Favorites).FirstOrDefault(s => s.Id == songId);
            if (song != null)
            {
                return _mapper.Map<SongDto>(song);
            }
            return null;
        }


        public SongDto GetBySongName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }


            var song = _context.Songs.Include(s => s.Comments).Include(s => s.Favorites).FirstOrDefault(s => s.Name == name);
            if (song != null)
            {
                return _mapper.Map<SongDto>(song);
            }
            return null;
        }

        public List<SongDto> GetByArtistName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }
            var songs = _context.Songs.Include(s => s.Comments).Include(s => s.Favorites).Where(s => s.Artist.Name == name).ToList();
            if (songs == null)
            {
                return null;
            }
            if (songs.Count == 0)
            {
                return null;
            }
            return _mapper.Map<List<SongDto>>(songs);

        }

        public async Task<List<SongDto>> GetSongByLyrics(string lyrics)
        {
            if (string.IsNullOrEmpty(lyrics))
            {
                throw new ArgumentException(nameof(lyrics));
            }

            var songs = await _context.SearcSongsByLyrics(lyrics);
            return _mapper.Map<List<SongDto>>(songs);


        }
        public SongDto Add(SongDto songDto, Guid artistId)
        {
            if (songDto == null)
            {
                throw new ArgumentNullException(nameof(songDto));
            }
            if (artistId == Guid.Empty)
            {
                throw new ArgumentException(nameof(artistId));
            }

            Song song = new Song(songDto.Name, songDto.Lyrics, artistId);

            _context.Songs.Add(song);
            _context.SaveChanges();
            return _mapper.Map<SongDto>(song);
        }


        public async Task<SongDto> AddSongWithArtist(SongDto songDto, string artistName)
        {
            if (songDto == null)
            {
                throw new ArgumentNullException(nameof(songDto));
            }
            var artistExists = _context.Artists.Any(a => a.Name == artistName);
            Artist artist = null;
            if (artistExists)
            {
                artist = _context.Artists.FirstOrDefault(a => a.Name == artistName);
            }
            else
            {
                var artistResponse = await _apiService.GetArtistDetails(artistName);
                artist = new Artist(artistName);
                _enrichService.Enrich(artist, artistResponse, _context);
                _context.Artists.Add(artist);
                await _context.SaveChangesAsync();
            }
            var song = _mapper.Map<Song>(songDto);
            song.Artist = artist;
            _context.Songs.Add(song);
            _context.SaveChanges();
            return _mapper.Map<SongDto>(song);
        }

        public SongDto Update(SongDto songDto)
        {
            if (songDto == null)
            {
                throw new ArgumentNullException(nameof(songDto));
            }

            var song = _context.Songs.FirstOrDefault(s => s.Id == songDto.Id);
            if (song == null)
            {
                throw new ArgumentException(nameof(song));
            }

            song.Name = songDto.Name;
            song.Lyrics = songDto.Lyrics;
            _context.SaveChanges();
            return _mapper.Map<SongDto>(song);
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }

            var song = _context.Songs.FirstOrDefault(s => s.Id == id);
            if (song == null)
            {
                throw new ArgumentException(nameof(song));
            }

            _context.Songs.Remove(song);
            _context.SaveChanges();
        }

    }
}
