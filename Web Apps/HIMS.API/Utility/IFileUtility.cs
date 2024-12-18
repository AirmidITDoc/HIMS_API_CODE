using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HIMS.API.Utility
{
    public interface IFileUtility
    {
        Task<string> UploadDocument(IFormFile objFile, string Folder,string FileName);
        Task<Tuple<MemoryStream, string, string>> DownloadFile(string filePath);
        Task<string> GetBase64(string filePath);
        string GetMimeType(string fileName);
        string SaveImageFromBase64(string Base64, string Folder);
        string GetBase64FromFolder(string Folder, string filename);
    }
}

