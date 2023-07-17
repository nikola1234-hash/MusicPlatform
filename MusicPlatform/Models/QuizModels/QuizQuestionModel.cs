namespace MusicPlatform.Models.QuizModels
{
    public class QuizQuestionModel
    {
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public Guid Id { get; set; }

        public QuizQuestionModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
