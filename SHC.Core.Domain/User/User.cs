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
        public IList<Roles> Roles { get; set; }
        public User() { }

        public User(Guid id, string hashedPassword, string phoneNumber, IList<Roles> roles)
        {
            Id = id;
            PhoneNumber = phoneNumber ?? throw new ArgumentException("Phone number is required");
            HashedPassword = hashedPassword ?? throw new ArgumentException("Hashed password is required");
            Roles = roles;
        }

        public void SetPassword(string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword)) throw new ArgumentException("Email is required");
            HashedPassword = hashedPassword;
        }

        public void AddRole(Roles role)
        {
            if (!Roles.Contains(role))
                Roles.Add(role);
        }
    }
}
