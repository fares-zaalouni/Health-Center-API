using Microsoft.EntityFrameworkCore;
using SHC.Core.Domain.Patient;


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
                                                Integrated Security = true");
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
            .HasMany<Appointment>("Appointments")
            .WithOne()
            .HasForeignKey("PatientId");

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

        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }
    }
}
