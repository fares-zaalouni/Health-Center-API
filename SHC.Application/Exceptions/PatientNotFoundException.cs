using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException(Guid id):
            base($"Patient with id {id} does not exist") { }
    }
}
