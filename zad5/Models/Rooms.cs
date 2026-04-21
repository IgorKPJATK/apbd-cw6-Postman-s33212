namespace zad5.Models;

using System.ComponentModel.DataAnnotations;

public class Rooms
{
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa sali jest wymagana.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Kod budynku jest wymagany.")]
        public string BuildingCode { get; set; }
        
        public int Floor { get; set; }
        
        [Range(1, 500, ErrorMessage = "Pojemność musi być między 1 a 500.")]
        public int Capacity { get; set; }
        
        public bool HasProjector { get; set; }
        public bool IsActive { get; set; }
}
