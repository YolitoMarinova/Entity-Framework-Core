using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions options)
             : base(options)
        {
        }

        public HospitalDbContext()
        {
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity
                    .HasKey(d => d.DoctorId);

                entity
                    .Property(n => n.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true);

                entity
                    .Property(s => s.Specialty)
                    .HasMaxLength(100)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<Patient>(entity =>
               {
                   entity
                     .HasKey(p => p.PatientId);

                   entity
                    .Property(n => n.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .IsRequired(true);

                   entity
                    .Property(n => n.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .IsRequired(true);

                   entity
                    .Property(a => a.Address)
                    .HasMaxLength(250)
                    .IsUnicode(true)
                    .IsRequired(true);

                   entity
                    .Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsRequired(false);

                   entity
                    .Property(i => i.HasInsurance)
                    .IsRequired(true);
               });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity
                    .HasKey(v => v.VisitationId);

                entity
                    .Property(d => d.Date)
                    .HasColumnType("DATETIME")
                    .IsRequired(true);

                entity
                    .Property(c => c.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(true)
                    .IsRequired(false);

                entity
                    .HasOne(v => v.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(v => v.PatientId);

                entity
                    .HasOne(v => v.Doctor)
                    .WithMany(d => d.Visitations)
                    .HasForeignKey(v => v.DoctorId);
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity
                    .HasKey(d => d.DiagnoseId);

                entity
                    .Property(n => n.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity
                    .Property(c => c.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(true)
                    .IsRequired(false);

                entity
                    .HasOne(d => d.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.PatientId);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity
                    .HasKey(m => m.MedicamentId);

                entity
                    .Property(n => n.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .IsRequired(true);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity
                    .HasKey(pm => new { pm.PatientId, pm.MedicamentId });

                entity
                    .HasOne(pm => pm.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(pm => pm.PatientId);

                entity
                    .HasOne(pm => pm.Medicament)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(pm => pm.MedicamentId);
            });
        }
    }
}
