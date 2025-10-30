namespace HIMS.Data.DTO.Administration
{
    public class DoctorPaysummarydetailListDto
    {

        public string? AddChargeDrName { get; set; }
        public long PBillNo { get; set; }
        public long BillNo { get; set; }
        public long? ChargesId { get; set; }
        public string? ServiceName { get; set; }

        public decimal? NetAmount { get; set; }


        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }


        public decimal? RefundAmount { get; set; }


        //public long? DoctorId { get; set; }
        public string? PatientName { get; set; }
        public string? CompanyName { get; set; }

        public string? lbl { get; set; }

        public long IsDoctorShareGenerated { get; set; }

        //public Boolean? IsBillShrHold { get; set; }




    }
}
