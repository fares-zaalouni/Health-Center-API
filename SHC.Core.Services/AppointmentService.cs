using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        public AppointmentService() { }
        public void ValidateAppointment(Appointment appointment, List<Appointment> patientAppointments) {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));

            if (appointment.DurationInMin <= 0)
                throw new Exception("Appointment duration must be positive.");

            if (appointment.AppointmentDate < DateTime.Now)
                throw new Exception("Appointment date cannot be in the past.");

            var newStart = appointment.AppointmentDate;

            var newEnd = appointment.AppointmentDate.AddMinutes(appointment.DurationInMin);

            bool hasOverlap = patientAppointments.Any(a =>
                newStart <= a.AppointmentDate.AddMinutes(a.DurationInMin) &&
                a.AppointmentDate <= newEnd);

            if (hasOverlap) throw new Exception("Appointment overlaps with an existing appointment.");
        }
    }
}
