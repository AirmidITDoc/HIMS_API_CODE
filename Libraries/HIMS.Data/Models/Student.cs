using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Student
    {
        public int? Rollno { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int? Grades { get; set; }
        public string? City { get; set; }
    }
}
