using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Connection;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<PatientMedicament> Prescriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.Connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(
                entity =>
                {
                    entity.HasKey(x => x.PatientId);

                    entity.Property(x => x.FirstName).IsRequired().IsUnicode().HasMaxLength(50);

                    entity.Property(x => x.LastName).IsRequired().IsUnicode().HasMaxLength(50);

                    entity.Property(x => x.Address).IsRequired().IsUnicode().HasMaxLength(250);

                    entity.Property(x => x.Email).IsRequired().IsUnicode().HasMaxLength(80);

                    entity.Property(x => x.HasInsurance).HasDefaultValue(true);
                }
            );

            modelBuilder.Entity<Doctor>(
                entity =>
                {
                    entity.HasKey(x => x.DoctorId);

                    entity.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(100);

                    entity.Property(x => x.Speciality).IsRequired().IsUnicode().HasMaxLength(100);
                }
            );

            modelBuilder.Entity<Visitation>(
                entity =>
                {
                    entity.HasKey(e => e.VisitationId);

                    entity.Property(e => e.Date).HasColumnName("VisitationDate").IsRequired()
                        .HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()");

                    entity.Property(e => e.Comments).IsRequired(false).IsUnicode().HasMaxLength(250);

                    entity.HasOne(e => e.Patient).WithMany(p => p.Visitations).HasForeignKey(p => p.PatientId)
                        .HasConstraintName("FK_Visitation_Patient");

                    entity.HasOne(e => e.Doctor).WithMany(v => v.Visitations).HasForeignKey(x => x.DoctorId)
                        .HasConstraintName("FK_DoctorsVisitations");
                }
                );

            modelBuilder.Entity<Diagnose>(
                entity =>
                {
                    entity.HasKey(e => e.DiagnoseId);

                    entity.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(50);

                    entity.Property(e => e.Comments).IsRequired().IsUnicode().HasMaxLength(250);

                    entity.HasOne(e => e.Patient).WithMany(e => e.Diagnoses).HasForeignKey(p => p.PatientId);
                });

            modelBuilder.Entity<Medicament>(
                entity =>
                {
                    entity.HasKey(e => e.MedicamentId);

                    entity.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(50);
                });

            modelBuilder.Entity<PatientMedicament>(
                entity =>
                {
                    entity.HasKey(e => new { e.PatientId, e.MedicamentId });

                    entity.HasOne(e => e.Medicament).WithMany(m => m.Prescriptions)
                        .HasForeignKey(e => e.MedicamentId);

                    entity.HasOne(e => e.Patient).WithMany(p => p.Prescriptions).HasForeignKey(e => e.PatientId);
                });
        }
    }
}
