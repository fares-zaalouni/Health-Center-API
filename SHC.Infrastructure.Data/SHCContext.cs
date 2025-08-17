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
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Patient>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Allergy>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<MedicalCondition>()
                .Property(m => m.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<MedicalPlan>()
                .Property(m => m.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<MedicationIntake>()
                .Property(m => m.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<User>()
               .HasMany<Patient>() 
               .WithOne()      
               .HasForeignKey(p => p.UserId)
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
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }
    }
}
