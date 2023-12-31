﻿using MusicPlatform.Models;
using MusicPlatform.Models.HomeModels;

namespace MusicPlatform.Models.HomeModels
{
    public class HomeViewModel
    {
        public HomeViewModel(string artistsTitle, string songsTitle, List<ArtistModel> artists, List<SongModel> songs)
        {
            ArtistsTitle = artistsTitle;
            SongsTitle = songsTitle;
            Artists = artists;
            Songs = songs;
        }

        public string ArtistsTitle { get; set; }
        public string SongsTitle { get; set; }
        public List<ArtistModel> Artists { get; set; }
        public List<SongModel> Songs { get; set; }
    }
}
