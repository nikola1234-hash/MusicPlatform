using MusicPlatform.Models.QuizModels;

namespace MusicPlatform.Services.Quiz
{
    public interface IQuizService
    {
        Task<List<QuizQuestionModel>> GenerateQuestions();
    }
}