// See https://aka.ms/new-console-template for more information
using SHC.Core.Domain.Patient;
using SHC.Core.Domain.User;
using SHC.Core.Services;
using SHC.Infrastructure.Data;
Appointment app = new Appointment(Guid.NewGuid(), DateTime.Now.AddDays(3), Guid.NewGuid());
Appointment app2 = new Appointment(Guid.NewGuid(), DateTime.Now.AddDays(5), Guid.NewGuid());
Appointment app3 = new Appointment(Guid.NewGuid(), DateTime.Now.AddDays(3).AddMinutes(-20), Guid.NewGuid(), duration: TimeSpan.FromMinutes(30));
List<Appointment> appList = new List<Appointment>();
appList.Add(app);
appList.Add(app2);






Console.WriteLine("Hello, World!");
