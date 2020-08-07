using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreProcedures.Source.Entities;

namespace StoreProcedures.PostgreModels
{
    public partial class PostgreContext : DbContext
    {
        public PostgreContext()
        {
        }

        public PostgreContext(DbContextOptions<PostgreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bed> Bed { get; set; }
        public virtual DbSet<BedEquipment> BedEquipment { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<HealthWorker> HealthWorker { get; set; }
        public virtual DbSet<HistoryStore> HistoryStore { get; set; }
        public virtual DbSet<Hospital> Hospital { get; set; }
        public virtual DbSet<HospitalProcedure> HospitalProcedure { get; set; }
        public virtual DbSet<Lounge> Lounge { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PgBuffercache> PgBuffercache { get; set; }
        public virtual DbSet<PgStatStatements> PgStatStatements { get; set; }
        public virtual DbSet<Procedure> Procedure { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<ReservationProcedures> ReservationProcedures { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=pandemica.postgres.database.azure.com;Database=postgres;Port=5432;User Id=Jose@pandemica;Password=David2911;Ssl Mode=Require;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_stat_statements");

            modelBuilder.Entity<Bed>(entity =>
            {
                entity.HasKey(e => new { e.Number, e.LoungeNumber })
                    .HasName("bed_pkey");

                entity.HasIndex(e => e.Number)
                    .HasName("bed_number_key")
                    .IsUnique();

                entity.HasOne(d => d.LoungeNumberNavigation)
                    .WithMany(p => p.Bed)
                    .HasPrincipalKey(p => p.Number)
                    .HasForeignKey(d => d.LoungeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bed_lounge_number_fkey");
            });

            modelBuilder.Entity<BedEquipment>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.BedNumber, e.EquipmentId })
                    .HasName("bed_equipment_pkey");

                entity.HasIndex(e => e.Id)
                    .HasName("bed_equipment_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.BedNumberNavigation)
                    .WithMany(p => p.BedEquipment)
                    .HasPrincipalKey(p => p.Number)
                    .HasForeignKey(d => d.BedNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bed_equipment_bed_number_fkey");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.BedEquipment)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bed_equipment_equipment_id_fkey");
            });

            modelBuilder.Entity<HealthWorker>(entity =>
            {
                entity.HasKey(e => new { e.Ssn, e.HospitalId })
                    .HasName("health_worker_pkey");

                entity.HasIndex(e => e.Email)
                    .HasName("health_worker_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Ssn)
                    .HasName("health_worker_ssn_key")
                    .IsUnique();

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.HealthWorker)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("health_worker_hospital_id_fkey");
            });

            modelBuilder.Entity<HistoryStore>(entity =>
            {
                entity.HasKey(e => new { e.TableName, e.PkDateDest })
                    .HasName("history_store_pkey");
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<HospitalProcedure>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ProcedureId, e.HospitalId })
                    .HasName("hospital_procedure_pkey");

                entity.HasIndex(e => e.Id)
                    .HasName("hospital_procedure_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.HospitalProcedure)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hospital_procedure_hospital_id_fkey");

                entity.HasOne(d => d.Procedure)
                    .WithMany(p => p.HospitalProcedure)
                    .HasForeignKey(d => d.ProcedureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hospital_procedure_procedure_id_fkey");
            });

            modelBuilder.Entity<Lounge>(entity =>
            {
                entity.HasKey(e => new { e.Number, e.HospitalId })
                    .HasName("lounge_pkey");

                entity.HasIndex(e => e.Number)
                    .HasName("lounge_number_key")
                    .IsUnique();

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Lounge)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lounge_hospital_id_fkey");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Ssn)
                    .HasName("patient_pkey");

                entity.HasIndex(e => e.Email)
                    .HasName("patient_email_key")
                    .IsUnique();
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Ssn)
                    .HasName("person_pkey");

                entity.HasIndex(e => e.Email)
                    .HasName("person_email_key")
                    .IsUnique();
            });

            modelBuilder.Entity<PgBuffercache>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<PgStatStatements>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<Procedure>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("procedure_name_key")
                    .IsUnique();
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.HospitalId, e.PatientId })
                    .HasName("reservation_pkey");

                entity.HasIndex(e => e.Id)
                    .HasName("reservation_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_hospital_id_fkey");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_patient_id_fkey");
            });

            modelBuilder.Entity<ReservationProcedures>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ProcedureId, e.ReservationId })
                    .HasName("reservation_procedures_pkey");

                entity.HasIndex(e => e.Id)
                    .HasName("reservation_procedures_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Procedure)
                    .WithMany(p => p.ReservationProcedures)
                    .HasForeignKey(d => d.ProcedureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_procedures_procedure_id_fkey");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.ReservationProcedures)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_procedures_reservation_id_fkey");
            });

            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
