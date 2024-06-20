using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _7._Intro_to_ASP;

[ApiController]
[Route("Api/[controller]")]
[Authorize(Roles = "Employee")]
public class UserController(IUserService userService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync(LoginRequestDto requestDto)
    {
       var result = await userService.LoginUserAsync(requestDto);

        return Ok(new SingleResponseDto<string>(StatusCodes.Status200OK, "Login user is success.", result));
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterRequestDto registerRequestDto)
    {
        await userService.RegisterUserAsync(registerRequestDto);
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"User registered successfully"));
    }

    [HttpPost("AddRole")]
    public async Task<IActionResult> AddUserRole(UserRoleRequestDto requestDto)
    {
        await userService.AddUserRoleAsync(requestDto);
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"New User role added successfully"));
    }
    [HttpPost("RemoveRole")]
    public async Task<IActionResult> RemoveUserRole(UserRoleRequestDto requestDto)
    {
        await userService.RemoveUserRoleAsync(requestDto);
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"New User role removed successfully"));
    }

    [HttpPost("GenerateOtp")]
    public Task<IActionResult> GenerateOtpAsync(GenerateOtpRequestDto requestDto)
    {
        throw new NotImplementedException();
    }

    [HttpPost("ForgotPassword")]
    public Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequestDto requestDto)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserAsync()
    {
        var result = await userService.GetAllAsync();
         if(result is null || !result.Any())
        {
            throw new NullReferenceException("User Data is Empty");
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid id)
    {
        var result = await userService.GetByIdAsync(id);
        if(result is null)
        {
            throw new NullReferenceException($"User with id {id} is Not Found");
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(UserRequestDto userRequestDto)
    {
        await userService.CreateAsync(userRequestDto);
        return Ok();
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateUserAsync(Guid Id, UserRequestDto userRequestDto)
    {
        var isUpdated = await userService.UpdateAsync(Id, userRequestDto);
        if(!isUpdated){
            throw new NullReferenceException($"User with id {Id} Could Not Be Updated");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly Updated"));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var isDeleted = await userService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new NullReferenceException($"User with id {id} Could Not Be Deleted");
        }
        return Ok(new MessageResponseDto(StatusCodes.Status200OK,"Data Successfuly deleted"));
    }
}
