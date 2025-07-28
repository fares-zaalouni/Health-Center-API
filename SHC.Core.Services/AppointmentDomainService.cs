using SHC.Core.Domain.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Services
{
    public class AppointmentDomainService
    {
        public AppointmentDomainService() { }
        public void ValidateAppointment(Appointment appointment, List<Appointment> patientAppointments) {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            if (appointment.Duration <= TimeSpan.Zero) throw new Exception("Appointment duration must be positive.");
            if (appointment.AppointmentDate < DateTime.Now) throw new Exception("Appointment date cannot be in the past.");

            var newStart = appointment.AppointmentDate;
            var newEnd = appointment.AppointmentDate + appointment.Duration;

            bool hasOverlap = patientAppointments.Any(a =>
                newStart <= a.AppointmentDate + a.Duration &&
                a.AppointmentDate <= newEnd);

            if (hasOverlap) throw new Exception("Appointment overlaps with an existing appointment.");
        }
    }
}
