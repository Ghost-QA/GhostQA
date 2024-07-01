﻿using GhostQA_API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GhostQA_API.Models
{
    public class SuiteScheduleInfo : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Interval { get; set; }
        public string SuiteName { get; set; }
        public int RootId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDate { get; set; }
        public string DaysOfWeek { get; set; }
        public int DayOfMonth { get; set; }
        public int RepeatEvery { get; set; }
        public string JobId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
    }
}
