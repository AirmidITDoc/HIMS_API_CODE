using HIMS.Data.DataProviders;
using System.Data;
using System.Transactions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using HIMS.Services.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Extensions;
using System.Dynamic;
using Microsoft.Data.SqlClient;
using static LinqToDB.Common.Configuration;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace HIMS.Services.OPPatient
{
    public class VisitDetailsService : IVisitDetailsService
    {
        private readonly HIMSDbContext _context;
        public VisitDetailsService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<dynamic>> GetListAsync(GridRequestModel model)
        {
            //dynamic resultList = _ICommonService.GetDataSetByProc(model);
                DatabaseHelper odal = new();
            Dictionary<string, string> fields = SearchFieldExtension.GetSearchFields(model.Filters).ToDictionary(e => e.FieldName, e => e.FieldValueString);
            string sp_Name = "m_Rtrv_VisitDetailsList_1_Pagi";
            int sp_Para = 0;
            SqlParameter[] para = new SqlParameter[fields.Count];
            foreach (var property in fields)
            {
                var param = new SqlParameter
                {
                    ParameterName = "@" + property.Key,
                    Value = property.Value.ToString()
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            DataSet ds = odal.FetchDataSetBySP(sp_Name, para);
            if (ds.Tables[1].Rows.Count > 0)
            {
                return new PagedList<dynamic>((dynamic)ds.Tables[1].ToDynamic(), model.First, model.Rows, Convert.ToInt32(ds.Tables[0].Rows[0]["total_row"]));
            }
            return new PagedList<dynamic>((dynamic)ds.Tables[0].ToDynamic(), model.First, model.Rows, Convert.ToInt32(ds.Tables[0].Rows[0]["total_row"]));
        }

        public virtual async Task CancelAsync(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                VisitDetail objVisit = await _context.VisitDetails.FindAsync(objVisitDetail.VisitId);
                objVisit.IsCancelled = objVisitDetail.IsCancelled;
                objVisit.IsCancelledBy = objVisitDetail.IsCancelledBy;
                objVisit.IsCancelledDate = objVisitDetail.IsCancelledDate;
                _context.VisitDetails.Update(objVisit);
                _context.Entry(objVisit).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task InsertAsync(Registration objRegistration, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            //// NEW CODE With EDMX
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Add Registration table records
                _context.Registrations.Add(objRegistration);
                await _context.SaveChangesAsync();

                //// Update Registration table records
                //ConfigSetting objConfigRSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                //objConfigRSetting.RegNo = Convert.ToString(Convert.ToInt32(objConfigRSetting.RegNo) + 1);
                //_context.ConfigSettings.Update(objConfigRSetting);
                //_context.Entry(objConfigRSetting).State = EntityState.Modified;
                //await _context.SaveChangesAsync();

                // Add VisitDetail table records
                objVisitDetail.RegId = objRegistration.RegId;
                _context.VisitDetails.Add(objVisitDetail);
                await _context.SaveChangesAsync();

                // Update VisitDetail table records
                ConfigSetting objConfigSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                objConfigSetting.Opno = Convert.ToString(Convert.ToInt32(objConfigSetting.Opno) + 1);
                _context.ConfigSettings.Update(objConfigSetting);
                _context.Entry(objConfigSetting).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Add TokenNumber table records
                List<VisitDetail> objVisit = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now).ToListAsync();
                foreach (var item in objVisit)
                {
                    TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
                    if (objToken != null)
                    {
                        objToken.TokenNo = Convert.ToInt32(objToken.TokenNo ?? 0) + 1;

                        TTokenNumberWithDoctorWise objCurrentToken = new()
                        {
                            TokenNo = objToken.TokenNo,
                            VisitDate = item.VisitDate,
                            VisitId = item.VisitId,
                            DoctorId = item.ConsultantDocId,
                            IsStatus = false
                        };
                        _context.TTokenNumberWithDoctorWises.Add(objCurrentToken);
                        await _context.SaveChangesAsync();
                    }
                }

                scope.Complete();
            }


        }





        public virtual async Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {



            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("v_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            objRegistration.RegId = Convert.ToInt32(RegId);
            objVisitDetail.RegId = Convert.ToInt32(RegId);

            string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("v_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitId);

            var tokenObj = new
            {
                VisitId = Convert.ToInt32(VisitId)
            };
            odal.ExecuteNonQuery("v_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());

            // Add TokenNumber table records
            //List<VisitDetail> objVisit = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now).ToListAsync();
            //foreach (var item in objVisit)
            //{
            //    TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
            //    if (objToken != null)
            //    {
            //        objToken.TokenNo = Convert.ToInt32(objToken.TokenNo ?? 0) + 1;

            //        TTokenNumberWithDoctorWise objCurrentToken = new()
            //        {
            //            TokenNo = objToken.TokenNo,
            //            VisitDate = item.VisitDate,
            //            VisitId = item.VisitId,
            //            DoctorId = item.ConsultantDocId,
            //            IsStatus = false
            //        };
            //        _context.TTokenNumberWithDoctorWises.Add(objCurrentToken);
            //        await _context.SaveChangesAsync();
            //    }
            //}
        }

        public virtual async Task UpdateAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {



            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rEntity = { "RegNo", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember", "ReligionId", "AreaId", "IsSeniorCitizen", "AddedBy", "RegDate", "RegTime" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_RegistrationForAppointment_1", CommandType.StoredProcedure, entity);
            objRegistration.RegId = Convert.ToInt32(objRegistration.RegId);
            objVisitDetail.RegId = Convert.ToInt32(objRegistration.RegId);

            string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("v_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitId);

            //var tokenObj = new
            //{
            //    PatVisitID = Convert.ToInt32(VisitId)
            //};
            //odal.ExecuteNonQuery("m_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());

            // Add TokenNumber table records
            List<VisitDetail> objVisit = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now).ToListAsync();
            foreach (var item in objVisit)
            {
                TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
                if (objToken != null)
                {
                    objToken.TokenNo = Convert.ToInt32(objToken.TokenNo ?? 0) + 1;

                    TTokenNumberWithDoctorWise objCurrentToken = new()
                    {
                        TokenNo = objToken.TokenNo,
                        VisitDate = item.VisitDate,
                        VisitId = item.VisitId,
                        DoctorId = item.ConsultantDocId,
                        IsStatus = false
                    };
                    _context.TTokenNumberWithDoctorWises.Add(objCurrentToken);
                    await _context.SaveChangesAsync();
                }
            }
        }

    }
}
