namespace MusicPlatform.Models.QuizModels
{
    public class QuizViewModel
    {
        public List<QuizQuestionModel> Questions { get; set; }
        public QuizViewModel()
        {
            Questions = new List<QuizQuestionModel>();
        }
    }
}
