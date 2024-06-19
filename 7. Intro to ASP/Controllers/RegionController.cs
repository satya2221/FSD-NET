using AutoMapper;
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
            throw new NullReferenceException("Region Data is Empty");
        }
        return Ok(new DataResponseDto<RegionResponseDto>(StatusCodes.Status200OK, "Data Found", result));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRegionByIdAsync(Guid id)
    {
        var result = await regionService.GetByIdAsync(id);
        if(result is null)
        {
            throw new NullReferenceException($"Region with id {id} is Not Found");
        }
        return Ok(new SingleResponseDto<RegionResponseDto>(StatusCodes.Status200OK, "Data Found", result) );
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegionAsync(RegionRequestDto regionRequestDto)
    {
        await regionService.CreateAsync(regionRequestDto);
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly created"));
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateRegionAsync(Guid Id, RegionRequestDto regionRequestDto)
    {
        var isUpdated = await regionService.UpdateAsync(Id, regionRequestDto);
        if(!isUpdated){
           throw new NullReferenceException($"Region with id {Id} Could Not Be Updated");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly updated"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRegionAsync(Guid id)
    {
        var isDeleted = await regionService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new NullReferenceException($"Region with id {id} Could Not Be Deleted");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly deleted"));
    }
}
