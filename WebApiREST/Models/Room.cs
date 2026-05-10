namespace WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
public class Room
{
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole Name jest wymagane.")]
        public string? Name { get; set; }
        [Required]
        public int? BuildingCode {get; set;}
        public int Floor {get; set;}
        [Range(1, int.MaxValue, ErrorMessage = "Capacity musi byc wieksze od zera.")]
        public int Capacity {get; set;}
        public bool HasProjector {get; set;}
        public bool IsActive {get; set;}
}