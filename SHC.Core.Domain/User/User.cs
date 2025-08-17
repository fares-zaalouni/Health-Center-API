using Microsoft.AspNet.Identity;
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
        public string PhoneNumber { get; private set; }
        public string HashedPassword { get; private set; }
        public DateTime Dob { get; private set; }
        public User() { }

        public User(Guid id, DateTime dob, string hashedPassword, string phoneNumber)
        {
            Id = id;
            PhoneNumber = phoneNumber ?? throw new ArgumentException("Phone number is required");
            HashedPassword = hashedPassword ?? throw new ArgumentException("Hashed password is required");
            Dob = dob;
        }


        public void SetDob(DateTime dob)
        {
            if (dob > DateTime.UtcNow) throw new ArgumentException("Date of birth cannot be in the future");
            Dob = dob;
        }


        public void SetPassword(string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword)) throw new ArgumentException("Email is required");
            HashedPassword = hashedPassword;
        }
    }
}
