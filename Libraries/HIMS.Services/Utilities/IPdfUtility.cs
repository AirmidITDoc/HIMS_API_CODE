﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet;

namespace HIMS.Services.Utilities
{
    public interface IPdfUtility
    {
        Tuple<byte[], string> GeneratePdfFromHtml(string html, string storageBasePath , string FolderName, string FileName = "", Orientation PageOrientation = Orientation.Portrait, PaperKind PaperSize = PaperKind.A4);

        //Tuple<byte[], string> CreateExel(string html, string FolderName, string FileName = "", Wkhtmltopdf.NetCore.Options.Orientation PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
        //bool CreateExel();
      
        //Tuple<byte[], string> CreateExel(string html, string FolderName, string FileName = "", Wkhtmltopdf.NetCore.Options.Orientation PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
        //bool CreateExel();
        string GetHeader(string filePath, string basePath, long HospitalId = 0);
        //string GetStoreHeader(string filePath, long StoreId = 0);
        //string GetHeader(int Id, int Type = 1);
        //string GetTemplateHeader(int Id);
    }
}
