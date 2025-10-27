namespace HIMS.API.Utility
{
    public interface IFileUtility
    {
        Task<Tuple<MemoryStream, string, string>> DownloadFile(string filePath);
        Task<string> GetBase64(string filePath);
        string GetMimeType(string fileName);
        string SaveImageFromBase64(string Base64, string Folder);
        Task<string> GetBase64FromFolder(string Folder, string filename);
        Task<string> UploadFileAsync(IFormFile file, string Folder);
        void RemoveFile(string FileName, string Folder);
    }
}

