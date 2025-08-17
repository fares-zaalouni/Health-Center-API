using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Services.Exceptions
{
    public class AppointmentOverlapException : Exception
    {
        public AppointmentOverlapException( Guid overlapedAppointmet) : 
            base($"The new appointment overlaps with existing appointment  {overlapedAppointmet}") { }
    }
}
