using SHC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.Commands;

public class RenewTokensCommand : ICommand
{
    public Guid UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string RefreshToken { get; set; }
    public Guid DeviceId { get; set; }
}
