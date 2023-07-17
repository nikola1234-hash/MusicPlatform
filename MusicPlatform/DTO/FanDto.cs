using System.ComponentModel.DataAnnotations;

namespace MusicPlatform.DTO
{
    public class FanDto : IValidatableObject
    {
        public string UserId { get; set; }
        public string ArtistId { get; set; }
        public FanDto()
        {
            
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(ArtistId))
            {
                yield return new ValidationResult("Invalid data");
            }
        }
    }
}
