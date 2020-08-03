﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    public partial class VisibleJobs
    {
        [Column("job_id")]
        public Guid JobId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(128)]
        public string Name { get; set; }
        [Column("delete_requested_time")]
        public DateTime? DeleteRequestedTime { get; set; }
        [Column("is_system")]
        public bool IsSystem { get; set; }
        [Column("schedule_start_time")]
        public DateTime ScheduleStartTime { get; set; }
        [Column("schedule_end_time")]
        public DateTime ScheduleEndTime { get; set; }
        [Required]
        [Column("schedule_interval_type")]
        [StringLength(50)]
        public string ScheduleIntervalType { get; set; }
        [Column("schedule_interval_count")]
        public int ScheduleIntervalCount { get; set; }
        [Column("enabled")]
        public bool Enabled { get; set; }
        [Required]
        [Column("description")]
        [StringLength(512)]
        public string Description { get; set; }
        [Column("last_job_execution_id")]
        public Guid? LastJobExecutionId { get; set; }
    }
}
