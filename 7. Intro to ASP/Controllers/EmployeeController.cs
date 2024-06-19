using Microsoft.AspNetCore.Mvc;

namespace _7._Intro_to_ASP;

[ApiController]
[Route("Api/[controller]")]
public class EmployeeController (IEmployeeService employeeService) : ControllerBase
{
    [HttpGet("Details")]
    public async Task<IActionResult> GetAllEmployeeDetailAsync([FromQuery]EmployeeDetailRequestDto requestDto)
    {
        var result = await employeeService.GetEmployeeDetails(requestDto);
        return Ok(new DataPaginationResponseDto<EmployeeDetailResponseDto>(StatusCodes.Status200OK, "Data found.", requestDto.PageIndex,requestDto.PageSize, result.count , result.mapEmployeeDetail));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployeeAsync()
    {
        var result = await employeeService.GetAllAsync();
         if(result is null || !result.Any())
        {
            throw new NullReferenceException("Employee Data is Empty");
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeByIdAsync(Guid id)
    {
        var result = await employeeService.GetByIdAsync(id);
        if(result is null)
        {
            throw new NullReferenceException($"Employee with id {id} is Not Found");
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync(EmployeeRequestDto employeeRequestDto)
    {
        await employeeService.CreateAsync(employeeRequestDto);
        return Ok();
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateEmployeeAsync(Guid Id, EmployeeRequestDto employeeRequestDto)
    {
        var isUpdated = await employeeService.UpdateAsync(Id, employeeRequestDto);
        if(!isUpdated){
            throw new NullReferenceException($"Employee with id {Id} Could Not Be Updated");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly Updated"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
    {
        var isDeleted = await employeeService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new NullReferenceException($"Employee with id {id} Could Not Be Deleted");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly deleted"));
    }
}
