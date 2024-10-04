namespace HIMS.API.Models.Nursing
{

    public class NewPrescription
    {
        public List<IPPrescriptionModel> TIpPrescription { get; set; }

        //public IPMedicalRecordModel TPrescription { get; set; }
    }
    public class IPPrescriptionModel
    {

        public long IppreId { get; set; }
        public long? OpdIpdIp { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Ptime { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public string? Remark { get; set; }
        public bool? IsClosed { get; set; }
        public long? StoreId { get; set; }
        public long? WardID { get; set; }
    }
    //public class IPMedicalRecordModel
    //{

    //}
}