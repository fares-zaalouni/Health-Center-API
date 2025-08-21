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
    public DateTime Dob { get; private set; }
    public string Cin { get; private set; }
    public string Email { get; private set; }

    public Doctor(
        Guid id,
        Guid userId,
        string firstname,
        string lastname,
        DateTime dob,
        string cin,
        string email)
    {
        Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
        UserId = userId != Guid.Empty ? userId : throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        Firstname = !string.IsNullOrWhiteSpace(firstname) ? firstname : throw new ArgumentException("First name is required.", nameof(firstname));
        Lastname = !string.IsNullOrWhiteSpace(lastname) ? lastname : throw new ArgumentException("Last name is required.", nameof(lastname));
        Dob = dob != default ? dob : throw new ArgumentException("Date of birth is required.", nameof(dob));
        Cin = !string.IsNullOrWhiteSpace(cin) ? cin : throw new ArgumentException("CIN is required.", nameof(cin));
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentException("Email is required.", nameof(email));
    }
}
