using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HIMS.Services.IPPatient
{
    public  class MIcdDiagnosisMasterService: IMIcdDiagnosisMasterService
    {
        private readonly HIMSDbContext _context;

        public MIcdDiagnosisMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
      
        //public virtual MIcdDiagnosisMasterDto GetMIcdDiagnosis(string? Icdcode, string? DiagnosisName)
        //{
        //    MIcdDiagnosisMasterDto objMain = new();

        //    DatabaseHelper sql = new();

        //    SqlParameter[] para = new SqlParameter[2];
        //    para[0] = new SqlParameter("@Icdcode", Icdcode);
        //    para[1] = new SqlParameter("@DiagnosisName", DiagnosisName);

        //    DataTable dt = sql.FetchDataTableBySP("Retrieve_IcdDiagnosisMasterForCombo", para);

        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow dr = dt.Rows[0];

        //        objMain.Icdid = Convert.ToInt32(dr["Icdid"]);
        //        objMain.Icdcode = dr["Icdcode"].ToString() ?? "";
        //        objMain.DiagnosisName = dr["DiagnosisName"].ToString() ?? "";

        //        if (dt.Columns.Contains("Icdversion"))
        //            objMain.Icdversion = dr["Icdversion"].ToString() ?? "";

        //        if (dt.Columns.Contains("ShortName"))
        //            objMain.ShortName = dr["ShortName"].ToString();
        //    }

        //    return objMain;
        //}
        public List<MIcdDiagnosisMasterNewDto> GetMIcdDiagnosis(string Keyword)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Keyword", Keyword);
            return sql.FetchListBySP<MIcdDiagnosisMasterNewDto>("ps_Retrieve_IcdDiagnosisMasterForCombo", para);
        }
    }
}
