using Microsoft.AspNetCore.Mvc;

namespace _7._Intro_to_ASP;

[ApiController]
[Route("Api/[controller]")]
public class RoleController (IRoleService roleService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRoleAsync()
    {
        var result = await roleService.GetAllAsync();
         if(result is null || !result.Any())
        {
            throw new NullReferenceException("Role Data is Empty");
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleByIdAsync(Guid id)
    {
        var result = await roleService.GetByIdAsync(id);
        if(result is null)
        {
            throw new NullReferenceException($"Role with id {id} is Not Found");
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(RoleRequestDto roleRequestDto)
    {
        await roleService.CreateAsync(roleRequestDto);
        return Ok();
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateRoleAsync(Guid Id, RoleRequestDto roleRequestDto)
    {
        var isUpdated = await roleService.UpdateAsync(Id, roleRequestDto);
        if(!isUpdated){
            throw new NullReferenceException($"Role with id {Id} Could Not Be Updated");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly Updated"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoleAsync(Guid id)
    {
        var isDeleted = await roleService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new NullReferenceException($"Role with id {id} Could Not Be Deleted");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly deleted"));
    }
}
