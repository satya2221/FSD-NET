namespace _7._Intro_to_ASP;

public record MessageResponseDto(int Code = StatusCodes.Status404NotFound, string Message = "Data Not Found");