using DinkToPdf;
using DinkToPdf.Contracts;

namespace HIMS.API.Extensions
{
    public class DinkToPdfService
    {
        private readonly IConverter _converter;

        public DinkToPdfService(IConverter converter)
        {
            _converter = converter;
        }

        public async Task<byte[]> GenerateDinkPdfAsync(string htmlContent)
        {
            return await Task.Run(() =>
            {
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = new GlobalSettings()
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Landscape,
                        PaperSize = PaperKind.A4,
                    },
                    Objects = { new ObjectSettings() { HtmlContent = htmlContent } }
                };

                return _converter.Convert(doc);
            });
        }
    }
}
