using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
using SHC.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//register validators
builder.Services.AddScoped<IValidator<RegisterPatientCommand>, RegisterPatientCommandValidator>();
builder.Services.AddScoped<IValidator<RegisterAppointmentCommand>, RegisterAppointmentCommandValidator>();

//register repositories
builder.Services.AddScoped<IPatientCommandRepository, PatientCommandRepository>();
builder.Services.AddScoped<IPatientQueryRepository, PatientQueryRepository>();

builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();


// register handlers 
builder.Services.AddScoped<IHandler<RegisterPatientCommand, Result<Patient>>, RegisterPatientHandler>();
builder.Services.AddScoped<IHandler<RegisterAppointmentCommand, Result<Unit>>, RegisterAppointmentHandler>();
builder.Services.AddScoped<IHandler<LoginCommand, Result<LoginResponseDTO>>, LoginHandler>();

//register domain services
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPatientService, PatientService>();



//register db context
builder.Services.AddDbContext<DbContext, SHCContext>(ServiceLifetime.Scoped);

//register middlewares
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
