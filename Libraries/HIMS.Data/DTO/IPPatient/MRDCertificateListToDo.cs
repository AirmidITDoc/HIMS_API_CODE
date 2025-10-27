namespace HIMS.Data.DTO.IPPatient
{

    public class MRDCertificateListToDo
    {

        public long AdmissionID { get; set; }
        public long DischargeId { get; set; }
        public long RegId { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? Address { get; set; }
        public string? AdmissionDate { get; set; }
        public string? DischargeDate { get; set; }
        public string? GenderName { get; set; }
        public string? AgeYear { get; set; }
        public string? DepartmentName { get; set; }

        public string IPDNo { get; set; }

        public long BillNo { get; set; }

        public string? PBillNo { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }


        //public double? AnnualIncome { get; set; }

        public decimal? PaidAmount { get; set; }


        //public long IsCancelled { get; set; }
        //public bool Ischarity { get; set; }



    }
}



