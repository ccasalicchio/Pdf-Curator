using Aspose.Pdf;
using Aspose.Pdf.Devices;

namespace SplatDev.DigitalBookCurator.Core.Extensions
{
    public static class PdfExtensions
    {
        public static DateTime? GetPdfCreationDate(this string path)
        {
            Document pdfDocument = new(path);
            try
            {
                return pdfDocument.Info.CreationDate;

            }
            catch
            {
                return DateTime.Now.Date;
            }
        }

        public static byte[] DefaultPdfThumbnail(this string path)
        {
            using FileStream img = File.Open(path, FileMode.Open);
            MemoryStream stream = new();
            img.CopyTo(stream);
            return ImageExtensions.StreamToByteArray(stream);
        }

        public static byte[]? ExtractThumbnail(this string path)
        {
            try
            {
                // Open document
                Document pdfDocument = new(path);

                // Define Resolution
                Resolution resolution = new(300);

                // Create Png device with specified attributes
                // Width, Height, Resolution
                PngDevice PngDevice = new(500, 700, resolution);

                // Convert a particular page and save the image to stream
                using MemoryStream output = new();

                PngDevice.Process(pdfDocument.Pages[1], output);
                return ImageExtensions.StreamToByteArray(output);

            }
            catch
            {
                return Array.Empty<byte>();
            }
        }
    }
}
