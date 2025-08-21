using SHC.Core.Interfaces;


namespace SHC.Application.Commands
{
    public class LoginCommand : ICommand
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string LoginType { get; set; } 
    }
}
