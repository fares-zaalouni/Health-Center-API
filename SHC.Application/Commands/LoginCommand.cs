using SHC.Core.Domain.User;
using SHC.Core.Interfaces;

namespace SHC.Application.Commands;

public class LoginCommand : ICommand
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; } 
}
