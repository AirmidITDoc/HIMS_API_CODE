using HIMS.Data.Models;
using Microsoft.Extensions.Configuration;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace HIMS.Services.Utilities
{
    public class PdfUtility : IPdfUtility
    {
        private readonly Data.Models.HIMSDbContext _context;
        private static IConverter converter = new SynchronizedConverter(new PdfTools());
        public readonly IConfiguration _configuration;

        public PdfUtility(HIMSDbContext HIMSDbContext, IConverter _converter, IConfiguration configuration)
        {
            _context = HIMSDbContext;
            converter = _converter;
            _configuration = configuration;
        }

        //public string GetHeader(string filePath, string basePath, long HospitalId = 0)
        //{
        //    string htmlHeader = System.IO.File.ReadAllText(filePath);
        //    HospitalMaster objHospital = _context.HospitalMasters.Find(Convert.ToInt64(1));
        //    //HospitalMaster objHospital = _context.HospitalMasters.Where(x => x.HospitalId == 1).FirstOrDefault();
        //    htmlHeader = htmlHeader.Replace("{{HospitalName}}", objHospital?.HospitalName ?? "");
        //    htmlHeader = htmlHeader.Replace("{{Address}}", objHospital?.HospitalAddress ?? "");
        //    htmlHeader = htmlHeader.Replace("{{City}}", objHospital?.City ?? "");
        //    htmlHeader = htmlHeader.Replace("{{Pin}}", objHospital?.Pin ?? "");
        //    htmlHeader = htmlHeader.Replace("{{Phone}}", objHospital?.Phone ?? "");
        //    htmlHeader = htmlHeader.Replace("{{Display}}", (objHospital?.HospitalId ?? 0) > 0 ? "visible" : "hidden");
        //    return htmlHeader.Replace("{{BaseUrl}}", basePath.Trim('/'));
        //}


        public string GetHeader(string filePath, string basePath, long HospitalId = 0)
        {
            string htmlHeader = System.IO.File.ReadAllText(filePath);
            HospitalMaster objHospital = _context.HospitalMasters.Find(Convert.ToInt64(1));
            htmlHeader = htmlHeader.Replace("{{HospitalName}}", objHospital?.HospitalName ?? "");
            htmlHeader = htmlHeader.Replace("{{Address}}", objHospital?.HospitalAddress ?? "");
            htmlHeader = htmlHeader.Replace("{{City}}", objHospital?.City ?? "");
            htmlHeader = htmlHeader.Replace("{{Pin}}", objHospital?.Pin ?? "");
            htmlHeader = htmlHeader.Replace("{{Phone}}", objHospital?.Phone ?? "");
            htmlHeader = htmlHeader.Replace("{{HospitalHeaderLine}}", objHospital?.HospitalHeaderLine ?? "");
            htmlHeader = htmlHeader.Replace("{{EmailID}}", objHospital?.EmailId ?? "");
            htmlHeader = htmlHeader.Replace("{{WebSiteInfo}}", objHospital?.WebSiteInfo ?? "");
            htmlHeader = htmlHeader.Replace("{{Display}}", (objHospital?.HospitalId ?? 0) > 0 ? "visible" : "hidden");
            return htmlHeader.Replace("{{BaseUrl}}", basePath.Trim('/'));
            //return htmlHeader.Replace("{{BaseUrl}}", _configuration.GetValue<string>("BaseUrl").Trim('/'));

        }
        //public string GetHeader(int Id, int Type = 1)
        //{
        //    if (Type == 1)
        //    {
        //        HospitalMaster objHospital = _Hospital.GetHospitalById(Id);
        //        return objHospital.Header;
        //    }
        //    else
        //    {
        //        HospitalStoreMaster objHospital = _Hospital.GetHospitalStoreById(Id);
        //        return objHospital.Header;
        //    }
        //}

        //public string GetTemplateHeader(int Id)
        //{
        //    M_ReportTemplateConfig objTemplate = _Hospital.GetTemplateById(Id);
        //    return objTemplate.TemplateDescription;
        //}

        //public string GetStoreHeader(string filePath, string basePath, long StoreId = 0)
        //{
        //    string htmlHeader = System.IO.File.ReadAllText(filePath);
        //    HospitalStoreMaster objStoreHospital = _context.MStoreMasters.Find(Convert.ToInt64(2));
        //    htmlHeader = htmlHeader.Replace("{{PrintStoreName}}", objStoreHospital?.PrintStoreName ?? "");
        //    htmlHeader = htmlHeader.Replace("{{StoreAddress}}", objStoreHospital?.StoreAddress ?? "");
        //    htmlHeader = htmlHeader.Replace("{{HospitalMobileNo}}", objStoreHospital?.HospitalMobileNo ?? "");
        //    htmlHeader = htmlHeader.Replace("{{HospitalEmailId}}", objStoreHospital?.HospitalEmailId ?? "");
        //    htmlHeader = htmlHeader.Replace("{{PrintStoreUnitName}}", objStoreHospital?.PrintStoreUnitName ?? "");
        //    htmlHeader = htmlHeader.Replace("{{DL_NO}}", objStoreHospital?.DL_NO ?? "");
        //    htmlHeader = htmlHeader.Replace("{{GSTIN}}", objStoreHospital?.GSTIN ?? "");
        //    htmlHeader = htmlHeader.Replace("{{Display}}", (objStoreHospital?.StoreId ?? 0) > 0 ? "visible" : "hidden");
        //    return htmlHeader.Replace("{{BaseUrl}}", basePath.Trim('/'));
        //}

        public Tuple<byte[], string> GeneratePdfFromHtml(string html, string storageBasePath , string FolderName, string FileName = "", Orientation PageOrientation = Orientation.Portrait, PaperKind PaperSize = PaperKind.A4)
        {
            //var options = new ConvertOptions
            //{
            //    EnableForms = true,
            //    PageOrientation = PageOrientation
            //};

            //_generatePdf.SetConvertOptions(options);
            //var pdf = _generatePdf.GetPDF(html);

            //------------
            //var converter = new BasicConverter(new PdfTools());
            //var converter = new SynchronizedConverter(new PdfTools());
            //converter.PhaseChanged += (sender, e) =>
            //{
            //    Console.WriteLine("Phase changed {0} - {1}", e.CurrentPhase, e.Description);
            //};
            //converter.ProgressChanged += (sender, e) =>
            //{
            //    Console.WriteLine("Progress changed {0}", e.Description);
            //};
            //converter.Finished += (sender, e) =>
            //{
            //    Console.WriteLine("Conversion {0} ", e.Success ? "successful" : "unsucessful");
            //};
            //converter.Warning += (sender, e) =>
            //{
            //    Console.WriteLine("[WARN] {0}", e.Message);
            //};
            //converter.Error += (sender, e) =>
            //{
            //    Console.WriteLine("[ERROR] {0}", e.Message);
            //};

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = PageOrientation,//Orientation.Portrait,
                    PaperSize = PaperSize,//PaperKind.A4,
                    Margins = new MarginSettings() { Top = 10, Bottom=10, Left=10, Right=10 },
                    //Scale = 0.9f,  // Reduce size to 90% of the original content size
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        FooterSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };
            byte[] bytes = converter.Convert(doc);
            //var pdfStream = new System.IO.MemoryStream();
            //pdfStream.Write(pdf, 0, pdf.Length);
            //pdfStream.Position = 0;
            //Byte[] bytes = pdfStream.ToArray();
            string DestinationPath = string.Empty; //_Sales.GetFilePath();
            if (string.IsNullOrWhiteSpace(DestinationPath))
                DestinationPath = storageBasePath;// _configuration.GetValue<string>("StorageBasePath");
            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);
            if (!Directory.Exists(DestinationPath.Trim('\\') + "\\" + FolderName + "\\" + DateTime.Now.ToString("ddMMyyyy")))
                Directory.CreateDirectory(DestinationPath.Trim('\\') + "\\" + FolderName + "\\" + DateTime.Now.ToString("ddMMyyyy"));
            string NewFileName = DestinationPath.Trim('\\') + "\\" + FolderName + "\\" + DateTime.Now.ToString("ddMMyyyy") + "\\" + (string.IsNullOrWhiteSpace(FileName) ? Guid.NewGuid().ToString() : FileName) + ".pdf";
            if (File.Exists(NewFileName))
                NewFileName = DestinationPath.Trim('\\') + "\\" + FolderName + "\\" + DateTime.Now.ToString("ddMMyyyy") + "\\" + FileName + "_" + (Guid.NewGuid()) + ".pdf";
            System.IO.File.WriteAllBytes(NewFileName, bytes);
            return new Tuple<byte[], string>(bytes, NewFileName);
        }



        //public Tuple<byte[], string> CreateExel(string html, string FolderName, string FileName = "", Orientation PageOrientation = Orientation.Portrait)
        //{
        //    // throw new NotImplementedException();
        //    // Instantiate a Workbook object that represents Excel file.
        //    var pdfStream = new System.IO.MemoryStream();
        //    Byte[] bytes = pdfStream.ToArray();
        //    Workbook wb = new Workbook();

        //    // When you create a new workbook, a default "Sheet1" is added to the workbook.
        //    Worksheet sheet = wb.Worksheets[0];

        //    // Access the "A1" cell in the sheet.
        //    Cell cell = sheet.Cells["A1"];

        //    // Input the "Hello World!" text into the "A1" cell.
        //    cell.PutValue("Hello World!");

        //    // Save the Excel as .xlsx file.
        //    wb.Save("Excel.xlsx", SaveFormat.Xlsx);
        //    String st = "ok";

        //    return new Tuple<byte[], string>(bytes, st); ;
        //}
    }
}
