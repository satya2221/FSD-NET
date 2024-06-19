using Microsoft.AspNetCore.Mvc;
using Models;

namespace _7._Intro_to_ASP;

[ApiController]
[Route("Api/[controller]")]
public class CountryController (ICountryService countryService) :ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCountryAsync()
    {
        var result = await countryService.GetAllAsync();
        if(result is null || !result.Any())
        {
            throw new NullReferenceException("Country Data is Empty");
        }
        return Ok(new DataResponseDto<CountryResponseDto>(StatusCodes.Status200OK, "Data Found", result));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountryByIdAsync(Guid id)
    {
        var result = await countryService.GetByIdAsync(id);
        if(result is null)
        {
            throw new NullReferenceException($"Country with id {id} is Not Found");
        }
        return Ok(new SingleResponseDto<CountryResponseDto>(StatusCodes.Status200OK,"Data Found",result ) );
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountryAsync(CountryRequestDto countryRequestDto)
    {
        await countryService.CreateAsync(countryRequestDto);
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly created"));
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateCountryAsync(Guid Id, CountryRequestDto countryRequestDto)
    {
        var isUpdated = await countryService.UpdateAsync(Id, countryRequestDto);
        if(!isUpdated){
            throw new NullReferenceException($"Region with id {Id} Could Not Be Updated");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly updated"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCountryAsync(Guid id)
    {
        var isDeleted = await countryService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new NullReferenceException($"Region with id {id} Could Not Be Deleted");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly deleted"));
    }
}
