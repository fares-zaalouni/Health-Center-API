using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Domain.Patient
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? EmergencyContactName { get; set; }
        public int? EmergencyContactPhone { get; set; }
        public BloodType? BloodType { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }
        public virtual IList<Appointment> Appointments { get; set; }
        public virtual IList<Allergy> Allergies { get; set; }
        public virtual IList<MedicalCondition> MedicalConditions { get; set; }
        public virtual IList<MedicalPlan> MedicalPlans { get; set; }

        public Patient(
            Guid id,
            string firstName,
            string lastName,
            string? emergencyContactName = null,
            int? emergencyContactPhone = null,
            BloodType? bloodType = null,
            float? weight = null,
            float? height = null)
        {
            Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName), "firstname is required.");
            EmergencyContactName = emergencyContactName;
            EmergencyContactPhone = emergencyContactPhone;
            BloodType = bloodType;
            Weight = weight;
            Height = height;
        }
    }
}
