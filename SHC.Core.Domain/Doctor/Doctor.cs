using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Domain.Doctor;

public class Doctor
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Cin { get; private set; }
    public string Email { get; private set; }

    public Doctor(
        Guid id,
        Guid userId,
        string firstName,
        string lastName,
        string cin,
        string email)
    {
        Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
        UserId = userId != Guid.Empty ? userId : throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        Firstname = !string.IsNullOrWhiteSpace(firstName) ? firstName : throw new ArgumentException("First name is required.", nameof(firstName));
        Lastname = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new ArgumentException("Last name is required.", nameof(lastName));
        Cin = !string.IsNullOrWhiteSpace(cin) ? cin : throw new ArgumentException("CIN is required.", nameof(cin));
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentException("Email is required.", nameof(email));
    }
}
