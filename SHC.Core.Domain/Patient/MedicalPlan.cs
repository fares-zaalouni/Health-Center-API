using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Domain.Patient
{
    public class MedicalPlan
    {
        public Guid Id { get; private set; }
        public string MedicationName { get; private set; }
        public float DailyDoze { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public MedicationType MedicationType { get; private set; }
        public virtual IList<MedicationIntake> MedicationIntakes { get; private set; }
        public Guid PatientId;

        public MedicalPlan(
            Guid id,
            string medicationName,
            float dailyDoze,
            DateOnly startDate,
            DateOnly endDate,
            MedicationType medicationType
            )
        {
            Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
            MedicationName = string.IsNullOrWhiteSpace(medicationName) ? throw new ArgumentException("Medication name is required and cannot be empty or whitespace.", nameof(medicationName)) : medicationName;
            DailyDoze = dailyDoze > 0 ? dailyDoze : throw new ArgumentException("Daily dose must be positive.", nameof(dailyDoze));
            StartDate = startDate < DateOnly.FromDateTime(DateTime.Now) ? throw new ArgumentException("Start date cannot be in the past.", nameof(startDate)) : startDate;
            EndDate = endDate <= startDate ? throw new ArgumentException("End date must be after start date.", nameof(endDate)) : endDate;
            MedicationType = medicationType != default ? medicationType : throw new ArgumentException("Medication type is required.", nameof(medicationType));
            MedicationIntakes = new List<MedicationIntake>();
        }

        public void UpdateMedicationName(string medicationName)
        {
            MedicationName = string.IsNullOrWhiteSpace(medicationName) ? throw new ArgumentException("Medication name is required and cannot be empty or whitespace.", nameof(medicationName)) : medicationName;
        }

        public void UpdateDailyDoze(float dailyDoze)
        {
            DailyDoze = dailyDoze > 0 ? dailyDoze : throw new ArgumentException("Daily dose must be positive.", nameof(dailyDoze));
        }

        public void UpdateStartDate(DateOnly startDate)
        {
            if (startDate < DateOnly.FromDateTime(DateTime.Now)) throw new ArgumentException("Start date cannot be in the past.", nameof(startDate));
            if (EndDate <= startDate) throw new ArgumentException("Start date must be before end date.", nameof(startDate));
            StartDate = startDate;
        }

        public void UpdateEndDate(DateOnly endDate)
        {
            if (endDate <= StartDate) throw new ArgumentException("End date must be after start date.", nameof(endDate));
            EndDate = endDate;
        }

        public void UpdateMedicationType(MedicationType medicationType)
        {
            MedicationType = medicationType != default ? medicationType : throw new ArgumentException("Medication type is required.", nameof(medicationType));
        }

        public void AddMedicationIntake(MedicationIntake intake)
        {
            if (intake == null) throw new ArgumentNullException(nameof(intake));
            MedicationIntakes.Add(intake);
        }

        public float CalculateCumulativeDose(DateOnly currentDate)
        {
            return MedicationIntakes
                .Where(i => i.IntakeTime.Date >= StartDate.ToDateTime(TimeOnly.MinValue) &&
                            i.IntakeTime.Date <= currentDate.ToDateTime(TimeOnly.MaxValue))
                .Sum(i => i.Doze);
        }
    }
}
