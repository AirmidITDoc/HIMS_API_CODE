using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.DocumentManagement;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HIMS.Services.DocumentManagement
{
    public class DocumentUploadService : IDocumentUploadService
    {
        private readonly HIMSDbContext _context;

        public DocumentUploadService(HIMSDbContext context)
        {
            _context = context;
        }
        public virtual async Task<List<RegistrationAutoCompleteDto>> SearchRegistration(string str)
        {
            var qry = from x in _context.Registrations
                      join g in _context.DbGenderMasters on x.GenderId equals g.GenderId into genderGroup
                      from g in genderGroup.DefaultIfEmpty()
                      where (x.FirstName + " " + (x.MiddleName ?? "") + " " + x.LastName).ToLower().StartsWith(str) || x.FirstName.ToLower().StartsWith(str) || x.RegNo.ToLower().StartsWith(str) || x.MobileNo.ToLower().Contains(str)
                      orderby x.RegNo == str ? 3 : x.MobileNo == str ? 2 : (x.FirstName + " " + x.LastName) == str ? 1 : 0
                      select new RegistrationAutoCompleteDto
                      {
                          FirstName = x.FirstName,
                          Id = x.RegId,
                          LastName = x.LastName,
                          MiddleName = x.MiddleName,
                          RegNo = x.RegNo,
                          MobileNo = x.MobileNo,
                          AgeYear = x.AgeYear,
                          DateofBirth = x.DateofBirth,
                          Gender = g != null ? g.GenderName : null,
                          PhotoInitials = x.FirstName.Substring(0, 1).ToUpper() + (string.IsNullOrEmpty(x.LastName) ? "" : x.LastName.Substring(0, 1).ToUpper())
                      };
            return await qry.Take(25).ToListAsync();
        }
        public virtual async Task<List<Admission>> GetRegistrationsByPatientId(long PatientId)
        {
            return await _context.Admissions.Where(x => x.RegId == PatientId).ToListAsync();
        }
        public async Task<List<DocumentFile>> Add(List<DocumentFile> entity, int UserId, string Username)
        {
            _context.DocumentFiles.AddRange(entity);
            await _context.SaveChangesAsync(UserId, Username);
            return entity;
        }
    }
}
