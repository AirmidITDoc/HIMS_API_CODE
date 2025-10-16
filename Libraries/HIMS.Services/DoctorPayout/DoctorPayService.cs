using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DoctorPayout
{
    public class DoctorPayService : IDoctorPayService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DoctorPayService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DoctorPayListDto>> GetList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorPayListDto>(model, "ps_rtrv_T_AdditionalDocPay_List");
        }


        public virtual async Task InsertAsync(TAdditionalDocPay ObjTAdditionalDocPay, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "TranDate", "TranTime", "PbillNo", "CompanyId", "BillDate", "ServiceName", "Price", "Qty", "TotalAmount", "DocAmount", "IsProcess", "DocId", "PatientName", "TranId" };
            var entity = ObjTAdditionalDocPay.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_insert_T_AdditionalDocPay_1", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity,  nameof(TAdditionalDocPay), (int)ObjTAdditionalDocPay.TranId,  Core.Domain.Logging.LogAction.Add,  CurrentUserId,  CurrentUserName);
        }
    }
}
