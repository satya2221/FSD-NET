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
            return NotFound();
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountryByIdAsync(Guid id)
    {
        var result = await countryService.GetByIdAsync(id);
        if(result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountryAsync(Country country)
    {
        var result = await countryService.CreateAsync(country);
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateCountryAsync(Guid Id, Country country)
    {
        var isUpdated = await countryService.UpdateAsync(Id, country);
        if(!isUpdated){
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCountryAsync(Guid id)
    {
        var isDeleted = await countryService.DeleteAsync(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return Ok();
    }
}
