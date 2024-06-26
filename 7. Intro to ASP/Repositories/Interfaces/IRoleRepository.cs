﻿using Models;

namespace _7._Intro_to_ASP;

public interface IRoleRepository : IGeneralRepository<Role>
{
    Task<Guid> GetEmployeeRole();
}
