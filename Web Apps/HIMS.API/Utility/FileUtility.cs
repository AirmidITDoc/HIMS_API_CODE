﻿using Aspose.Cells;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace HIMS.API.Utility
{
    public class FileUtility : IFileUtility
    {
        public readonly ISalesService _Sales;
        public readonly IConfiguration _configuration;
        public FileUtility(ISalesService sales, IConfiguration configuration)
        {
            _Sales = sales;
            _configuration = configuration;
        }
        public async Task<string> UploadDocument(IFormFile objFile, string Folder, string FileName)
        {
            var DestinationPath = "";
                //_Sales.GetFilePath();
            if (string.IsNullOrWhiteSpace(DestinationPath))
                DestinationPath = _configuration.GetValue<string>("StorageBasePath");
            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);
            if (!Directory.Exists($"{DestinationPath.Trim('\\')}\\{Folder}"))
                Directory.CreateDirectory($"{DestinationPath.Trim('\\')}\\{Folder}");
            string FilePath = Path.Combine(DestinationPath.Trim('\\'), Folder.Trim('\\'));
            string NewFileName = Path.Combine(FilePath, (string.IsNullOrWhiteSpace(FileName) ? Guid.NewGuid().ToString() : FileName) + System.IO.Path.GetExtension(objFile.FileName));
            if (File.Exists(NewFileName))
                NewFileName = Path.Combine(FilePath, FileName + "_" + Guid.NewGuid() + System.IO.Path.GetExtension(objFile.FileName));
            using (Stream fileStream = new FileStream(NewFileName, FileMode.Create))
            {
                await objFile.CopyToAsync(fileStream);
            }
            return NewFileName;
        }

        public async Task<Tuple<MemoryStream, string, string>> DownloadFile(string filePath)
        {
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;
            return new Tuple<MemoryStream, string, string>(memoryStream, GetMimeType(filePath), Path.GetFileName(filePath));
        }
        public async Task<string> GetBase64(string filePath)
        {
            if (!File.Exists(filePath))
                return "";
            byte[] imageArray = await System.IO.File.ReadAllBytesAsync(filePath);
            return Convert.ToBase64String(imageArray);
        }

        public string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        public string SaveImageFromBase64(string Base64, string Folder)
        {
            var DestinationPath = "";
                //_Sales.GetFilePath();
            if (string.IsNullOrWhiteSpace(DestinationPath))
                DestinationPath = _configuration.GetValue<string>("StorageBasePath");
            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);
            if (!Directory.Exists($"{DestinationPath.Trim('\\')}\\{Folder}"))
                Directory.CreateDirectory($"{DestinationPath.Trim('\\')}\\{Folder}");
            string FilePath = Path.Combine(DestinationPath.Trim('\\'), Folder.Trim('\\'));
            string FileName = Guid.NewGuid().ToString() + ".png";
            File.WriteAllBytes(Path.Combine(FilePath, FileName), Convert.FromBase64String(Base64.Replace("data:image/png;base64,", "")));
            return FileName;
        }
        public async Task<string> GetBase64FromFolder(string Folder, string filename)
        {
            var DestinationPath = "";
                //_Sales.GetFilePath();
            if (string.IsNullOrWhiteSpace(DestinationPath))
                DestinationPath = _configuration.GetValue<string>("StorageBasePath");
            string FilePath = Path.Combine(DestinationPath.Trim('\\'), Folder.Trim('\\'));
            if (!File.Exists($"{FilePath}\\{filename}"))
                return "";
            byte[] imageArray = await System.IO.File.ReadAllBytesAsync($"{FilePath}\\{filename}");
            return "data:image/png;base64," + Convert.ToBase64String(imageArray);
        }

    }
}