using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class TestMasterService : ITestMasterServices
    {
        private readonly Data.Models.HIMSDbContext _context;
        public TestMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<PathTestListDto>> PetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathTestListDto>(model, "Ps_Rtrv_PathologyTestList");

        }
        public virtual async Task<IPagedList<PathTestForUpdateListdto>> ListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathTestForUpdateListdto>(model, "Rtrv_PathTestForUpdate");

        }
        public virtual async Task<IPagedList<SubTestMasterListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SubTestMasterListDto>(model, "m_Retrieve_PathSubTestListForCombo");

        }
        public virtual async Task<IPagedList<PathTestDetailDto>> PathTestDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathTestDetailDto>(model, "rtrv_pathTestDetailList");

        }
        public virtual async Task<IPagedList<PathTemplateForUpdateListDto>> PathTemplateList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathTemplateForUpdateListDto>(model, "Rtrv_PathTemplateForUpdate");
        }
        public virtual void InsertSP(MPathTestMaster objTest, List<MPathTemplateDetail> ObjMPathTemplateDetail, List<MPathTestDetailMaster> ObjMPathTestDetailMaster, int UserId, string Username)
        {


            //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "UpdatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "MPathTemplateDetail1s", "MPathTemplateDetails", "MPathTestDetailMasters" };
            var entity = objTest.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VTestId = odal.ExecuteNonQuery("insert_PathologyTestMaster_1", CommandType.StoredProcedure, "TestId", entity);
            objTest.TestId = Convert.ToInt32(VTestId);

            if (objTest.IsTemplateTest == 1)
            {
                foreach (var item in ObjMPathTemplateDetail)

                {
                    item.TestId = Convert.ToInt32(VTestId);

                    string[] Entity = { "PtemplateId", "Test" };
                    var Tentity = item.ToDictionary();
                    foreach (var rProperty in Entity)
                    {
                        Tentity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("m_insert_PathologyTemplateTest_1", CommandType.StoredProcedure, Tentity);
                }

            }
            else if (objTest.IsTemplateTest == 0)
            {
                foreach (var Titem in ObjMPathTestDetailMaster)
                {

                    Titem.TestId = Convert.ToInt32(VTestId);


                    string[] DEntity = { "Test", "TestDetId", "Parameter" };
                    var dentity = Titem.ToDictionary();
                    foreach (var rProperty in DEntity)
                    {
                        dentity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("m_insert_PathTestDetailMaster_1", CommandType.StoredProcedure, dentity);
                }
            }
        }

        public virtual void UpdateSP(MPathTestMaster objTest, List<MPathTemplateDetail> ObjMPathTemplateDetail, List<MPathTestDetailMaster> ObjMPathTestDetailMaster, int UserId, string Username)
        {
            //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "AddedBy", "CreatedBy", "CreatedDate", "ModifiedDate", "MPathTemplateDetails", "MPathTemplateDetail1s", "MPathTestDetailMasters" };
            var entity = objTest.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_PathologyTestMaster_1", CommandType.StoredProcedure, entity);

            foreach (var item in ObjMPathTemplateDetail)
            {

                var tokensObj = new
                {
                    TestId = Convert.ToInt32(item.TestId)

                };

                odal.ExecuteNonQuery("m_Delete_M_PathTemplateDetails", CommandType.StoredProcedure, tokensObj.ToDictionary());
            }
            if (objTest.IsTemplateTest == 1)
            {

                foreach (var item in ObjMPathTemplateDetail)

                {
                    string[] Entity = { "PtemplateId", "Test" };
                    var Tentity = item.ToDictionary();
                    foreach (var rProperty in Entity)
                    {
                        Tentity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("m_insert_PathologyTemplateTest_1", CommandType.StoredProcedure, Tentity);
                }

            }
            else if (objTest.IsTemplateTest == 0)
            {
                foreach (var Titem in ObjMPathTestDetailMaster)
                {

                    var tokenObj = new
                    {
                        TestId = Convert.ToInt32(Titem.TestId)

                    };
                    odal.ExecuteNonQuery("m_Delete_M_PathTestDetailMaster", CommandType.StoredProcedure, tokenObj.ToDictionary());
                }
                foreach (var Titem in ObjMPathTestDetailMaster)
                {

                    string[] DEntity = { "Test", "TestDetId", "Parameter" };
                    var dentity = Titem.ToDictionary();
                    foreach (var rProperty in DEntity)
                    {
                        dentity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("m_insert_PathTestDetailMaster_1", CommandType.StoredProcedure, dentity);
                }
            }

        }

        public virtual async Task InsertAsync(MPathTestMaster objTest, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // remove conditional records
                if (objTest.IsTemplateTest == 1)
                {
                    objTest.MPathTestDetailMasters = null;
                }
                else
                {
                    objTest.MPathTemplateDetail1s = null;
                }
                // Add header table records
                _context.MPathTestMasters.Add(objTest);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(MPathTestMaster objTest, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {

                if (objTest.IsTemplateTest == 1)
                {
                    // Delete record from MPathTestMaster  table IsTemplateTest : 0
                    var lst1 = await _context.MPathTemplateDetails.Where(x => x.TestId == objTest.TestId).ToListAsync();
                    if (lst1 != null && lst1.Count > 0)
                    {
                        _context.MPathTemplateDetails.RemoveRange(lst1);
                    }
                    await _context.SaveChangesAsync(); // Save deletions before proceeding
                }
                else
                {
                    //Delete details table realted records // Delete record from MPathTestDetailMasters table IsNumeric : 1
                    var lst = await _context.MPathTestDetailMasters.Where(x => x.TestId == objTest.TestId).ToListAsync();
                    if (lst != null && lst.Count > 0)
                    {
                        _context.MPathTestDetailMasters.RemoveRange(lst);
                    }
                    await _context.SaveChangesAsync(); // Save deletions before proceeding
                }
                if (objTest.IsTemplateTest == 1)
                {
                    objTest.MPathTestDetailMasters = null; // Prevent re-inserting deleted records
                }
                else
                {
                    objTest.MPathTemplateDetail1s = null;
                }

                // Update header & detail table records
                _context.MPathTestMasters.Update(objTest);
                _context.Entry(objTest).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task TestUpdateAsync(MPathTestMaster ObjMPathTestMaster, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "TestId", "SpecimenTypeId", "SpecimenQty", "SpecimenConditionId", "ContainerTypeId", "CollectionMethod", "NoofContainer", "PreservationUsed", "BarcodeLabel", "IsConsentRequired", "IsFastingRequired", "IsApprovedRequired", "TestInformationTemplate", "Tatday", "Tathour", "Tatmin"};
            var Rentity = ObjMPathTestMaster.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Update_TestSpecimenDetails", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(MPathTestMaster), ObjMPathTestMaster.TestId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }

        

        Task ITestMasterServices.PaymentDateTimeUpdate(TPaymentPharmacy ObjTPaymentPharmacy, int UserId, string Username)
        {
            throw new NotImplementedException();
        }

      
    }
}



