using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces.IServices;
using SHC.Core.Services.Exceptions;
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
        public List<Appointment> ValidateAppointment(Appointment appointment, IList<Appointment> patientAppointments) {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));

            if (appointment.DurationInMin <= 0)
                throw new Exception("Appointment duration must be positive.");

            if (appointment.AppointmentDate < DateTime.Now)
                throw new Exception("Appointment date cannot be in the past.");

            var newStart = appointment.AppointmentDate;

            var newEnd = appointment.AppointmentDate.AddMinutes(appointment.DurationInMin);

            List<Appointment> overlapped = patientAppointments.Where(a =>
                newStart <= a.AppointmentDate.AddMinutes(a.DurationInMin) &&
                a.AppointmentDate <= newEnd).ToList();

            return overlapped;
        }
    }
}
