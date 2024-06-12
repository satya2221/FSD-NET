using Microsoft.AspNetCore.Mvc;

namespace _7._Intro_to_ASP;

[ApiController]
[Route("Api/[controller]")]
public class LocationController (ILocationService locationService) : ControllerBase 
{
    [HttpGet]
    public async Task<IActionResult> GetAllLocationAsync()
    {
        var result = await locationService.GetAllAsync();
         if(result is null || !result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLocationByIdAsync(Guid id)
    {
        var result = await locationService.GetByIdAsync(id);
        if(result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateLocationAsync(LocationRequestDto locationRequestDto)
    {
        await locationService.CreateAsync(locationRequestDto);
        return Ok();
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateLocationAsync(Guid Id, LocationRequestDto locationRequestDto)
    {
        var isUpdated = await locationService.UpdateAsync(Id, locationRequestDto);
        if(!isUpdated){
            return BadRequest();
        }
        return Ok();
    }
}
