using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Domain.Patient
{
    public class Allergy
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Allergent { get; private set; }
        public Guid PatientId;
        public AllergySeverity AllergySeverity { get; private set; }

        public Allergy(Guid id, string name, string allergent, AllergySeverity allergySeverity)
        {
            Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name is required and cannot be empty or whitespace.", nameof(name)) : name;
            Allergent = string.IsNullOrWhiteSpace(allergent) ? throw new ArgumentException("Allergent is required and cannot be empty or whitespace.", nameof(allergent)) : allergent;
            AllergySeverity = allergySeverity != default ? allergySeverity : throw new ArgumentException("Allergy severity is required.", nameof(allergySeverity));
        }

        public void UpdateName(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name is required and cannot be empty or whitespace.", nameof(name)) : name;
        }

        public void UpdateAllergent(string allergent)
        {
            Allergent = string.IsNullOrWhiteSpace(allergent) ? throw new ArgumentException("Allergent is required and cannot be empty or whitespace.", nameof(allergent)) : allergent;
        }

        public void UpdateSeverity(AllergySeverity allergySeverity)
        {
            AllergySeverity = allergySeverity != default ? allergySeverity : throw new ArgumentException("Allergy severity is required.", nameof(allergySeverity));
        }
    }
}

