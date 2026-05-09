namespace WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    [Required(ErrorMessage = "Pole OrganizerName jest wymagane.")]
    [StringLength(100, MinimumLength = 1)]
    public string? OrganizerName { get; set; }
    [Required(ErrorMessage = "Pole Topic jest wymagane.")]
    [StringLength(100, MinimumLength = 1)]
    public string? Topic { get; set; }
    public DateOnly Date { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Status { get; set; }
    
}