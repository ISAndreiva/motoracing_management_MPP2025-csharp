using ConcursMotociclism.domain;
using ConcursMotociclism.dto;
using Microsoft.AspNetCore.Mvc;

namespace ConcursMotociclism.Service;

[ApiController]
[Route("rest/races")]
public class RestRaceController(IRaceController raceController) : ControllerBase
{
    private IRaceController _raceController = raceController;
    
    [HttpGet]
    public ActionResult<List<Race>> GetRaces()
    {
        return _raceController.GetAllRaces().ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Race> GetRace(string id)
    {
        if (!Guid.TryParse(id, out var raceId))
        {
            return BadRequest("ID is not a valid GUID");
        }

        var race = _raceController.GetRaceById(raceId);
        if (race == null)
        {
            return NotFound("Race not found");
        }
        return race;
    }

    [HttpPost]
    public ActionResult<Race> CreateRace([FromBody] RaceDto raceDto)
    {
        var race = new Race(Guid.NewGuid(), raceDto.RaceName, raceDto.RaceClass);
        try
        {
            _raceController.AddRace(race);
            return StatusCode(201, race.Id.ToString());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Race> UpdateRace(string id, [FromBody] RaceDto raceDto)
    {
        if (raceDto.Id != id)
        {
            return BadRequest("ID in URL does not match ID in body");
        }
        if (!Guid.TryParse(raceDto.Id, out var raceId))
        {
            return BadRequest("ID is not a valid GUID");
        }

        var race = _raceController.GetRaceById(raceId);
        if (race == null)
        {
            return NotFound("Race does not exist.");
        }
        race.RaceName = raceDto.RaceName;
        race.RaceClass = raceDto.RaceClass;
        try
        {
            _raceController.UpdateRace(race);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<Race> DeleteRace(string id)
    {
        if (!Guid.TryParse(id, out var raceId))
        {
            return BadRequest("ID is not a valid GUID");
        }

        if (_raceController.GetRaceById(raceId) == null)
        {
            return NotFound("Race does not exist.");
        }
        try
        {
            _raceController.DeleteRace(raceId);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}