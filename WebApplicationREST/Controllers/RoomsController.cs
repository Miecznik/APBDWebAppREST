using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    public static List<Room> rooms = new List<Room>()
    {
        new Room() { Id = 1, Name = "Room 1", Capacity = 15, BuildingCode = 1, Floor = 1, HasProjector = false, IsActive = false},
        new Room() { Id = 2, Name = "Room 2", Capacity = 21, BuildingCode = 1, Floor = 2,HasProjector = false, IsActive = false},
        new Room() { Id = 3, Name = "Room 3", Capacity = 25, BuildingCode = 1, Floor = 2,HasProjector = true, IsActive = true},
        new Room() { Id = 4, Name = "Room 4", Capacity = 12, BuildingCode = 2, Floor = 1, HasProjector = false, IsActive = false},
        new Room() { Id = 5, Name = "Room 5", Capacity = 28, BuildingCode = 2, Floor = 2,HasProjector = false, IsActive = false},
        new Room() { Id = 6, Name = "Room 6", Capacity = 22, BuildingCode = 2, Floor = 2,HasProjector = true, IsActive = false},
    };
    //wyszukiwanie wszystkich sal
    [HttpGet]
    public IActionResult Get(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector, 
        [FromQuery] bool? activeOnly
        )
    {
        if (minCapacity == null && hasProjector == null && activeOnly == null)
        {
            return Ok(rooms);
        }
        
        
        return Ok(
            rooms.
                Where(x => x.Capacity >= minCapacity  ).
                Where(x => x.HasProjector == hasProjector ).
                Where(x => x.IsActive == activeOnly ));
    }
    //wyszukiwanie po ID
    
    [Route("{id}")]
    [HttpGet]
    public IActionResult GetById([FromRoute] int id)
    {
        var room = rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound();
        }
            
        return Ok(room);
    }

    //building number
    [Route("building/{BuildingCode}")]
    [HttpGet]
    public IActionResult GetbyBuilding( [FromRoute] int buildingCode)
    {
        var room = from z in  rooms
            where z.BuildingCode.Equals(buildingCode)
            select z;
        
        if (room == null)
        {
            return NotFound();
        }
                    
        return Ok(room);
    }
    
    
    
    [HttpPost]
    public IActionResult PostRoom([FromBody] Room room)
    {
        rooms.Add(room);

        return CreatedAtAction(
            nameof(GetById),
            new { id = room.Id },
            room
        );
    }

    [HttpPut("{id}")]
    public ActionResult<Room> Update(int id, [FromBody] Room updated)
    {
        var old = rooms.FirstOrDefault(r => r.Id == id);
        if (old == null)
        {
            return NotFound("Brak sali o takim ID");
        }
        
        old.Name = updated.Name;
        old.BuildingCode = updated.BuildingCode;
        old.Floor = updated.Floor;
        old.Capacity = updated.Capacity;
        old.HasProjector = updated.HasProjector;
        old.IsActive = updated.IsActive;
        return Ok(old);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var room = rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound();

        rooms.Remove(room); 
        return NoContent();    }
}