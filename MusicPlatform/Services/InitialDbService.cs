using AutoMapper;
using MusicPlatform.Data.Entities;
using MusicPlatform.Data;
using MusicPlatform.DTO;
using MusicPlatform.Services.Csv;
using Microsoft.AspNetCore.SignalR;
using MusicPlatform.Services.Progress;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MusicPlatform.Models;
using System.Reflection.Metadata;
using MusicPlatform.CommonUtils.Constants;

namespace MusicPlatform.Services
{
    public class InitialDbService : IInitialDbService
    {

        public delegate void ProgressUpdateHandler(int progress);
        public event ProgressUpdateHandler ProgressUpdated;
        private readonly IHubContext<ProgressHub> _hubContext;
        private readonly IOptions<RecordsSettings> _options;

        public InitialDbService(IHubContext<ProgressHub> hubContext, IOptions<RecordsSettings> options)
        {
            _hubContext = hubContext;
            _options = options;
        }

        public async Task InitializeDatabase(IProgress<int> progress)
        {
            try
            {
                var context = ObjectResolverService.Resolve<AppDbContext>();
                var songs = context.Songs.ToList();
                if (songs.Count == 0)
                {
                    var csvService = ObjectResolverService.Resolve<ICsvService<CsvSongDto>>();
                    var file = Directory.GetCurrentDirectory() + Constants.CSV_PATH;

                    var records = csvService.ReadCSV(file).GetRecords().ToList();
                    records = LoadRandomRecords(records);

                    var totalRecords = records.Count;

                    for (int i = 0; i < records.Count; i++)
                    {
                        var record = records[i];
                        var artistName = record.artist;
                        var songName = record.song;
                        var songText = record.text;

                        // Check if the artist already exists in the database
                        var artist = await context.Artists.FirstOrDefaultAsync(a => a.Name == artistName);
                        if (artist == null)
                        {
                            // Create a new artist
                            artist = new Artist { Name = artistName };
                            await context.Artists.AddAsync(artist);
                        }

                        // Create a new song
                        var song = new Song
                        {
                            Name = songName,
                            Lyrics = songText,
                            Artist = artist
                        };

                        await context.Songs.AddAsync(song);

                        var progressPercentage = (int)(((i + 1) / (double)totalRecords) * 100);
                        progress?.Report(progressPercentage);
                        var recordsDone = new
                        {
                            current = i + 1,
                            total = totalRecords
                        };
                        await _hubContext.Clients.All.SendAsync("RecordsUpdate", recordsDone);

                        // await Task.Delay(100); // Adjust the delay duration if needed
                    }
                    await _hubContext.Clients.All.SendAsync("Done", null);
                    // Save the changes to the database
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // TODO: Log exception
            }
        }
        private List<CsvSongDto> LoadRandomRecords(List<CsvSongDto> records)
        {
            // Shuffle the list of records
            Random rng = new Random();
            List<CsvSongDto> shuffledRecords = records.OrderBy(record => rng.Next()).ToList();

            // Select the first 500 records from the shuffled list
            List<CsvSongDto> randomRecords = shuffledRecords.Take(_options.Value.TotalRecords).ToList();

            return randomRecords;
        }

    }
}
