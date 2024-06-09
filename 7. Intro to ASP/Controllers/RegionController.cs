using Microsoft.AspNetCore.Mvc;
using Models;

namespace _7._Intro_to_ASP;

[ApiController]
[Route("Api/[controller]/")]
public class RegionController (IRegionService regionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRegionAsync()
    {
        var result = await regionService.GetAllAsync();
        if(result is null || !result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRegionByIdAsync(Guid id)
    {
        var result = await regionService.GetByIdAsync(id);
        if(result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegionAsync(Region region)
    {
        var result = await regionService.CreateAsync(region);
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateRegionAsync(Guid Id, Region region)
    {
        var isUpdated = await regionService.UpdateAsync(Id, region);
        if(!isUpdated){
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRegionAsync(Guid id)
    {
        var isDeleted = await regionService.DeleteAsync(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return Ok();
    }
}
