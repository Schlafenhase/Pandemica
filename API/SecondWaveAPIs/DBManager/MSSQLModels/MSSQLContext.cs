using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBManager.MSSQLModels
{
    public partial class MSSQLContext : DbContext
    {
        public MSSQLContext()
        {
        }

        public MSSQLContext(DbContextOptions<MSSQLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentInstance> AgentInstance { get; set; }
        public virtual DbSet<AgentVersion> AgentVersion { get; set; }
        public virtual DbSet<CasesAndDeathsByCountry> CasesAndDeathsByCountry { get; set; }
        public virtual DbSet<CommandData> CommandData { get; set; }
        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Continent> Continent { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CurrentJobVersionNumbers> CurrentJobVersionNumbers { get; set; }
        public virtual DbSet<DatabaseCredentials> DatabaseCredentials { get; set; }
        public virtual DbSet<Enforces> Enforces { get; set; }
        public virtual DbSet<EnumType> EnumType { get; set; }
        public virtual DbSet<HistoryStore> HistoryStore { get; set; }
        public virtual DbSet<Hospital> Hospital { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobCancellations> JobCancellations { get; set; }
        public virtual DbSet<JobExecutions> JobExecutions { get; set; }
        public virtual DbSet<JobExecutions1> JobExecutions1 { get; set; }
        public virtual DbSet<JobTaskExecutions> JobTaskExecutions { get; set; }
        public virtual DbSet<JobVersions> JobVersions { get; set; }
        public virtual DbSet<JobVersions1> JobVersions1 { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Jobs1> Jobs1 { get; set; }
        public virtual DbSet<JobstepData> JobstepData { get; set; }
        public virtual DbSet<JobstepVersions> JobstepVersions { get; set; }
        public virtual DbSet<Jobsteps> Jobsteps { get; set; }
        public virtual DbSet<Jobsteps1> Jobsteps1 { get; set; }
        public virtual DbSet<Medication> Medication { get; set; }
        public virtual DbSet<MessageQueue> MessageQueue { get; set; }
        public virtual DbSet<MetaInformation> MetaInformation { get; set; }
        public virtual DbSet<MetaInformation1> MetaInformation1 { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Pathology> Pathology { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientMedication> PatientMedication { get; set; }
        public virtual DbSet<PatientPathologies> PatientPathologies { get; set; }
        public virtual DbSet<PatientState> PatientState { get; set; }
        public virtual DbSet<PatientStateByCountry> PatientStateByCountry { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<ProvinceStateRegion> ProvinceStateRegion { get; set; }
        public virtual DbSet<SanitaryMeasurements> SanitaryMeasurements { get; set; }
        public virtual DbSet<Scaleunitlimits> Scaleunitlimits { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ScheduleTask> ScheduleTask { get; set; }
        public virtual DbSet<ScheduleTask1> ScheduleTask1 { get; set; }
        public virtual DbSet<ScriptBatches> ScriptBatches { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Subscription> Subscription { get; set; }
        public virtual DbSet<SyncObjectData> SyncObjectData { get; set; }
        public virtual DbSet<Syncgroup> Syncgroup { get; set; }
        public virtual DbSet<Syncgroupmember> Syncgroupmember { get; set; }
        public virtual DbSet<TargetAssociations> TargetAssociations { get; set; }
        public virtual DbSet<TargetGroupMembers> TargetGroupMembers { get; set; }
        public virtual DbSet<TargetGroupMembersJson> TargetGroupMembersJson { get; set; }
        public virtual DbSet<TargetGroupMemberships> TargetGroupMemberships { get; set; }
        public virtual DbSet<TargetGroups> TargetGroups { get; set; }
        public virtual DbSet<Targets> Targets { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<Taskdependency> Taskdependency { get; set; }
        public virtual DbSet<TemporaryData> TemporaryData { get; set; }
        public virtual DbSet<Uihistory> Uihistory { get; set; }
        public virtual DbSet<Userdatabase> Userdatabase { get; set; }
        public virtual DbSet<VisibleJobs> VisibleJobs { get; set; }
        public virtual DbSet<VisibleTargetGroups> VisibleTargetGroups { get; set; }
        public virtual DbSet<VisibleTargetsFormatted> VisibleTargetsFormatted { get; set; }
        public virtual DbSet<WorldAccumulated> WorldAccumulated { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:firstwavedb.database.windows.net,1433;Initial Catalog=Pandemica;Persist Security Info=False;User ID=schlafenhase;Password=Kevinlaovejaturbo123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>(entity =>
            {
                entity.HasIndex(e => new { e.State, e.Lastupdatetime })
                    .HasName("index_action_state_lastupdatetime");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Creationtime).HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasIndex(e => new { e.Subscriptionid, e.Name })
                    .HasName("IX_Agent_SubId_Name")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.State).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.Subscriptionid)
                    .HasConstraintName("FK__agent__subscript");
            });

            modelBuilder.Entity<AgentInstance>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentInstance)
                    .HasForeignKey(d => d.Agentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__agent_ins__agent");
            });

            modelBuilder.Entity<AgentVersion>(entity =>
            {
                entity.HasIndex(e => e.Version)
                    .HasName("UQ__agent_ve__0F540134C95C9DC7")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ExpiresOn).HasDefaultValueSql("('9999-12-31 23:59:59.997')");
            });

            modelBuilder.Entity<CasesAndDeathsByCountry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASES AND DEATHS BY COUNTRY");

                entity.Property(e => e.Country).IsUnicode(false);
            });

            modelBuilder.Entity<CommandData>(entity =>
            {
                entity.HasIndex(e => e.TextChecksum)
                    .HasName("IX_command_text_checksum");

                entity.Property(e => e.CommandDataId).ValueGeneratedNever();

                entity.Property(e => e.TextChecksum).HasComputedColumnSql("(binary_checksum([text]))");
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastModified).HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => new { e.Person, e.Patient, e.Id })
                    .HasName("PK__CONTACT__40E82D94DD3D8188");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__CONTACT__3214EC06F4B02AF1")
                    .IsUnique();

                entity.Property(e => e.Person).IsUnicode(false);

                entity.Property(e => e.Patient).IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.PatientNavigation)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.Patient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CONTACT_PATIENT_FK");

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.Person)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CONTACT_PERSON_FK");
            });

            modelBuilder.Entity<Continent>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__CONTINEN__737584F774F8CEB1");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__CONTINEN__737584F600E7D2E0")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__COUNTRY__737584F7CA5DF0FB");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__COUNTRY__7614F5F6468E35F9")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__COUNTRY__737584F60D99F1C9")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.ContinentName).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.HasOne(d => d.ContinentNameNavigation)
                    .WithMany(p => p.Country)
                    .HasForeignKey(d => d.ContinentName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COUNTRY_CONTINENT_FK");
            });

            modelBuilder.Entity<CurrentJobVersionNumbers>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("current_job_version_numbers", "jobs_internal");
            });

            modelBuilder.Entity<DatabaseCredentials>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("database_credentials", "jobs_internal");
            });

            modelBuilder.Entity<Enforces>(entity =>
            {
                entity.HasKey(e => new { e.Country, e.Measurement, e.Id })
                    .HasName("PK__ENFORCES__499B7EC46748DEFA");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__ENFORCES__3214EC0603B00165")
                    .IsUnique();

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Enforces)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENFORCES_COUNTRY_FK");

                entity.HasOne(d => d.MeasurementNavigation)
                    .WithMany(p => p.Enforces)
                    .HasForeignKey(d => d.Measurement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENFORCES_SANITARY_MEASUREMENTS_FK");
            });

            modelBuilder.Entity<EnumType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastModified).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Type).IsUnicode(false);
            });

            modelBuilder.Entity<HistoryStore>(entity =>
            {
                entity.HasKey(e => new { e.TableName, e.PkDateDest })
                    .HasName("history_store_primary")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__HOSPITAL__7614F5F67F050DD6")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__HOSPITAL__3214EC065A152240")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__HOSPITAL__737584F60D271B65")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("UQ__HOSPITAL__5C7E359EEFC11D0C")
                    .IsUnique();

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.ManagerName).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Region).IsUnicode(false);

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Hospital)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HOSPITAL_COUNTRY_FK");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasIndex(e => e.IsCancelled)
                    .HasName("index_job_iscancelled");

                entity.Property(e => e.JobId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.InitialInsertTimeUtc).HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<JobCancellations>(entity =>
            {
                entity.HasKey(e => e.JobExecutionId)
                    .HasName("PK_jobs_internal.job_cancellations");

                entity.Property(e => e.JobExecutionId).ValueGeneratedNever();

                entity.HasOne(d => d.JobExecution)
                    .WithOne(p => p.JobCancellations)
                    .HasForeignKey<JobCancellations>(d => d.JobExecutionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.job_cancellations_jobs_internal.job_executions_job_execution_id");
            });

            modelBuilder.Entity<JobExecutions>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("job_executions", "jobs");
            });

            modelBuilder.Entity<JobExecutions1>(entity =>
            {
                entity.HasKey(e => e.JobExecutionId)
                    .HasName("PK_jobs_internal.job_executions");

                entity.HasIndex(e => e.LastJobTaskExecutionId)
                    .HasName("IX_last_job_task_execution_id");

                entity.HasIndex(e => e.ParentJobExecutionId)
                    .HasName("IX_parent_job_execution_id");

                entity.HasIndex(e => e.RootJobExecutionId)
                    .HasName("IX_root_job_execution_id");

                entity.HasIndex(e => e.TargetId)
                    .HasName("IX_target_id");

                entity.HasIndex(e => new { e.JobId, e.JobVersionNumber, e.StepId })
                    .HasName("IX_job_id_job_version_number_step_id");

                entity.HasIndex(e => new { e.JobExecutionId, e.DoNotRetryUntilTime, e.ParentJobExecutionId, e.LastJobTaskExecutionId, e.EndTime })
                    .HasName("IX_job_executions_end_time");

                entity.HasIndex(e => new { e.JobExecutionId, e.DoNotRetryUntilTime, e.ParentJobExecutionId, e.LastJobTaskExecutionId, e.IsActive })
                    .HasName("IX_job_executions_is_active");

                entity.HasIndex(e => new { e.JobExecutionId, e.StepId, e.CreateTime, e.Lifecycle, e.ParentJobExecutionId })
                    .HasName("IX_job_executions_parent_job_execution_id");

                entity.Property(e => e.JobExecutionId).ValueGeneratedNever();

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.LastJobTaskExecution)
                    .WithMany(p => p.JobExecutions1)
                    .HasForeignKey(d => d.LastJobTaskExecutionId)
                    .HasConstraintName("FK_jobs_internal.job_executions_jobs_internal.job_task_executions_last_job_task_execution_id");

                entity.HasOne(d => d.ParentJobExecution)
                    .WithMany(p => p.InverseParentJobExecution)
                    .HasForeignKey(d => d.ParentJobExecutionId)
                    .HasConstraintName("FK_jobs_internal.job_executions_jobs_internal.job_executions_parent_job_execution_id");

                entity.HasOne(d => d.RootJobExecution)
                    .WithMany(p => p.InverseRootJobExecution)
                    .HasForeignKey(d => d.RootJobExecutionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.job_executions_jobs_internal.job_executions_root_job_execution_id");

                entity.HasOne(d => d.Target)
                    .WithMany(p => p.JobExecutions1)
                    .HasForeignKey(d => d.TargetId)
                    .HasConstraintName("FK_jobs_internal.job_executions_jobs_internal.targets_target_id");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobExecutions1)
                    .HasForeignKey(d => new { d.JobId, d.JobVersionNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.job_executions_jobs_internal.job_versions_job_id_job_version_number");

                entity.HasOne(d => d.Jobsteps1)
                    .WithMany(p => p.JobExecutions1)
                    .HasForeignKey(d => new { d.JobId, d.JobVersionNumber, d.StepId })
                    .HasConstraintName("FK_jobs_internal.job_executions_jobs_internal.jobsteps_job_id_job_version_number_step_id");
            });

            modelBuilder.Entity<JobTaskExecutions>(entity =>
            {
                entity.HasKey(e => e.JobTaskExecutionId)
                    .HasName("PK_jobs_internal.job_task_executions");

                entity.HasIndex(e => e.CreateTime);

                entity.HasIndex(e => e.EndTime);

                entity.HasIndex(e => e.JobExecutionId)
                    .HasName("IX_job_executions_one_top_level_task_for_execution")
                    .IsUnique()
                    .HasFilter("([previous_job_task_execution_id] IS NULL)");

                entity.HasIndex(e => e.PreviousJobTaskExecutionId)
                    .HasName("IX_previous_job_task_execution_id")
                    .HasFilter("([previous_job_task_execution_id] IS NOT NULL)");

                entity.Property(e => e.JobTaskExecutionId).ValueGeneratedNever();

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.JobExecution)
                    .WithOne(p => p.JobTaskExecutions)
                    .HasForeignKey<JobTaskExecutions>(d => d.JobExecutionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.job_task_executions_jobs_internal.job_executions_job_execution_id");

                entity.HasOne(d => d.PreviousJobTaskExecution)
                    .WithMany(p => p.InversePreviousJobTaskExecution)
                    .HasForeignKey(d => d.PreviousJobTaskExecutionId)
                    .HasConstraintName("FK_jobs_internal.job_task_executions_jobs_internal.job_task_executions_previous_job_task_execution_id");
            });

            modelBuilder.Entity<JobVersions>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("job_versions", "jobs");
            });

            modelBuilder.Entity<JobVersions1>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.JobVersionNumber })
                    .HasName("PK_jobs_internal.job_versions");

                entity.HasIndex(e => e.JobId)
                    .HasName("IX_job_id");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobVersions1)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.job_versions_jobs_internal.jobs_job_id");
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("jobs", "jobs");
            });

            modelBuilder.Entity<Jobs1>(entity =>
            {
                entity.HasKey(e => e.JobId)
                    .HasName("PK_jobs_internal.jobs");

                entity.HasIndex(e => e.LastJobExecutionId)
                    .HasName("IX_last_job_execution_id");

                entity.HasIndex(e => new { e.Name, e.DeleteRequestedTime, e.IsSystem })
                    .IsUnique();

                entity.Property(e => e.JobId).ValueGeneratedNever();

                entity.HasOne(d => d.LastJobExecution)
                    .WithMany(p => p.Jobs1)
                    .HasForeignKey(d => d.LastJobExecutionId)
                    .HasConstraintName("FK_jobs_internal.jobs_jobs_internal.job_executions_last_job_execution_id");
            });

            modelBuilder.Entity<JobstepData>(entity =>
            {
                entity.HasIndex(e => e.CommandDataId)
                    .HasName("IX_command_data_id");

                entity.HasIndex(e => e.ResultSetDestinationTargetId)
                    .HasName("IX_result_set_destination_target_id");

                entity.HasIndex(e => e.TargetId)
                    .HasName("IX_target_id");

                entity.Property(e => e.JobstepDataId).ValueGeneratedNever();

                entity.HasOne(d => d.CommandData)
                    .WithMany(p => p.JobstepData)
                    .HasForeignKey(d => d.CommandDataId)
                    .HasConstraintName("FK_jobs_internal.jobstep_data_jobs_internal.command_data_command_data_id");

                entity.HasOne(d => d.ResultSetDestinationTarget)
                    .WithMany(p => p.JobstepDataResultSetDestinationTarget)
                    .HasForeignKey(d => d.ResultSetDestinationTargetId)
                    .HasConstraintName("FK_jobs_internal.jobstep_data_jobs_internal.targets_result_set_destination_target_id");

                entity.HasOne(d => d.Target)
                    .WithMany(p => p.JobstepDataTarget)
                    .HasForeignKey(d => d.TargetId)
                    .HasConstraintName("FK_jobs_internal.jobstep_data_jobs_internal.targets_target_id");
            });

            modelBuilder.Entity<JobstepVersions>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("jobstep_versions", "jobs");
            });

            modelBuilder.Entity<Jobsteps>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("jobsteps", "jobs");
            });

            modelBuilder.Entity<Jobsteps1>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.JobVersionNumber, e.StepId })
                    .HasName("PK_jobs_internal.jobsteps");

                entity.HasIndex(e => e.JobstepDataId)
                    .HasName("IX_jobstep_data_id");

                entity.HasIndex(e => new { e.JobId, e.JobVersionNumber, e.StepName })
                    .IsUnique();

                entity.Property(e => e.StepName).HasDefaultValueSql("('JobStep')");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Jobsteps1)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_jobs_internal.jobsteps_jobs_internal.jobs_job_id");

                entity.HasOne(d => d.JobstepData)
                    .WithMany(p => p.Jobsteps1)
                    .HasForeignKey(d => d.JobstepDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.jobsteps_jobs_internal.jobstep_data_jobstep_data_id");

                entity.HasOne(d => d.JobNavigation)
                    .WithMany(p => p.Jobsteps1)
                    .HasForeignKey(d => new { d.JobId, d.JobVersionNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.jobsteps_jobs_internal.job_versions_job_id_job_version_number");
            });

            modelBuilder.Entity<Medication>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("UQ__MEDICATI__3214EC0652D59079")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__MEDICATI__737584F6CF6626EB")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Pharmacy).IsUnicode(false);
            });

            modelBuilder.Entity<MessageQueue>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__MessageQ__C87C0C9C5531AED4");

                entity.HasIndex(e => e.JobId)
                    .HasName("index_messagequeue_jobid");

                entity.HasIndex(e => new { e.QueueId, e.UpdateTimeUtc, e.InsertTimeUtc, e.ExecTimes, e.Version })
                    .HasName("index_messagequeue_getnextmessage");

                entity.HasIndex(e => new { e.QueueId, e.MessageType, e.UpdateTimeUtc, e.InsertTimeUtc, e.ExecTimes, e.Version })
                    .HasName("index_messagequeue_getnextmessagebytype");

                entity.Property(e => e.MessageId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.InitialInsertTimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.MessageQueue)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__MessageQu__JobId__7FB5F314");
            });

            modelBuilder.Entity<MetaInformation>(entity =>
            {
                entity.Property(e => e.State).HasDefaultValueSql("((1))");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.VersionString)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1.0.0.0')");
            });

            modelBuilder.Entity<MetaInformation1>(entity =>
            {
                entity.Property(e => e.State).HasDefaultValueSql("((1))");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.VersionString)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1.0.0.0')");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_jobs_internal.__MigrationHistory");
            });

            modelBuilder.Entity<Pathology>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("UQ__PATHOLOG__3214EC06E2CD5FE8")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__PATHOLOG__737584F676722461")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Ssn)
                    .HasName("PK__PATIENT__CA33E0E5F5ADCA1E");

                entity.HasIndex(e => e.Ssn)
                    .HasName("UQ__PATIENT__CA33E0E4E9981271")
                    .IsUnique();

                entity.Property(e => e.Ssn).IsUnicode(false);

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Nationality).IsUnicode(false);

                entity.Property(e => e.Region).IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.PatientCountryNavigation)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_COUNTRY_C_FK");

                entity.HasOne(d => d.HospitalNavigation)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.Hospital)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_HOSPITAL_FK");

                entity.HasOne(d => d.NationalityNavigation)
                    .WithMany(p => p.PatientNationalityNavigation)
                    .HasForeignKey(d => d.Nationality)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_COUNTRY_N_FK");
            });

            modelBuilder.Entity<PatientMedication>(entity =>
            {
                entity.HasKey(e => new { e.Patient, e.Medication, e.Id })
                    .HasName("PK__PATIENT___2C3A05A751D57C2F");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__PATIENT___3214EC06A7A60432")
                    .IsUnique();

                entity.Property(e => e.Patient).IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.MedicationNavigation)
                    .WithMany(p => p.PatientMedication)
                    .HasForeignKey(d => d.Medication)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_MEDICATION_MEDICATION_FK");

                entity.HasOne(d => d.PatientNavigation)
                    .WithMany(p => p.PatientMedication)
                    .HasForeignKey(d => d.Patient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_MEDICATION_PATIENT_FK");
            });

            modelBuilder.Entity<PatientPathologies>(entity =>
            {
                entity.HasKey(e => new { e.Patient, e.Pathology, e.Id })
                    .HasName("PK__PATIENT___F845D11AA320F13D");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__PATIENT___3214EC064BA6620C")
                    .IsUnique();

                entity.Property(e => e.Patient).IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.PathologyNavigation)
                    .WithMany(p => p.PatientPathologies)
                    .HasForeignKey(d => d.Pathology)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_PATHOLOGIES_PATHOLOGY_FK");

                entity.HasOne(d => d.PatientNavigation)
                    .WithMany(p => p.PatientPathologies)
                    .HasForeignKey(d => d.Patient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_PATHOLOGIES_PATIENT_FK");
            });

            modelBuilder.Entity<PatientState>(entity =>
            {
                entity.HasKey(e => new { e.State, e.Patient, e.Id })
                    .HasName("PK__PATIENT___D1AA4944E3333C2D");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__PATIENT___3214EC06F19B3CE4")
                    .IsUnique();

                entity.Property(e => e.Patient).IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.PatientNavigation)
                    .WithMany(p => p.PatientState)
                    .HasForeignKey(d => d.Patient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_STATE_PATIENT_FK");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.PatientState)
                    .HasForeignKey(d => d.State)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_STATE_STATE_FK");
            });

            modelBuilder.Entity<PatientStateByCountry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PATIENT STATE BY COUNTRY");

                entity.Property(e => e.Country).IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Ssn)
                    .HasName("PK__PERSON__CA33E0E5CCD4F103");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__PERSON__7614F5F61542F398")
                    .IsUnique();

                entity.HasIndex(e => e.Ssn)
                    .HasName("UQ__PERSON__CA33E0E46EF95B4A")
                    .IsUnique();

                entity.Property(e => e.Ssn).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ProvinceStateRegion>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.Country, e.Id })
                    .HasName("PK__PROVINCE__E520231BDAF8EBAB");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__PROVINCE__3214EC063F5CE499")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__PROVINCE__737584F68A324CA4")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.ProvinceStateRegion)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PROVINCE_STATE_REGION_COUNTRY_FK");
            });

            modelBuilder.Entity<SanitaryMeasurements>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("UQ__SANITARY__3214EC06F0DAE0EB")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__SANITARY__737584F610345A8F")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Scaleunitlimits>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastModified).HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<ScheduleTask>(entity =>
            {
                entity.Property(e => e.SyncGroupId).ValueGeneratedNever();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.SyncGroup)
                    .WithOne(p => p.ScheduleTask)
                    .HasForeignKey<ScheduleTask>(d => d.SyncGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ScheduleT__SyncG");
            });

            modelBuilder.Entity<ScheduleTask1>(entity =>
            {
                entity.HasKey(e => e.ScheduleTaskId)
                    .HasName("PK__Schedule__8DAD173ACDB15694");

                entity.HasIndex(e => e.MessageId)
                    .HasName("ScheduleTask_MessageId_Index");

                entity.Property(e => e.ScheduleTaskId).ValueGeneratedNever();

                entity.HasOne(d => d.ScheduleNavigation)
                    .WithMany(p => p.ScheduleTask1)
                    .HasForeignKey(d => d.Schedule)
                    .HasConstraintName("FK__ScheduleT__Sched__0A338187");
            });

            modelBuilder.Entity<ScriptBatches>(entity =>
            {
                entity.HasKey(e => new { e.CommandDataId, e.ScriptBatchNumber })
                    .HasName("PK_jobs_internal.script_batches");

                entity.HasIndex(e => e.CommandDataId)
                    .HasName("IX_command_data_id");

                entity.HasOne(d => d.CommandData)
                    .WithMany(p => p.ScriptBatches)
                    .HasForeignKey(d => d.CommandDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.script_batches_jobs_internal.command_data_command_data_id");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("UQ__STATE__3214EC06F98111CF")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__STATE__737584F64DECE6F7")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasIndex(e => e.Syncserveruniquename)
                    .HasName("IX_SyncServerUniqueName")
                    .IsUnique()
                    .HasFilter("([syncserveruniquename] IS NOT NULL)");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.EnableDetailedProviderTracing).HasDefaultValueSql("((0))");

                entity.Property(e => e.Policyid).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SyncObjectData>(entity =>
            {
                entity.HasKey(e => new { e.ObjectId, e.DataType })
                    .HasName("PK_SyncObjectExtInfo");

                entity.Property(e => e.CreatedTime).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModified)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Syncgroup>(entity =>
            {
                entity.HasIndex(e => e.HubMemberid)
                    .HasName("index_syncgroup_hub_memberid");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ConflictTableRetentionInDays).HasDefaultValueSql("((30))");

                entity.Property(e => e.State).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncEnabled).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.HubMember)
                    .WithMany(p => p.Syncgroup)
                    .HasForeignKey(d => d.HubMemberid)
                    .HasConstraintName("FK__syncgroup__hub_m");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Syncgroup)
                    .HasForeignKey(d => d.Subscriptionid)
                    .HasConstraintName("FK__syncgroup__subsc");
            });

            modelBuilder.Entity<Syncgroupmember>(entity =>
            {
                entity.HasIndex(e => e.Databaseid)
                    .HasName("index_syncgroupmember_databaseid");

                entity.HasIndex(e => new { e.Syncgroupid, e.Databaseid })
                    .HasName("IX_SyncGroupMember_SyncGroupId_DatabaseId")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.HubstateLastupdated).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.MemberstateLastupdated).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Scopename).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Database)
                    .WithMany(p => p.Syncgroupmember)
                    .HasForeignKey(d => d.Databaseid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__syncmember__datab");

                entity.HasOne(d => d.Syncgroup)
                    .WithMany(p => p.Syncgroupmember)
                    .HasForeignKey(d => d.Syncgroupid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__syncmember__syncg");
            });

            modelBuilder.Entity<TargetAssociations>(entity =>
            {
                entity.HasKey(e => new { e.ParentTargetId, e.ChildTargetId })
                    .HasName("PK_jobs_internal.target_associations");

                entity.HasIndex(e => e.ChildTargetId)
                    .HasName("IX_child_target_id");

                entity.HasIndex(e => e.ParentTargetId)
                    .HasName("IX_parent_target_id");

                entity.HasOne(d => d.ChildTarget)
                    .WithMany(p => p.TargetAssociationsChildTarget)
                    .HasForeignKey(d => d.ChildTargetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.target_associations_jobs_internal.targets_child_target_id");

                entity.HasOne(d => d.ParentTarget)
                    .WithMany(p => p.TargetAssociationsParentTarget)
                    .HasForeignKey(d => d.ParentTargetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.target_associations_jobs_internal.targets_parent_target_id");
            });

            modelBuilder.Entity<TargetGroupMembers>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("target_group_members", "jobs");

                entity.Property(e => e.MembershipType).IsUnicode(false);
            });

            modelBuilder.Entity<TargetGroupMembersJson>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("target_group_members_json", "jobs_internal");
            });

            modelBuilder.Entity<TargetGroupMemberships>(entity =>
            {
                entity.HasKey(e => new { e.ParentTargetId, e.ChildTargetId })
                    .HasName("PK_jobs_internal.target_group_memberships");

                entity.HasIndex(e => e.ChildTargetId)
                    .HasName("IX_child_target_id");

                entity.HasIndex(e => e.ParentTargetId)
                    .HasName("IX_parent_target_id");

                entity.HasOne(d => d.ChildTarget)
                    .WithMany(p => p.TargetGroupMembershipsChildTarget)
                    .HasForeignKey(d => d.ChildTargetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.target_group_memberships_jobs_internal.targets_child_target_id");

                entity.HasOne(d => d.ParentTarget)
                    .WithMany(p => p.TargetGroupMembershipsParentTarget)
                    .HasForeignKey(d => d.ParentTargetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jobs_internal.target_group_memberships_jobs_internal.targets_parent_target_id");
            });

            modelBuilder.Entity<TargetGroups>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("target_groups", "jobs");
            });

            modelBuilder.Entity<Targets>(entity =>
            {
                entity.HasKey(e => e.TargetId)
                    .HasName("PK_jobs_internal.targets");

                entity.HasIndex(e => new { e.TargetGroupName, e.DeleteRequestedTime })
                    .HasName("IX_target_group_unique")
                    .IsUnique()
                    .HasFilter("([target_type]='TargetGroup')");

                entity.HasIndex(e => new { e.SubscriptionId, e.ResourceGroupName, e.ServerName, e.RefreshCredentialName })
                    .HasName("IX_sql_server_target_unique")
                    .IsUnique()
                    .HasFilter("([target_type]='SqlServer')");

                entity.HasIndex(e => new { e.ServerName, e.DatabaseName, e.ElasticPoolName, e.SubscriptionId, e.ResourceGroupName })
                    .HasName("IX_sql_database_target_unique")
                    .IsUnique()
                    .HasFilter("([target_type]='SqlDatabase')");

                entity.HasIndex(e => new { e.SubscriptionId, e.ResourceGroupName, e.ServerName, e.ElasticPoolName, e.RefreshCredentialName })
                    .HasName("IX_sql_elastic_pool_target_unique")
                    .IsUnique()
                    .HasFilter("([target_type]='SqlElasticPool')");

                entity.HasIndex(e => new { e.SubscriptionId, e.ResourceGroupName, e.ServerName, e.ShardMapName, e.RefreshCredentialName })
                    .HasName("IX_sql_shard_map_target_unique")
                    .IsUnique()
                    .HasFilter("([target_type]='SqlShardMap')");

                entity.Property(e => e.TargetId).ValueGeneratedNever();

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasIndex(e => new { e.Actionid, e.Completedtime })
                    .HasName("index_task_completedtime");

                entity.HasIndex(e => new { e.Id, e.State, e.Actionid })
                    .HasName("index_task_actionid");

                entity.HasIndex(e => new { e.Type, e.Agentid, e.State })
                    .HasName("index_task_agentid_state")
                    .HasFilter("([state]=(0))");

                entity.HasIndex(e => new { e.Type, e.State, e.Completedtime })
                    .HasName("index_task_state")
                    .HasFilter("([state]=(2))");

                entity.HasIndex(e => new { e.Id, e.OwningInstanceid, e.Lastheartbeat, e.State })
                    .HasName("index_task_state_lastheartbeat")
                    .HasFilter("([state]<(0))");

                entity.HasIndex(e => new { e.OwningInstanceid, e.Version, e.State, e.Agentid, e.DependencyCount, e.Priority, e.Creationtime })
                    .HasName("index_task_gettask");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Creationtime).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Priority).HasDefaultValueSql("((100))");

                entity.Property(e => e.State).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaskNumber).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.Actionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__task__actionid");
            });

            modelBuilder.Entity<Taskdependency>(entity =>
            {
                entity.HasKey(e => new { e.Nexttaskid, e.Prevtaskid })
                    .HasName("PK_TaskTask");

                entity.HasOne(d => d.Nexttask)
                    .WithMany(p => p.TaskdependencyNexttask)
                    .HasForeignKey(d => d.Nexttaskid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__taskdepen__nextt");

                entity.HasOne(d => d.Prevtask)
                    .WithMany(p => p.TaskdependencyPrevtask)
                    .HasForeignKey(d => d.Prevtaskid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__taskdepen__prevt");
            });

            modelBuilder.Entity<TemporaryData>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.RequestedData).IsUnicode(false);
            });

            modelBuilder.Entity<Uihistory>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Agentid)
                    .HasName("Idx_UIHistory_AgentId");

                entity.HasIndex(e => e.CompletionTime)
                    .HasName("Idx_UIHistory_CompletionTime");

                entity.HasIndex(e => e.Databaseid)
                    .HasName("Idx_UIHistory_DatabaseId");

                entity.HasIndex(e => e.Id)
                    .HasName("Idx_UIHistory_Id");

                entity.HasIndex(e => e.Serverid)
                    .HasName("Idx_UIHistory_ServerId")
                    .IsClustered();

                entity.HasIndex(e => e.SyncgroupId)
                    .HasName("Idx_UIHistory_SyncgroupId");

                entity.Property(e => e.IsWritable).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Userdatabase>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Userdatabase)
                    .HasForeignKey(d => d.Subscriptionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__userdatab__subsc");
            });

            modelBuilder.Entity<VisibleJobs>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("visible_jobs", "jobs_internal");
            });

            modelBuilder.Entity<VisibleTargetGroups>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("visible_target_groups", "jobs_internal");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<VisibleTargetsFormatted>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("visible_targets_formatted", "jobs_internal");
            });

            modelBuilder.Entity<WorldAccumulated>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("WORLD ACCUMULATED");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
