using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Domain.Patient
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public bool IsUrgent { get; private set; }
        public int DurationInMin { get; private set; }
        public Guid AssignedDoctorId { get; private set; }
        public Guid PatientId;
        public Appointment(Guid id, DateTime appointmentDate, Guid assignedDoctorId, int durationInMin = 30, bool isUrgent = false)
        {
            Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
            AppointmentDate = appointmentDate != default ? appointmentDate : throw new ArgumentException("Appointment date is required.", nameof(appointmentDate));
            if (appointmentDate < DateTime.Now) throw new ArgumentException("Appointment date cannot be in the past.", nameof(appointmentDate));
            AssignedDoctorId = assignedDoctorId != Guid.Empty ? assignedDoctorId : throw new ArgumentException("Assigned doctor ID cannot be empty.", nameof(assignedDoctorId));
            IsUrgent = isUrgent;
            DurationInMin = durationInMin ;
        }
        public void UpdateAppointmentDate(DateTime newDate)
        {
            if (newDate == default) throw new ArgumentException("New appointment date is required.", nameof(newDate));
            if (newDate < DateTime.Now) throw new ArgumentException("New appointment date cannot be in the past.", nameof(newDate));
            AppointmentDate = newDate;
        }

        public void SetUrgency(bool isUrgent)
        {
            IsUrgent = isUrgent;
        }

        public void ReassignDoctor(Guid newDoctorId)
        {
            AssignedDoctorId = newDoctorId != Guid.Empty ? newDoctorId : throw new ArgumentException("New doctor ID cannot be empty.", nameof(newDoctorId));
        }

        public void SetDuration(int duration)
        {
            DurationInMin = duration > 0 ? duration : throw new ArgumentException("Duration cannot be zero or negative", nameof(duration));
        }
    }
}
