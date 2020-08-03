using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBManager.PostgreModels
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
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

                entity.ToTable("bed");

                entity.HasIndex(e => e.Number)
                    .HasName("bed_number_key")
                    .IsUnique();

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.LoungeNumber).HasColumnName("lounge_number");

                entity.Property(e => e.Icu)
                    .IsRequired()
                    .HasColumnName("icu")
                    .HasColumnType("bit(1)");

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

                entity.ToTable("bed_equipment");

                entity.HasIndex(e => e.Id)
                    .HasName("bed_equipment_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BedNumber).HasColumnName("bed_number");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

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

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("equipment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(15);

                entity.Property(e => e.Provider)
                    .IsRequired()
                    .HasColumnName("provider")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<HealthWorker>(entity =>
            {
                entity.HasKey(e => new { e.Ssn, e.HospitalId })
                    .HasName("health_worker_pkey");

                entity.ToTable("health_worker");

                entity.HasIndex(e => e.Email)
                    .HasName("health_worker_email_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Ssn)
                    .HasName("health_worker_ssn_key")
                    .IsUnique();

                entity.Property(e => e.Ssn)
                    .HasColumnName("ssn")
                    .HasMaxLength(15);

                entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(15);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("fname")
                    .HasMaxLength(15);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("lname")
                    .HasMaxLength(15);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(15);

                entity.Property(e => e.Sex).HasColumnName("sex");

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

                entity.ToTable("history_store");

                entity.Property(e => e.TableName)
                    .HasColumnName("table_name")
                    .HasMaxLength(50);

                entity.Property(e => e.PkDateDest)
                    .HasColumnName("pk_date_dest")
                    .HasMaxLength(400);

                entity.Property(e => e.PkDateSrc)
                    .IsRequired()
                    .HasColumnName("pk_date_src")
                    .HasMaxLength(400);

                entity.Property(e => e.RecordState).HasColumnName("record_state");

                entity.Property(e => e.Timemark).HasColumnName("timemark");
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.ToTable("hospital");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<HospitalProcedure>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ProcedureId, e.HospitalId })
                    .HasName("hospital_procedure_pkey");

                entity.ToTable("hospital_procedure");

                entity.HasIndex(e => e.Id)
                    .HasName("hospital_procedure_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProcedureId).HasColumnName("procedure_id");

                entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

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

                entity.ToTable("lounge");

                entity.HasIndex(e => e.Number)
                    .HasName("lounge_number_key")
                    .IsUnique();

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

                entity.Property(e => e.Floor).HasColumnName("floor");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(15);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(15);

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

                entity.ToTable("patient");

                entity.HasIndex(e => e.Email)
                    .HasName("patient_email_key")
                    .IsUnique();

                entity.Property(e => e.Ssn)
                    .HasColumnName("ssn")
                    .HasMaxLength(15);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Ssn)
                    .HasName("person_pkey");

                entity.ToTable("person");

                entity.HasIndex(e => e.Email)
                    .HasName("person_email_key")
                    .IsUnique();

                entity.Property(e => e.Ssn)
                    .HasColumnName("ssn")
                    .HasMaxLength(15);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<PgBuffercache>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_buffercache");

                entity.Property(e => e.Bufferid).HasColumnName("bufferid");

                entity.Property(e => e.Isdirty).HasColumnName("isdirty");

                entity.Property(e => e.PinningBackends).HasColumnName("pinning_backends");

                entity.Property(e => e.Relblocknumber).HasColumnName("relblocknumber");

                entity.Property(e => e.Reldatabase)
                    .HasColumnName("reldatabase")
                    .HasColumnType("oid");

                entity.Property(e => e.Relfilenode)
                    .HasColumnName("relfilenode")
                    .HasColumnType("oid");

                entity.Property(e => e.Relforknumber).HasColumnName("relforknumber");

                entity.Property(e => e.Reltablespace)
                    .HasColumnName("reltablespace")
                    .HasColumnType("oid");

                entity.Property(e => e.Usagecount).HasColumnName("usagecount");
            });

            modelBuilder.Entity<PgStatStatements>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_stat_statements");

                entity.Property(e => e.BlkReadTime).HasColumnName("blk_read_time");

                entity.Property(e => e.BlkWriteTime).HasColumnName("blk_write_time");

                entity.Property(e => e.Calls).HasColumnName("calls");

                entity.Property(e => e.Dbid)
                    .HasColumnName("dbid")
                    .HasColumnType("oid");

                entity.Property(e => e.LocalBlksDirtied).HasColumnName("local_blks_dirtied");

                entity.Property(e => e.LocalBlksHit).HasColumnName("local_blks_hit");

                entity.Property(e => e.LocalBlksRead).HasColumnName("local_blks_read");

                entity.Property(e => e.LocalBlksWritten).HasColumnName("local_blks_written");

                entity.Property(e => e.MaxTime).HasColumnName("max_time");

                entity.Property(e => e.MeanTime).HasColumnName("mean_time");

                entity.Property(e => e.MinTime).HasColumnName("min_time");

                entity.Property(e => e.Query).HasColumnName("query");

                entity.Property(e => e.Queryid).HasColumnName("queryid");

                entity.Property(e => e.Rows).HasColumnName("rows");

                entity.Property(e => e.SharedBlksDirtied).HasColumnName("shared_blks_dirtied");

                entity.Property(e => e.SharedBlksHit).HasColumnName("shared_blks_hit");

                entity.Property(e => e.SharedBlksRead).HasColumnName("shared_blks_read");

                entity.Property(e => e.SharedBlksWritten).HasColumnName("shared_blks_written");

                entity.Property(e => e.StddevTime).HasColumnName("stddev_time");

                entity.Property(e => e.TempBlksRead).HasColumnName("temp_blks_read");

                entity.Property(e => e.TempBlksWritten).HasColumnName("temp_blks_written");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("oid");
            });

            modelBuilder.Entity<Procedure>(entity =>
            {
                entity.ToTable("procedure");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.HospitalId, e.PatientId })
                    .HasName("reservation_pkey");

                entity.ToTable("reservation");

                entity.HasIndex(e => e.Id)
                    .HasName("reservation_id_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

                entity.Property(e => e.PatientId)
                    .HasColumnName("patient_id")
                    .HasMaxLength(15);

                entity.Property(e => e.Procedure)
                    .IsRequired()
                    .HasColumnName("procedure")
                    .HasMaxLength(15);

                entity.Property(e => e.Startdate)
                    .HasColumnName("startdate")
                    .HasColumnType("date");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
