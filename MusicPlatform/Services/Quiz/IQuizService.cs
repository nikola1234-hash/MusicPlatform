using MusicPlatform.Models.Quiz;

namespace MusicPlatform.Services.Quiz
{
    public interface IQuizService
    {
        Task<List<QuizQuestionModel>> GenerateQuestions();
    }
}