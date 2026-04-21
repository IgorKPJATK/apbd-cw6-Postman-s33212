namespace zad5.Models;

using System.ComponentModel.DataAnnotations;

public class Reservation : IValidatableObject
{
        public int Id { get; set; }
        public int RoomId { get; set; }
        
        [Required]
        public string OrganizerName { get; set; }
        
        [Required]
        public string Topic { get; set; }
        
        [Required]
        public DateOnly Date { get; set; }
        
        [Required]
        public TimeSpan StartTime { get; set; }
        
        [Required]
        public TimeSpan EndTime { get; set; }
        
        public string Status { get; set; } 
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult("Godzina zakończenia musi być późniejsza niż rozpoczęcia.", new[] { nameof(EndTime) });
            }
        }
}
