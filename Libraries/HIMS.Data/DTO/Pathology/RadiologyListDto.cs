﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace HIMS.Data.DTO.Pathology
{
    public class RadiologyListDto
    {
        public long RadReportId { get; set; }
        public string? RadDate { get; set; }
        public string? RadTime { get; set; }
        public long VisitId { get; set; }
        public long Visit_Adm_ID { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? GenderName { get; set; }
        public string? ConsultantDoctor { get; set; }
        public string? VisitDate { get; set; }
        public string? VisitTime { get; set; }
        public string? TestName { get; set; }
        public long opdipdtype { get; set; } 
        public string? PBillNo { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public string OPDNo { get; set; }
        public long RadTestID { get; set; }
        public long TestId { get; set; }
        public long ChargeId { get; set; }
        public string? CategoryName { get; set; }
        public long IsCancelled { get; set; }
        public string? AdmissionDate { get; set; }
        public string? OP_IP_Number { get; set; }
        public long opdipdid { get; set; }
        public string PatientType { get; set; }
        public string AgeYear { get; set; }

    }

}




