using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;

namespace HIMS.Services.IPPatient
{
    public class DischargeSummaryService : IDischargeSummaryService
    {
        private readonly HIMSDbContext _context;
        public DischargeSummaryService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AddedByDate", "UpdatedBy", "UpdatedByDate" };
            var entity = ObjDischargeSummary.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VDischargeSummaryId = odal.ExecuteNonQuery("sp_insert_DischargeSummary_1", CommandType.StoredProcedure, "DischargeSummaryId", entity);
            ObjDischargeSummary.DischargeSummaryId = Convert.ToInt32(VDischargeSummaryId);

            foreach (var item in ObjTIpPrescriptionDischarge)
            { 

            string[] DEntity = { "PrecriptionId", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsClosed" };
            var pentity = item.ToDictionary();
            foreach (var Property in DEntity)
            {
                pentity.Remove(Property);
            }
            odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity);

            }
        }

       

        public virtual async Task UpdateAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AdmissionId", "DischargeSummaryDate", "DischargeSummaryTime", "AddedBy", "AddedByDate", "UpdatedByDate" };
            var Uentity = ObjDischargeSummary.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Uentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_update_DischargeSummary_1", CommandType.StoredProcedure, Uentity);
        
            
            //foreach (var item in ObjTIpPrescriptionDischarge)
            //{
            //    var tokensObj = new
            //    {
            //        OPDIPDID = Convert.ToInt32(item.OpdIpdId)
            //    };
            //    odal.ExecuteNonQuery("sp_Delete_T_IP_Prescription_Discharge", CommandType.StoredProcedure, tokensObj.ToDictionary());
            //}
                                //string[] DEntity = { "PrecriptionId", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsClosed" };
                                //var pentity = ObjTIpPrescriptionDischarge.ToDictionary();
                                //foreach (var Property in DEntity)
                                //{
                                //    pentity.Remove(Property);
                                //}
                                //odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity);


            //foreach (var item in ObjTIpPrescriptionDischarge)
            //{

            //    string[] DEntity1 = { "PrecriptionId", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsClosed" };
            //    var pentity1 = item.ToDictionary();
            //    foreach (var Property in DEntity1)
            //    {
            //        pentity1.Remove(Property);
            //    }
            //    odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity1);

            //}

        }

        public virtual async Task<IPagedList<DischrageSummaryListDTo>> IPDischargesummaryList(GridRequestModel objGrid)
        {
            return await DatabaseHelper.GetGridDataBySp<DischrageSummaryListDTo>(objGrid, "v_Rtrv_T_DischargeSummary");
        }

        public virtual async Task<IPagedList<IPPrescriptiononDischargeListDto>> IPPrescriptionDischargesummaryList(GridRequestModel objGrid)
        {
            return await DatabaseHelper.GetGridDataBySp<IPPrescriptiononDischargeListDto>(objGrid, "v_Rtrv_IP_Prescription_Discharge");
        }
    }
}

















