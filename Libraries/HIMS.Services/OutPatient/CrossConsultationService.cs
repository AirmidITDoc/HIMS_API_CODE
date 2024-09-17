using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HIMS.Services.OutPatient
{
    public class CrossConsultationService : ICrossConsultationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CrossConsultationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(VisitDetail objCrossConsultation, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "Opdno", "IsMark", "Comments", "IsXray" };
            var entity = objCrossConsultation.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VisitID = odal.ExecuteNonQuery("insert_VisitDetails_New_1", CommandType.StoredProcedure, "VisitID", entity);
            objCrossConsultation.RegId = Convert.ToInt32(VisitID);

            await _context.SaveChangesAsync(UserId, Username);
        }
    }
}
