using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Application.Handlers;
using SHC.Core.Domain.Patient;
using SHC.Core.Domain.User;
using SHC.Core.Interfaces;
using SHC.Core.Interfaces.IRepositories;

namespace SHC.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IHandler<RegisterPatientCommand, Result<Patient>> _registerPatientHandler;
    private readonly IHandler<RegisterAppointmentCommand, Result<Unit>> _registerAppointmentHandler;
    // REMOVE USER REPOSITORY LATER , ONLY FOR TEST
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUnitOfWork _unitOfWork;
    public PatientController(
        IHandler<RegisterPatientCommand, Result<Patient>> registerPatientHandler,
        IHandler<RegisterAppointmentCommand, Result<Unit>> registerAppointmentHandler,
        IUserCommandRepository userCommandRepository,
        IUnitOfWork unitOfWork
        )
    {
        _registerPatientHandler = registerPatientHandler;
        _registerAppointmentHandler = registerAppointmentHandler;
        _userCommandRepository = userCommandRepository;
        _unitOfWork = unitOfWork;
    }
    [HttpPost]
    [Authorize(Roles = nameof(Roles.Secretary))]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterPatientCommand command)
    {
        var result = await _registerPatientHandler.Handle(command);
        return Ok(result);
    }

    [HttpPost]
    [Route("{id}/appointments/register")]
    public async Task<IActionResult> RegisterAppointment(Guid id, RegisterAppointmentCommand command)
    {
        command.PatientId = id;
        Result<Unit> result = await _registerAppointmentHandler.Handle(command);
        if(result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok();
    }

    [HttpGet]
    [Route("test-user/{phone}/{role}")]
    public async Task<IActionResult> TestUser(string phone, Roles role)
    {
        PasswordHasher<string> hasher = new PasswordHasher<string>();
        User user = new User(
            Guid.NewGuid(),
            hasher.HashPassword(null, "Fares852"),
            phone,
            [role]
            );
        await _userCommandRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();
        return Ok(user);
    }

}
