using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data;
using MusicPlatform.Data.Entities;
using MusicPlatform.Models.Quiz;
using MusicPlatform.Services.Api;

namespace MusicPlatform.Services.Quiz
{
    public class QuizService : IQuizService
    {
        private readonly AppDbContext _dbContext;
        private int maxQuestions = 3;
        private readonly IApiService apiService;

        public QuizService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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

            Random random = new Random();
            int number = random.Next(1, artists.Count);

            var topOneAlbum = await apiService.GetArtistTopAlbum(artists[number].Name);

            List<string> answers = new List<string>();
            answers.Add(topOneAlbum.Albums.FirstOrDefault().Name.ToString());
            for (int i = 0; i < 2; i++)
            {
                var album = await apiService.GetArtistTopAlbum(artists[number + i].Name);
                answers.Add(album.Albums.FirstOrDefault().Name.ToString());
            }

            for (int i = 0; i < maxQuestions; i++)
            {
                var question = new QuizQuestionModel();
                question.Question = "Which album is number one of Artist: " + artists[number].Name;
                question.Answers = GenerateAnswers(answers);
                question.CorrectAnswer = topOneAlbum.Albums.FirstOrDefault().Name.ToString();
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
            }

            return questions;


        }

        private async Task<List<QuizQuestionModel>> GenerateQuestionsByArtist()
        {
            List<QuizQuestionModel> questions = new List<QuizQuestionModel>();
            var songs = _dbContext.Songs.Include(s => s.Artist).ToList();

            Random random = new Random();
            int number = random.Next(1, songs.Count);

            var topSongs = await apiService.GetTopTrack(songs[number].Artist.Name);

            List<string> answers = new List<string>();
            answers.Add(topSongs.Tracks.FirstOrDefault().Name);
            for (int i = 0; i < 2; i++)
            {
                var song = await apiService.GetTopTrack(songs[number + i].Artist.Name);
                answers.Add(song.Tracks.FirstOrDefault().Name);
            }

            for (int i = 0; i < maxQuestions; i++)
            {
                var question = new QuizQuestionModel();
                question.Question = "Which song is number one of the Artist: " + songs[number].Name;
                question.Answers = GenerateAnswers(answers);
                question.CorrectAnswer = topSongs.Tracks.FirstOrDefault().Name;
                questions.Add(question);
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
