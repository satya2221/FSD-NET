using Microsoft.AspNetCore.Mvc;

namespace _7._Intro_to_ASP;

[ApiController]
[Route("Api/[controller]")]
public class JobController(IJobService jobService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllJobAsync()
    {
        var result = await jobService.GetAllAsync();
         if(result is null || !result.Any())
        {
            throw new NullReferenceException("Job Data is Empty");
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobByIdAsync(Guid id)
    {
        var result = await jobService.GetByIdAsync(id);
        if(result is null)
        {
            throw new NullReferenceException($"Job with id {id} is Not Found");
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateJobAsync(JobRequestDto jobRequestDto)
    {
        await jobService.CreateAsync(jobRequestDto);
        return Ok();
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateJobAsync(Guid Id, JobRequestDto jobRequestDto)
    {
        var isUpdated = await jobService.UpdateAsync(Id, jobRequestDto);
        if(!isUpdated){
            throw new NullReferenceException($"Job with id {Id} Could Not Be Updated");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly Updated"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteJobAsync(Guid id)
    {
        var isDeleted = await jobService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new NullReferenceException($"Job with id {id} Could Not Be Deleted");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly deleted"));
    }
}
