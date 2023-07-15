using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MusicPlatform.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusicPlatform.Models.HomeModels
{
    public class SongModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lyrics { get; set; }
        public Artist Artist { get; set; }
        public SongModel(Song song)
        {
            Id = song.Id;
            Name = song.Name;
            Lyrics = song.Lyrics;
            Artist = song.Artist;

        }
    }
}
