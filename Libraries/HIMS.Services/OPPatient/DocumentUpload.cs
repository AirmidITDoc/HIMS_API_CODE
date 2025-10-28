using HIMS.Data.Models;

namespace HIMS.Services.OPPatient
{
    public class DocumentUpload : IDocumentUpload
    {
        private readonly HIMSDbContext _context;
        public DocumentUpload(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        //public Task InsertAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName)
        //{
        //   // throw new NotImplementedException();
        //}
    }
}
