namespace _7._Intro_to_ASP;

public record SingleResponseDto<TEntity>(int Code, string Message, TEntity Data);
