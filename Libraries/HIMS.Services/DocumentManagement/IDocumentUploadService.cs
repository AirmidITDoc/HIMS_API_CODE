using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.DocumentManagement;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System.Linq.Expressions;

namespace HIMS.Services.DocumentManagement
{
    public interface IDocumentUploadService
    {
        Task<List<RegistrationAutoCompleteDto>> SearchRegistration(string str);
        Task<List<DocumentFile>> GetDocumentsByCategoryAndAdmissionId(long AdmissionId, int CategoryId);
        Task<List<DocumentFile>> GetDocumentsByPatientId(long PatientId);
        Task<List<DocumentFile>> GetAllDocuments(int count = 50);
        Task<List<Admission>> GetRegistrationsByPatientId(long PatientId);
        Task<List<DocumentFile>> Add(List<DocumentFile> entity, int UserId, string Username);
    }
}
