using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Sql;
using System.Xml.Linq;

namespace HIMS.Data.DTO.Administration
{
    public class PathDispatchReportHistoryListDto
    {
        public long DispatchId { get; set; }
        public long DispatchModeId { get; set; }
        public string Name { get; set; }
        public long UnitId { get; set; }
        public string? HospitalName { get; set; }
        public string? Comments { get; set; }
        public long? DispatchBy { get; set; }
        public DateTime? DispatchOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long LabPatientId { get; set; }
        public long OPD_IPD_Type { get; set; }
        public string? PatientName { get; set; }
        public string DoctorName { get; set; }
        public long? IsVerifyid { get; set; }
        public DateTime? IsVerifyedDate { get; set; }
        public bool? IsVerifySign { get; set; }
        public string? OutSourceLabName { get; set; }
        public bool? IsCompleted { get; set; }
        public long PathTestID { get; set; }
        public string? ServiceName { get; set; }

        public string? CreatedUser { get; set; }

        public string? Modifieduser { get; set; }




    }
}
