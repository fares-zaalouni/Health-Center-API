using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHC.Core.Domain.Patient;
using SHC.Core.Domain.User;


namespace SHC.Infrastructure.Data
{
    public class SHCContext:DbContext
    {
        public DbSet<Patient> DBPatient { get; set; }
        public DbSet<Allergy> DBAllergy { get; set; }
        public DbSet<Appointment> DBAppointment { get; set; }
        public DbSet<MedicalCondition> DBMedicalCondition { get; set; }
        public DbSet<MedicationIntake> DBMedicationIntake { get; set; }
        public DbSet<MedicalPlan> DBMedicalPlan { get; set; }
        public DbSet<User> DBUser { get; set; }

        /*private AMContext _context;
        private AMContext()
        {
            _context = new AMContext();
        }
        public AMContext getInstance()
        {
            if(_context == null)
                _context = new AMContext();
            return _context;
        }*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb; 
                                                Initial Catalog = shc; 
                                                Integrated Security = true")
                .EnableSensitiveDataLogging()
                 .EnableDetailedErrors()
                 .LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new PlaneConfig());


            /*modelBuilder.Entity<Passenger>()
                .HasDiscriminator<int>("IsTraveller")
                .HasValue<Passenger>(0)
                .HasValue<Traveller>(1)
                .HasValue<Staff>(2);*/
            modelBuilder.Entity<Patient>()
               .HasOne<User>() // no navigation on Patient
               .WithOne()      // no navigation on User
               .HasForeignKey<Patient>(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne()
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Patient>()
                .HasMany<Allergy>("Allergies")
                .WithOne()
                .HasForeignKey("PatientId");

            modelBuilder.Entity<Patient>()
                .HasMany<MedicalCondition>("MedicalConditions")
                .WithOne()
                .HasForeignKey("PatientId");

            modelBuilder.Entity<Patient>()
                .HasMany<MedicalPlan>("MedicalPlans")
                .WithOne()
                .HasForeignKey("PatientId");

            modelBuilder.Entity<Patient>()
                .Property(p => p.UserId)
                .IsRequired();


            modelBuilder.Entity<Appointment>()
                .Property(a => a.Id)
                .ValueGeneratedNever();
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }
    }
}
