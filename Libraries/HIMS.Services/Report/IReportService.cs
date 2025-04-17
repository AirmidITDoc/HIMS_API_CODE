using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Http;

namespace HIMS.Services.Report
{
    public partial interface IReportService
    {
        string GetReportSetByProc(ReportRequestModel model);
        string GetNewReportSetByProc(ReportNewRequestModel model);
        Task<List<ServiceMasterDTO>> SearchService(string str);
        Task<List<MDepartmentMaster>> SearchDepartment(string str);
        Task<List<CashCounter>> SearchCashCounter(string str);
    }
    //public interface IFileUtility
    //{
    //    Task<string> UploadDocument(IFormFile objFile, string Folder, string FileName);
    //    Task<Tuple<MemoryStream, string, string>> DownloadFile(string filePath);
    //    Task<string> GetBase64(string filePath);
    //    string GetMimeType(string fileName);
    //    string SaveImageFromBase64(string Base64, string Folder);
    //    Task<string> GetBase64FromFolder(string Folder, string filename);
    //    string GetReportSetByProc(ReportRequestModel model);
    //    string GetNewReportSetByProc(ReportNewRequestModel model);
    //    Task<List<ServiceMasterDTO>> SearchService(string str);
    //    Task<List<MDepartmentMaster>> SearchDepartment(string str);
    //    Task<List<CashCounter>> SearchCashCounter(string str);
    //}
}
