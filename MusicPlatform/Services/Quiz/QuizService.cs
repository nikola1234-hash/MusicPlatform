using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data;
using MusicPlatform.Data.Entities;
using MusicPlatform.Models.QuizModels;
using MusicPlatform.Services.Api;

namespace MusicPlatform.Services.Quiz
{
    public class QuizService : IQuizService
    {
        private readonly AppDbContext _dbContext;
        private int maxQuestions = 3;
        private readonly IApiService _apiService;

        public QuizService(AppDbContext dbContext, IApiService apiService)
        {
            _dbContext = dbContext;
            _apiService = apiService;
        }

        private int QuestionType()
        {
            Random random = new Random();
            int number = random.Next(1, 3);
            return number;
        }

        public async Task<List<QuizQuestionModel>> GenerateQuestions()
        {
            int questionType = QuestionType();

            switch (questionType)
            {
                case 1:
                    return await GenerateQuestionsByArtist();
                case 2:
                    return await GenerateQuestionsAlbum();
                case 3:
                    return GenerateQuestionsBySong();
                default:
                    return GenerateQuestionsBySong();
            }


        }

        private async Task<List<QuizQuestionModel>> GenerateQuestionsAlbum()
        {
            List<QuizQuestionModel> questions = new List<QuizQuestionModel>();
            var artists = _dbContext.Artists.ToList();


            for (int i = 0; i < maxQuestions; i++)
            {
                Random random = new Random();
                int number = random.Next(1, artists.Count);

                var topOneAlbum = await _apiService.GetArtistTopAlbum(artists[number].Name);

                while (topOneAlbum == null)
                {
                    topOneAlbum = await _apiService.GetArtistTopAlbum(artists[number + 1].Name);

                    if (number == artists.Count)
                    {
                        number = 0;
                    }
                }
                List<string> answers = new List<string>();
                answers.Add(topOneAlbum.TopAlbum.Albums.FirstOrDefault().Name.ToString());
                for (int j = 0; j < 2; j++)
                {
                    var album = await _apiService.GetArtistTopAlbum(artists[number + 1].Name);
                    var rand = new Random();
                    answers.Add(album.TopAlbum.Albums[rand.Next(album.TopAlbum.Albums.Count)].Name.ToString());
                }

                var question = new QuizQuestionModel();
                question.Question = "Which album is number one of Artist: " + artists[number].Name;
                question.Answers = GenerateAnswers(answers);
                question.CorrectAnswer = topOneAlbum.TopAlbum.Albums.FirstOrDefault().Name.ToString();
                questions.Add(question);
            }

            return questions;
        }

        private List<QuizQuestionModel> GenerateQuestionsBySong()
        {
            List<QuizQuestionModel> questions = new List<QuizQuestionModel>();
            var songs = _dbContext.Songs.Include(s => s.Artist).ToList();

            Random random = new Random();
            int number = random.Next(1, songs.Count);

            for (int i = 0; i < maxQuestions; i++)
            {
                
                var question = new QuizQuestionModel();
                question.Question = "Who is the artist of the song: " + songs[number].Name;
                question.Answers = GenerateAnswers(songs[number]);
                question.CorrectAnswer = songs[number].Artist.Name;
                questions.Add(question);
                number = random.Next(1, songs.Count);
            }

            return questions;


        }

        private async Task<List<QuizQuestionModel>> GenerateQuestionsByArtist()
        {
            List<QuizQuestionModel> questions = new List<QuizQuestionModel>();
            var songs = _dbContext.Songs.Include(s => s.Artist).ToList();


            for (int i = 0; i < maxQuestions; i++)
            {

                Random random = new Random();
                int number = random.Next(1, songs.Count);

                var topSongs = await _apiService.GetTopTrack(songs[number].Artist.Name);
                while (topSongs == null)
                {
                    topSongs = await _apiService.GetTopTrack(songs[number + 1].Artist.Name);
                    if (number == songs.Count)
                    {
                        number = 0;
                    }
                }

                List<string> answers = new List<string>();
                answers.Add(topSongs.TopTracks.Tracks.FirstOrDefault().Name);
                for (int j = 0; j < 2; j++)
                {
                    var song = await _apiService.GetTopTrack(songs[number + 1].Artist.Name);
                    var rand = new Random();
                    answers.Add(song.TopTracks.Tracks[rand.Next(song.TopTracks.Tracks.Count)].Name);
                }
                var question = new QuizQuestionModel();
                question.Question = "Which song is number one of the Artist: " + songs[number].Name;
                question.Answers = GenerateAnswers(answers);
                question.CorrectAnswer = topSongs.TopTracks.Tracks.FirstOrDefault().Name;
                questions.Add(question);
                number = random.Next(1, songs.Count);
            }

            return questions;
        }

        private List<string> GenerateAnswers(Song song)
        {
            var artists = _dbContext.Artists.ToList();
            Random random = new Random();
            var number = random.Next(1, artists.Count);

            List<string> answer = new List<string>();
            answer.Add(song.Artist.Name);
            answer.Add(artists[number].Name);
            answer.Add(artists[number + 1].Name);

            return ShakeList(answer);

        }

        private List<string> GenerateAnswers(List<string> answer)
        {


            return ShakeList(answer);

        }

        private List<string> ShakeList(List<string> asnwers)
        {
            Random random = new Random();
            int n = asnwers.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string value = asnwers[k];
                asnwers[k] = asnwers[n];
                asnwers[n] = value;
            }

            return asnwers;
        }
    }




}
