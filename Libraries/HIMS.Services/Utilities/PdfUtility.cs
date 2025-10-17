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


        public string GetHeader(string filePath, long HospitalId = 0)
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

            //RS
            string logoFileName = (objHospital?.Header ?? "").ConvertToString();

            var HospitalLogo = string.IsNullOrWhiteSpace(logoFileName) ? "" : GetBase64FromFolder("Hospital\\Logo",objHospital?.Header.ConvertToString());

            htmlHeader = htmlHeader.Replace("{{Header}}", HospitalLogo);


            return htmlHeader.Replace("{{Display}}", (objHospital?.HospitalId ?? 0) > 0 ? "visible" : "hidden");


            //return htmlHeader.Replace("{{BaseUrl}}", basePath.Trim('/'));
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

        public Tuple<byte[], string> GeneratePdfFromHtml(string html, string storageBasePath, string FolderName, string FileName = "", Orientation PageOrientation = Orientation.Portrait, PaperKind PaperSize = PaperKind.A4)
        {

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
                        FooterSettings = { FontSize = 9, Left = "AirmidTech Innovation Pvt. Ltd | M : 9970164262", Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
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
        public Tuple<byte[], string> GeneratePdfFromHtmlA5(string html,string storageBasePath,string FolderName,string FileName = "",Orientation PageOrientation = Orientation.Portrait,PaperKind PaperSize = PaperKind.A5) // Default to A5 size
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    ColorMode = ColorMode.Color,
                    Orientation = PageOrientation,
                    PaperSize = PaperSize, // Now defaults to A5
                    Margins = new MarginSettings() { Top = 8, Bottom = 8, Left = 8, Right = 8 },
                    // Scale = 0.9f, // Uncomment if scaling is needed
                },
                Objects = 
        {
            new ObjectSettings()
            {
                PagesCount = true,
                HtmlContent = html,
                WebSettings = new WebSettings()
                {
                    DefaultEncoding = "utf-8"
                },
                FooterSettings = new FooterSettings()
                {
                    FontSize = 9,
                    Left = "AirmidTech Innovation Pvt. Ltd | M : 9970164262",
                    Right = "Page [page] of [toPage]",
                    Line = true,
                    Spacing = 2.812
                }
            }
        }
            };

            // Convert HTML to PDF
            byte[] bytes = converter.Convert(doc);

            // Determine and create storage path
            string DestinationPath = string.IsNullOrWhiteSpace(storageBasePath) ? "" : storageBasePath;
            string dateFolder = DateTime.Now.ToString("ddMMyyyy");
            string fullFolderPath = Path.Combine(DestinationPath, FolderName, dateFolder);

            if (!Directory.Exists(fullFolderPath))
                Directory.CreateDirectory(fullFolderPath);

            // Generate unique filename if not provided
            string fileBaseName = string.IsNullOrWhiteSpace(FileName) ? Guid.NewGuid().ToString() : FileName;
            string newFileName = Path.Combine(fullFolderPath, $"{fileBaseName}.pdf");

            // Avoid file name conflict
            if (File.Exists(newFileName))
                newFileName = Path.Combine(fullFolderPath, $"{fileBaseName}_{Guid.NewGuid()}.pdf");

            // Save the PDF
            File.WriteAllBytes(newFileName, bytes);

            return new Tuple<byte[], string>(bytes, newFileName);
        }
        public Tuple<byte[], string> GeneratePdfFromHtmlThermal(string html,string storageBasePath,string FolderName,string FileName = "",Orientation PageOrientation = Orientation.Portrait,PaperKind PaperSize = PaperKind.Custom) // You won't use this since it's overridden below
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
            ColorMode = ColorMode.Color,
            Orientation = PageOrientation,
            PaperSize = new PechkinPaperSize("30mm", "1000mm"), // 3cm width, height = max A4 height (you can increase if needed)
            Margins = new MarginSettings()
            {
                Top = 5,
                Bottom = 10, // Add bottom space for footer
                Left = 0,
                Right = 0
            }
        },
                Objects = {
            new ObjectSettings() {
                PagesCount = true,
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8" },
                FooterSettings = new FooterSettings()
                {
                    FontSize = 7,
                    Right = "Page [page] of [toPage]",
                    Line = true,
                    Spacing = 1.5
                }
            }
        }
            };

            byte[] bytes = converter.Convert(doc);

            string DestinationPath = string.Empty;
            if (string.IsNullOrWhiteSpace(DestinationPath))
                DestinationPath = storageBasePath;

            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);

            string datedFolderPath = Path.Combine(DestinationPath, FolderName, DateTime.Now.ToString("ddMMyyyy"));
            if (!Directory.Exists(datedFolderPath))
                Directory.CreateDirectory(datedFolderPath);

            string newFileName = Path.Combine(datedFolderPath, (string.IsNullOrWhiteSpace(FileName) ? Guid.NewGuid().ToString() : FileName) + ".pdf");

            if (File.Exists(newFileName))
                newFileName = Path.Combine(datedFolderPath, FileName + "_" + Guid.NewGuid().ToString() + ".pdf");

            File.WriteAllBytes(newFileName, bytes);

            return new Tuple<byte[], string>(bytes, newFileName);
        }

        public Tuple<byte[], string> GeneratePdfFromHtmlBarCode(string html, string storageBasePath, string FolderName, string FileName = "", Orientation PageOrientation = Orientation.Portrait, PaperKind PaperSize = PaperKind.A4)
        {

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = PageOrientation,//Orientation.Portrait,
                    //PaperSize = PaperSize,//PaperKind.A4,
                    //Margins = new MarginSettings() { Top = 10, Bottom=10, Left=10, Right=10 },
                    //Scale = 0.9f,  // Reduce size to 90% of the original content size
                    
                    //PageSize need to define 
                    //Width : 103 mm
                    //height : 25 mm
                    PaperSize = new PechkinPaperSize("103mm", "25mm"),
                      Margins = new MarginSettings()
                        {
                            Top = 0,
                            Bottom = 0,
                            Left = 0,
                            Right = 0
                        }

                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                       // FooterSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
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

        public string GetBase64FromFolder(string Folder, string filename)
        {
            var DestinationPath = "";
            //_Sales.GetFilePath();
            if (string.IsNullOrWhiteSpace(DestinationPath))
                DestinationPath = _configuration["StorageBaseUrl"];
            //string fullFilePath = "E:\\Storage\\Hospital\\Logo\\hospitallogo.jfif";

            string FilePath = Path.Combine(DestinationPath.Trim('\\'), Folder.Trim('\\'));
          string  fullFilePath = Path.Combine(FilePath, filename);

          
            if (!File.Exists(fullFilePath))
                return "";

            byte[] imageArray = File.ReadAllBytes(fullFilePath);
            return "data:image/png;base64," + Convert.ToBase64String(imageArray);
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
