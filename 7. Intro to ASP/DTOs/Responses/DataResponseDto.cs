namespace _7._Intro_to_ASP;

public record DataResponseDto<TResponse>(int Code, string Message, IEnumerable<TResponse> Responses);