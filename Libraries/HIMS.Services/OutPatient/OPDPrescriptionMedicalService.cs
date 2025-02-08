using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public class OPDPrescriptionMedicalService : IOPDPrescriptionMedicalService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OPDPrescriptionMedicalService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(TPrescription objTPrescription, int UserId, string Username)
        {

            ////Add header table records
            DatabaseHelper odal = new();
            //string[] PEntity = { "PrecriptionId", "OpdIpdIp", "OpdIpdType", "Date", "Ptime", "ClassId", "GenericId", "DrugId", "DoseId", "Days", "InstructionId", "QtyPerDay", "TotalQty", "Instruction",
            //    "Remark","IsClosed","IsEnglishOrIsMarathi","Pweight","Pulse","Bp","Bsl","ChiefComplaint","IsAddBy","SpO2","StoreId","DoseOption2","DaysOption2","DoseOption3","DaysOption3"};
            //var pentity = objTPrescription.ToDictionary();
            //foreach (var Property in PEntity)
            //{
            //    pentity.Remove(Property);
            //}
            //odal.ExecuteNonQuery("m_delete_OPPrescription_1", CommandType.StoredProcedure, pentity);

            string[] rEntity = { "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" };

            var entity = objTPrescription.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vPrecriptionId = odal.ExecuteNonQuery("v_insert_OPPrescription_1", CommandType.StoredProcedure, "PrecriptionId", entity);
            objTPrescription.PrecriptionId = Convert.ToInt32(vPrecriptionId);


        }
    }
}
