using _7._Intro_to_ASP;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>{
        options.InvalidModelStateResponseFactory = context =>
        {
            var errorFields = context.ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?
                        .Errors
                        .Select(error =>error.ErrorMessage)
                        .ToArray()
            );
            var erroResponse = new ErrorResponseDto(StatusCodes.Status400BadRequest,
                                    "Bad Request, one or more validation errors",
                                    errorFields
                                );
            return new BadRequestObjectResult(erroResponse);
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http, // Ini yang bikin bisa tanpa embel embel 'Bearer' di depan
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EmployeeDbContext>(option => option.UseSqlServer(connectionString));

#region add scope Repository
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
#endregion

#region add scope Service
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
#endregion

builder.Services.AddAutoMapper(typeof(Program).Assembly);

#region Utilities config
builder.Services.AddExceptionHandler<GlobalExceptionHandlerCustom>();
builder.Services.AddTransient<ITokenHandler, _7._Intro_to_ASP.TokenHandler>(
    _ => new _7._Intro_to_ASP.TokenHandler(
        builder.Configuration["JWTService:Key"],
        builder.Configuration["JWTService:Issuer"],
        builder.Configuration["JWTService:Audience"],
        Convert.ToInt32(builder.Configuration["JWTService:DurationInMinute"])
    )
);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x => {
        x.TokenValidationParameters = new (){
            ValidIssuer = builder.Configuration["JWTService:Issuer"],
            ValidAudience = builder.Configuration["JWTService:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTService:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew= TimeSpan.Zero
        };
    });
#endregion

builder.Services.AddFluentValidationAutoValidation()
.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(_ => {});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
