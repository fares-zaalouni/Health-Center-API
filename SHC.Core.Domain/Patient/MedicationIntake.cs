using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Domain.Patient
{
    public class MedicationIntake
    {
        public Guid Id { get; private set; }
        public float Doze { get; private set; }
        public DateTime IntakeTime { get; private set; }
        public Guid PatientId;

        public MedicationIntake(Guid id, float doze, DateTime intakeTime)
        {
            Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
            Doze = doze > 0 ? doze : throw new ArgumentException("Dose must be positive.", nameof(doze));
            IntakeTime = intakeTime != default ? intakeTime : throw new ArgumentException("Intake time is required.", nameof(intakeTime));
            if (intakeTime > DateTime.Now) throw new ArgumentException("Intake time cannot be in the future.", nameof(intakeTime));
        }

        public void UpdateDoze(float doze)
        {
            Doze = doze > 0 ? doze : throw new ArgumentException("Dose must be positive.", nameof(doze));
        }

        public void UpdateIntakeTime(DateTime intakeTime)
        {
            if (intakeTime == default) throw new ArgumentException("Intake time is required.", nameof(intakeTime));
            if (intakeTime > DateTime.Now) throw new ArgumentException("Intake time cannot be in the future.", nameof(intakeTime));
            IntakeTime = intakeTime;
        }
    }
}
