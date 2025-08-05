using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Domain.User
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Cin { get; private set; }
        public DateTime Dob { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        private User() { }

        public User(Guid id, string firstname, string lastname, string cin, DateTime dob, string email, string phoneNumber)
        {
            Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty");
            SetName(firstname, lastname);
            SetCin(cin);
            SetDob(dob);
            SetEmail(email);
            SetPhoneNumber(phoneNumber);
        }

        public void SetName(string firstname, string lastname)
        {
            if (string.IsNullOrWhiteSpace(firstname)) throw new ArgumentException("Firstname is required");
            if (string.IsNullOrWhiteSpace(lastname)) throw new ArgumentException("Lastname is required");

            Firstname = firstname;
            Lastname = lastname;
        }

        public void SetCin(string cin)
        {
            if (!cin.Any()) throw new ArgumentException("CIN must be a positive number");
            Cin = cin;
        }

        public void SetDob(DateTime dob)
        {
            if (dob > DateTime.UtcNow) throw new ArgumentException("Date of birth cannot be in the future");
            Dob = dob;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required");
            Email = email;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            if (!phoneNumber.Any()) throw new ArgumentException("Phone number must be positive");
            PhoneNumber = phoneNumber;
        }
    }
}
