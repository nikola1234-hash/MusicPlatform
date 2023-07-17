namespace MusicPlatform.DTO
{
    public class AnswerModel
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public AnswerModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
