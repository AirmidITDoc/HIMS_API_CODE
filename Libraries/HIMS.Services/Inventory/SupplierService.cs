using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    //public class SupplierService : ISupplierService
    //{
    //    private readonly Data.Models.HIMSDbContext _context;
    //    public SupplierService(HIMSDbContext HIMSDbContext)
    //    {
    //        _context = HIMSDbContext;
    //    }

        //public virtual async Task InsertAsyncSP(MSupplierMaster objSupplier, MAssignSupplierToStore objAssignSupplier, int currentUserId, string currentUserName)
        //{
        //    DatabaseHelper odal = new();
        //    string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
        //    var entity = objSupplier.ToDictionary();
        //    foreach (var rProperty in rEntity)
        //    {
        //        entity.Remove(rProperty);
        //    }
        //    string RegId = odal.ExecuteNonQuery("m_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
        //    objSupplier.SupplierId = Convert.ToInt32(RegId);
        //    objAssignSupplier.SupplierId = Convert.ToInt32(RegId);

        //    string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray" };
        //    var visitentity = objVisitDetail.ToDictionary();
        //    foreach (var rProperty in rVisitEntity)
        //    {
        //        visitentity.Remove(rProperty);
        //    }
        //    string VisitId = odal.ExecuteNonQuery("m_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
        //    objVisitDetail.VisitId = Convert.ToInt32(VisitId);

        //    SqlParameter[] para = new SqlParameter[1];
        //    para[0] = new SqlParameter
        //    {
        //        SqlDbType = SqlDbType.BigInt,
        //        ParameterName = "@PatVisitID",
        //        Value = Convert.ToInt32(VisitId)
        //    };
        //    odal.ExecuteNonQuery("m_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, para);
        //}




    //}
}
