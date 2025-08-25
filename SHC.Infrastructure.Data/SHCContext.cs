using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHC.Core.Domain.Admin;
using SHC.Core.Domain.Doctor;
using SHC.Core.Domain.Patient;
using SHC.Core.Domain.Secretary;
using SHC.Core.Domain.User;
using SHC.Infrastructure.Models;

namespace SHC.Infrastructure.Data;

public class SHCContext : DbContext
{
    public DbSet<User> DBUser { get; set; }
    public DbSet<Admin> DBAdmin { get; set; }
    public DbSet<Patient> DBPatient { get; set; }
    public DbSet<Doctor> DBDoctor { get; set; }
    public DbSet<Secretary> DBSecretary { get; set; }
    public DbSet<Allergy> DBAllergy { get; set; }
    public DbSet<Appointment> DBAppointment { get; set; }
    public DbSet<MedicalCondition> DBMedicalCondition { get; set; }
    public DbSet<MedicationIntake> DBMedicationIntake { get; set; }
    public DbSet<MedicalPlan> DBMedicalPlan { get; set; }
    public DbSet<RefreshToken> DBRefreshToken { get; set; }

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
        //optionsBuilder.UseLazyLoadingProxies();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .Property(a => a.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Doctor>()
            .Property(d => d.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Admin>()
            .Property(a => a.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Secretary>()
            .Property(s => s.Id)
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

        modelBuilder.Entity<RefreshToken>()
            .Property(rt => rt.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<RefreshToken>()
            .HasOne<RefreshToken>()         
            .WithOne()                     
            .HasForeignKey<RefreshToken>(rt => rt.ReplacedByToken) 
            .IsRequired(false);

        modelBuilder.Entity<User>()
           .HasMany<Patient>() 
           .WithOne()      
           .HasForeignKey(p => p.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
           .HasMany<RefreshToken>()
           .WithOne()
           .HasForeignKey(r => r.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Appointments)
            .WithOne()
            .HasForeignKey("PatientId");

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Allergies)
            .WithOne()
            .HasForeignKey("PatientId");

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.MedicalConditions)
            .WithOne()
            .HasForeignKey("PatientId");

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.MedicalPlans)
            .WithOne()
            .HasForeignKey("PatientId");

        modelBuilder.Entity<MedicalPlan>()
            .HasMany(mp => mp.MedicationIntakes)
            .WithOne()
            .HasForeignKey("MedicalPlanId");

        modelBuilder.Entity<Patient>()
            .Property(p => p.UserId)
            .IsRequired();

        modelBuilder.Entity<Doctor>()
            .Property(d => d.UserId)
            .IsRequired();

        modelBuilder.Entity<Doctor>()
            .HasOne<User>()
            .WithOne();

        modelBuilder.Entity<Admin>()
            .Property(a => a.UserId)
            .IsRequired();

        modelBuilder.Entity<Admin>()
            .HasOne<User>()
            .WithOne();

        modelBuilder.Entity<Secretary>()
            .Property(s => s.UserId)
            .IsRequired();

        modelBuilder.Entity<Secretary>()
            .HasOne<User>()
            .WithOne();
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        //configurationBuilder.Properties<DateTime>().HaveColumnType("date");
    }
}
