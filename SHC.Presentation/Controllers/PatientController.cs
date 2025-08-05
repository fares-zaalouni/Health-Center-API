using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Application.Handlers;
using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces;

namespace SHC.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IHandler<RegisterPatientCommand, Patient> _registerPatientHandler;
        private readonly IHandler<RegisterAppointmentCommand, Unit> _registerAppointmentHandler;

        public PatientController(
            IHandler<RegisterPatientCommand, Patient> registerPatientHandler,
            IHandler<RegisterAppointmentCommand, Unit> registerAppointmentHandler
            )
        {
            _registerPatientHandler = registerPatientHandler;
            _registerAppointmentHandler = registerAppointmentHandler;
        }
        [HttpPost]
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
            var result = await _registerAppointmentHandler.Handle(command);
            return Ok(result);
        }
    }
}
