using System.ComponentModel.DataAnnotations;

namespace MusicPlatform.Models.API.SongsModels
{
    public class CommentModel : IValidatableObject
    {
        
        public string UserId { get; set; }
        public string SongId { get; set; }
        public string Comment { get; set; }
        public CommentModel()
        {
            
        }

        public CommentModel(string userId, string songId, string comment)
        {
            UserId = userId;
            SongId = songId;
            Comment = comment;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(SongId) || string.IsNullOrEmpty(Comment))
            {
                yield return new ValidationResult("Invalid data");
            }
        }
    }
}
