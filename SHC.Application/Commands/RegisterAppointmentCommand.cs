using SHC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.Commands
{
    public class RegisterAppointmentCommand : ICommand
    {
        public DateTime AppointmentDate { get; set; }
        public bool IsUrgent { get; set; }
        public int Duration { get; set; }
        public Guid AssignedDoctorId { get; set; }
        public Guid PatientId { get; set; }
    }
}
