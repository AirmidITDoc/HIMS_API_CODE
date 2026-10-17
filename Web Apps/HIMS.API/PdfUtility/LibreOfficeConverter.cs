using System.Diagnostics;

namespace HIMS.API.PdfUtility
{
    public interface IOfficeToPdfConverter
    {
        /// <summary>Converts a Word/Excel/PPT/etc. file to PDF and returns the PDF path.</summary>
        Task<string> ConvertToPdfAsync(string inputPath, string outputDir, CancellationToken ct);
    }

    /// <summary>
    /// Uses LibreOffice (MPL 2.0, open source) in headless mode.
    /// Each conversion gets its own profile dir so conversions can run in parallel safely.
    /// </summary>
    public class LibreOfficeConverter : IOfficeToPdfConverter
    {
        // Limit parallel soffice processes (each uses ~200-400 MB RAM)
        private static readonly SemaphoreSlim Gate = new(2);
        private readonly string _sofficePath;
        private readonly ILogger<LibreOfficeConverter> _logger;

        public LibreOfficeConverter(IConfiguration config, ILogger<LibreOfficeConverter> logger)
        {
            _logger = logger;
            // appsettings.json: "LibreOffice": { "Path": "soffice" }
            // Windows: C:\Program Files\LibreOffice\program\soffice.exe
            _sofficePath = config["LibreOffice:Path"] ?? "soffice";
        }

        public async Task<string> ConvertToPdfAsync(string inputPath, string outputDir, CancellationToken ct)
        {
            await Gate.WaitAsync(ct);
            var profileDir = Path.Combine(Path.GetTempPath(), "lo_profile_" + Guid.NewGuid().ToString("N"));
            try
            {
                Directory.CreateDirectory(outputDir);

                var psi = new ProcessStartInfo
                {
                    FileName = _sofficePath,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                psi.ArgumentList.Add("--headless");
                psi.ArgumentList.Add("--norestore");
                psi.ArgumentList.Add("--nolockcheck");
                psi.ArgumentList.Add($"-env:UserInstallation={new Uri(profileDir).AbsoluteUri}");
                psi.ArgumentList.Add("--convert-to");
                psi.ArgumentList.Add("pdf");
                psi.ArgumentList.Add("--outdir");
                psi.ArgumentList.Add(outputDir);
                psi.ArgumentList.Add(inputPath);

                using var process = new Process { StartInfo = psi };
                process.Start();

                using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
                timeout.CancelAfter(TimeSpan.FromMinutes(2));

                try
                {
                    await process.WaitForExitAsync(timeout.Token);
                }
                catch (OperationCanceledException)
                {
                    try { process.Kill(entireProcessTree: true); } catch { /* ignore */ }
                    throw new TimeoutException($"LibreOffice conversion timed out for {Path.GetFileName(inputPath)}");
                }

                var outputPdf = Path.Combine(outputDir,
                    Path.GetFileNameWithoutExtension(inputPath) + ".pdf");

                if (process.ExitCode != 0 || !File.Exists(outputPdf))
                {
                    var err = await process.StandardError.ReadToEndAsync();
                    _logger.LogError("LibreOffice failed for {File}: {Err}", inputPath, err);
                    throw new InvalidOperationException($"Could not convert {Path.GetFileName(inputPath)} to PDF");
                }

                return outputPdf;
            }
            finally
            {
                try { if (Directory.Exists(profileDir)) Directory.Delete(profileDir, true); } catch { }
                Gate.Release();
            }
        }
    }
}
