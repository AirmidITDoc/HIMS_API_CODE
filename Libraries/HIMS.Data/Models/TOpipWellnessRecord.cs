using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOpipWellnessRecord
    {
        public long WlsId { get; set; }
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public long? StepCount { get; set; }
        public decimal? CaloriesBurned { get; set; }
        public decimal? SleepDuration { get; set; }
        public long? ActivityLevel { get; set; }
        public decimal? BloodGlucose { get; set; }
        public decimal? FluidIntake { get; set; }
        public string? MentalStatus { get; set; }
        public string? GeneralWellbeing { get; set; }
        public DateTime? LastMenstrualPeriod { get; set; }
        public long? NumberOfMenstrualPeriodsPerYear { get; set; }
        public string? AgeOfMenarche { get; set; }
        public string? AgeOfMenopause { get; set; }
        public long? TobaccoSmokingStatus { get; set; }
        public long? TobaccoChewingStatus { get; set; }
        public long? AlcoholDrinkingStatus { get; set; }
        public string? DietType { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
