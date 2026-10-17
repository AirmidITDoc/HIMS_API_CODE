using HIMS.Data.Models;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace HIMS.API.PdfUtility
{
    // ---- Adapt these to your existing DB / storage layer -----------------------------------
    public record StoredFile(int Id, string FileName, string FullPath);

    // -----------------------------------------------------------------------------------------

    public record MergeResult(string PdfPath, List<string> SkippedFiles);

    public interface ICategoryMergeService
    {
        Task<MergeResult?> MergeAsync(List<DocumentFile> files, string FolderPath, CancellationToken ct = default);
    }

    public class CategoryMergeService : ICategoryMergeService
    {
        private static readonly HashSet<string> ImageExt = new(StringComparer.OrdinalIgnoreCase)
        { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

        private static readonly HashSet<string> OfficeExt = new(StringComparer.OrdinalIgnoreCase)
        { ".doc", ".docx", ".rtf", ".odt", ".txt", ".xls", ".xlsx", ".ppt", ".pptx" };

        private readonly IOfficeToPdfConverter _converter;
        private readonly ILogger<CategoryMergeService> _logger;

        public CategoryMergeService(IOfficeToPdfConverter converter,
            ILogger<CategoryMergeService> logger)
        {
            _converter = converter;
            _logger = logger;
        }

        public async Task<MergeResult?> MergeAsync(List<DocumentFile> files, string FolderPath, CancellationToken ct = default)
        {
            //var files = await _repo.GetFilesAsync(categoryId, includeSubCategories, ct);
            if (files.Count == 0) return null;

            var workDir = Path.Combine(Path.GetTempPath(), "merge_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(workDir);

            var outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".pdf");
            var skipped = new List<string>();
            var openSources = new List<PdfDocument>(); // keep open until Save()

            try
            {
                using var output = new PdfDocument();

                foreach (var file in files)
                {
                    ct.ThrowIfCancellationRequested();
                    try
                    {
                        var ext = Path.GetExtension(file.SavedFileName);
                        var firstPageIndex = output.PageCount;

                        if (ext.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            AppendPdf(output, Path.Combine(FolderPath, file.SavedFileName), openSources);
                        }
                        else if (OfficeExt.Contains(ext))
                        {
                            // copy to workDir with a safe unique name (avoids odd chars / name clashes)
                            var safeInput = Path.Combine(workDir, $"{file.Id}{ext.ToLowerInvariant()}");
                            File.Copy(Path.Combine(FolderPath, file.SavedFileName), safeInput, true);
                            var pdfPath = await _converter.ConvertToPdfAsync(safeInput, workDir, ct);
                            AppendPdf(output, pdfPath, openSources);
                        }
                        else if (ImageExt.Contains(ext))
                        {
                            AppendImage(output, Path.Combine(FolderPath, file.SavedFileName));
                        }
                        else
                        {
                            skipped.Add($"{Path.Combine(FolderPath, file.SavedFileName)} (unsupported type)");
                            continue;
                        }

                        // Bookmark per source file (nice navigation in the merged PDF)
                        if (output.PageCount > firstPageIndex)
                            output.Outlines.Add(Path.Combine(FolderPath, file.SavedFileName), output.Pages[firstPageIndex], true);
                    }
                    catch (OperationCanceledException) { throw; }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Skipping {File} while merging category", Path.Combine(FolderPath, file.SavedFileName));
                        skipped.Add($"{Path.Combine(FolderPath, file.SavedFileName)} ({ex.Message})");
                    }
                }

                if (output.PageCount == 0) return null;

                output.Save(outputPath);
                return new MergeResult(outputPath, skipped);
            }
            finally
            {
                foreach (var d in openSources) d.Dispose();
                try { Directory.Delete(workDir, true); } catch { }
            }
        }

        private static void AppendPdf(PdfDocument output, string path, List<PdfDocument> openSources)
        {
            // Import mode = read-only, allows copying pages. Fails for password-protected PDFs.
            var src = PdfReader.Open(path, PdfDocumentOpenMode.Import);
            openSources.Add(src);
            foreach (var page in src.Pages)
                output.AddPage(page);
        }

        private static void AppendImage(PdfDocument output, string path)
        {
            using var img = XImage.FromFile(path);

            var page = output.AddPage();
            page.Size = PageSize.A4;
            page.Orientation = img.PointWidth > img.PointHeight
                ? PdfSharpCore.PageOrientation.Landscape
                : PdfSharpCore.PageOrientation.Portrait;

            const double margin = 20;
            var maxW = page.Width - 2 * margin;
            var maxH = page.Height - 2 * margin;
            var scale = Math.Min(Math.Min(maxW / img.PointWidth, maxH / img.PointHeight), 1.0); // never upscale
            var w = img.PointWidth * scale;
            var h = img.PointHeight * scale;

            using var gfx = XGraphics.FromPdfPage(page);
            gfx.DrawImage(img, (page.Width - w) / 2, (page.Height - h) / 2, w, h);
        }
    }
}
