namespace HIMS.API.Models.OutPatient
{
    public class TariffwiseClassRatechangeModel
    {
        public long? ClassId { get; set; }
        public long? TariffId { get; set; }
        public long? OpdIpdId { get; set; }
        public long? NewClassId { get; set; }
        public long? NewTariffId { get; set; }


    }
}