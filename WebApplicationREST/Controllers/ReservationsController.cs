using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;
[Route("api/[controller]")]
[ApiController]

public class ReservationsController : ControllerBase
{
    public static List<Reservation> reservations = new()
{
    new Reservation
    {
        Id = 1,
        Date = new DateOnly(2026, 05, 10),
        StartTime = new DateTime(2025, 05, 10, 8, 0, 0),
        EndTime = new DateTime(2025, 05, 10, 10, 0, 0),
        OrganizerName = "Jan Kowalski",
        RoomId = 2,
        Status = "confirmed",
        Topic = "Topic 1"
    },

    new Reservation
    {
        Id = 2,
        Date = new DateOnly(2026, 05, 11),
        StartTime = new DateTime(2025, 05, 11, 10, 30, 0),
        EndTime = new DateTime(2025, 05, 11, 12, 0, 0),
        OrganizerName = "Anna Nowak",
        RoomId = 2,
        Status = "confirmed",
        Topic = "Topic 2"
    },

    new Reservation
    {
        Id = 3,
        Date = new DateOnly(2026, 05, 12),
        StartTime = new DateTime(2025, 05, 12, 9, 0, 0),
        EndTime = new DateTime(2025, 05, 12, 11, 30, 0),
        OrganizerName = "Piotr Wiśniewski",
        RoomId = 3,
        Status = "cancelled",
        Topic = "Topic 3"
    },

    new Reservation
    {
        Id = 4,
        Date = new DateOnly(2026, 05, 13),
        StartTime = new DateTime(2025, 05, 13, 13, 0, 0),
        EndTime = new DateTime(2025, 05, 13, 15, 0, 0),
        OrganizerName = "Katarzyna Zielińska",
        RoomId = 1,
        Status = "confirmed",
        Topic = "Topic 4"
    },

    new Reservation
    {
        Id = 5,
        Date = new DateOnly(2026, 05, 14),
        StartTime = new DateTime(2025, 05, 14, 15, 30, 0),
        EndTime = new DateTime(2025, 05, 14, 17, 0, 0),
        OrganizerName = "Marek Lewandowski",
        RoomId = 4,
        Status = "confirmed",
        Topic = "Topic 5"
    },

    new Reservation
    {
        Id = 6,
        Date = new DateOnly(2026, 05, 15),
        StartTime = new DateTime(2025, 05, 15, 11, 0, 0),
        EndTime = new DateTime(2025, 05, 15, 13, 0, 0),
        OrganizerName = "Ewa Kamińska",
        RoomId = 2,
        Status = "confirmed",
        Topic = "Topic 6"
    }
};
    
    [HttpGet]
    public IActionResult Get(
        [FromQuery] DateOnly? date,
        [FromQuery] String? status, 
        [FromQuery] int? roomId
    )
    {
        if (date == null && status == null && roomId == null)
        {
            return Ok(reservations);
        }
        
        
        return Ok(
            reservations.
                Where(x => x.Date == date).
                Where(x => x.Status == status).
                Where(x => x.RoomId== roomId));
    }
    
    [Route("{id}")]
    [HttpGet]
    public IActionResult GetById([FromRoute] int id)
    {
        var reservation = reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }
            
        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult PostRoom([FromBody] Reservation reservation)
    {
        if (ReservationTimeConflict(reservation))
        {
            return BadRequest(
                "EndTime musi być późniejsze niż StartTime");
        }
        reservations.Add(reservation);

        return CreatedAtAction(
            nameof(GetById),
            new { id = reservation.Id },
            reservation
        );
    }
    
    [HttpPut("{id}")]
    public ActionResult<Room> Update(int id, [FromBody] Reservation reservation)
    {
        var old = reservations.FirstOrDefault(r => r.Id == id);
        if (old == null)
        {
            return NotFound("Brak rezerwacji o takim ID");
        }

        if (ReservationTimeConflict(reservation))
        {
            return BadRequest(
                "EndTime musi być późniejsze niż StartTime");
        }

        ReservationTimeConflict(reservation);
        old.RoomId = reservation.RoomId;
        old.OrganizerName = reservation.OrganizerName;
        old.Topic = reservation.Topic;
        old.Date = reservation.Date;
        old.StartTime = reservation.StartTime;
        old.EndTime = reservation.EndTime;
        old.Status = reservation.Status;
        
        return Ok(old);
    }
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var reservation = reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
            return NotFound();

        reservations.Remove(reservation); 
        return NoContent();    }

    public bool ReservationTimeConflict(Reservation reservation)
    {
        return reservation.StartTime >= reservation.EndTime ;

    }
    
}