﻿namespace _7._Intro_to_ASP;

public interface IGeneralService<TRequest, TResponse> 
{
    Task<IEnumerable<TResponse>?> GetAllAsync();
    Task<TResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(TRequest request);
    Task<bool> UpdateAsync(Guid id, TRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task CheckNullReferenceCustom<TEntityRef>(Guid id,IGeneralRepository<TEntityRef> repository, string propertyName)
        where TEntityRef : class;
}
