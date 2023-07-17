namespace MusicPlatform.Models.Quiz
{
    public class QuizQuestionModel
    {
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
        
        public QuizQuestionModel()
        {
            
        }
    }
}
