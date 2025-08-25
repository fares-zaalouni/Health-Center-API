using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Application.DTOs;
using SHC.Application.Handlers;
using SHC.Application.Validators;
using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces;
using SHC.Core.Interfaces.IRepositories;
using SHC.Core.Interfaces.IServices;
using SHC.Core.Services;
using SHC.Infrastructure.Data;
using SHC.Infrastructure.Data.Repositories;
using SHC.Infrastructure.Data.Repositories.Command_Repositories;
using SHC.Infrastructure.Data.Repositories.Query_Repositories;
using SHC.Infrastructure.Models;
using SHC.Infrastructure.Security.Authentication;
using SHC.Infrastructure.Security.JWT;
using SHC.Presentation.Middlewares;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//register validators
builder.Services.AddScoped<IValidator<RegisterPatientCommand>, RegisterPatientCommandValidator>();
builder.Services.AddScoped<IValidator<RegisterAppointmentCommand>, RegisterAppointmentCommandValidator>();
builder.Services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();

//register repositories
builder.Services.AddScoped<IPatientCommandRepository, PatientCommandRepository>();
builder.Services.AddScoped<IPatientQueryRepository, PatientQueryRepository>();

builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();

builder.Services.AddScoped<IDoctorQueryRepository, DoctorQueryRepository>();

builder.Services.AddScoped<ISecretaryQueryRepository, SecretaryQueryRepository>();

builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

// register handlers 
builder.Services.AddScoped<IHandler<RegisterPatientCommand, Result<Patient>>, RegisterPatientHandler>();
builder.Services.AddScoped<IHandler<RegisterAppointmentCommand, Result<Unit>>, RegisterAppointmentHandler>();
builder.Services.AddScoped<IHandler<LoginCommand, Result<LoginResponseDTO>>, LoginHandler>();
builder.Services.AddScoped<IHandler<RenewTokensCommand, Result<RenewTokensResponseDTO>>, RenewTokensHandler>();

//register domain services
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPatientService, PatientService>();


//register Security 
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuerSigningKey = true,
        };
    });


//register db context
builder.Services.AddDbContext<DbContext, SHCContext>(ServiceLifetime.Scoped);

//register middlewares
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // 🔹 Define the Bearer scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token in the format: Bearer {your token}"
    });

    // 🔹 Apply Bearer requirement to operations with [Authorize]
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
