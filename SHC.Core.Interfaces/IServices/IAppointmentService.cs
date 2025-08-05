using SHC.Core.Domain.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Interfaces.IServices
{
    public interface IAppointmentService
    {
        void ValidateAppointment(Appointment appointment, List<Appointment> patientAppointments);
    }
}
