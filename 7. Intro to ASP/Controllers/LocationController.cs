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
            throw new NullReferenceException("Location Data is Empty");
        }
        return Ok(new DataResponseDto<LocationResponseDto>(StatusCodes.Status200OK, "Data Found", result));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLocationByIdAsync(Guid id)
    {
        var result = await locationService.GetByIdAsync(id);
        if(result is null)
        {
            throw new NullReferenceException($"Location with id {id} is Not Found");
        }
        return Ok(new SingleResponseDto<LocationResponseDto>(StatusCodes.Status200OK,"Data Found",result ) );
    }
    [HttpPost]
    public async Task<IActionResult> CreateLocationAsync(LocationRequestDto locationRequestDto)
    {
        await locationService.CreateAsync(locationRequestDto);
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly created"));
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateLocationAsync(Guid Id, LocationRequestDto locationRequestDto)
    {
        var isUpdated = await locationService.UpdateAsync(Id, locationRequestDto);
        if(!isUpdated){
            throw new NullReferenceException($"Location with id {Id} Could Not Be Updated");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly created"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLocationAsync(Guid id)
    {
        var isDeleted = await locationService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new NullReferenceException($"Location with id {id} Could Not Be Deleted");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly deleted"));
    }
}
