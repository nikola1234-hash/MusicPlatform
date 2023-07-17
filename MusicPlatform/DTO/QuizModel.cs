using MusicPlatform.Models.QuizModels;

namespace MusicPlatform.DTO
{
    public class QuizModel
    {
        public List<QuizQuestionModel> QuizQuestions { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public QuizModel()
        {
            
        }
    }
}
