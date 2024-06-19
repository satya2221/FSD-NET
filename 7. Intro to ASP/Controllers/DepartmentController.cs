using Microsoft.AspNetCore.Mvc;

namespace _7._Intro_to_ASP;

[ApiController]
[Route("Api/[controller]")]
public class DepartmentController(IDepartmentService departmentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllDepartmentAsync()
    {
        var result = await departmentService.GetAllAsync();
         if(result is null || !result.Any())
        {
            throw new NullReferenceException("Department Data is Empty");
        }
        return Ok(new DataResponseDto<DepartmentResponseDto>(StatusCodes.Status200OK, "Data Found", result));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentByIdAsync(Guid id)
    {
        var result = await departmentService.GetByIdAsync(id);
        if(result is null)
        {
            throw new NullReferenceException($"Department with id {id} is Not Found");
        }
        return Ok(new SingleResponseDto<DepartmentResponseDto>(StatusCodes.Status200OK,"Data Found",result ) );
    }
    [HttpPost]
    public async Task<IActionResult> CreateDepartmentAsync(DepartmentRequestDto departmentRequestDto)
    {
        await departmentService.CreateAsync(departmentRequestDto);
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly created"));
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateDepartmentAsync(Guid Id, DepartmentRequestDto departmentRequestDto)
    {
        var isUpdated = await departmentService.UpdateAsync(Id, departmentRequestDto);
        if(!isUpdated){
            throw new NullReferenceException($"Department with id {Id} Could Not Be Updated");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly Updated"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDepartmentAsync(Guid id)
    {
        var isDeleted = await departmentService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new NullReferenceException($"Department with id {id} Could Not Be Deleted");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly deleted"));
    }
}
