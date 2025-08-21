using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Domain.Patient
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public DateTime Dob { get; private set; }
        public string? Cin { get; private set; }
        public string? Email { get; private set; }
        public string? EmergencyContactName { get; private set; }
        public string? EmergencyContactPhone { get; private set; }
        public BloodType? BloodType { get; private set; }
        public float? Weight { get; private set; }
        public float? Height { get; private set; }
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
        public IList<Allergy> Allergies { get; set; } = new List<Allergy>();
        public IList<MedicalCondition> MedicalConditions { get; set; } = new List<MedicalCondition>();
        public IList<MedicalPlan> MedicalPlans { get; set; } = new List<MedicalPlan>();

        public Patient(Guid id, Guid userId, string firstname, string lastname, DateTime dob, string? cin, string? email, string? emergencyContactName, string? emergencyContactPhone, BloodType? bloodType, float? weight, float? height)
        {
            Id = id;
            UserId = userId;
            Firstname = firstname;
            Lastname = lastname;
            Dob = dob;
            Cin = cin;
            Email = email;
            EmergencyContactName = emergencyContactName;
            EmergencyContactPhone = emergencyContactPhone;
            BloodType = bloodType;
            Weight = weight;
            Height = height;
        }
    }
}
