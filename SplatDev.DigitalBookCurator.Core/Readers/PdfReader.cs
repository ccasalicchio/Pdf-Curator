using Microsoft.Extensions.Logging;

using SplatDev.DigitalBookCurator.Core.Extensions;
using SplatDev.DigitalBookCurator.Core.Models;

using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace SplatDev.DigitalBookCurator.Core.Readers
{
    public class PdfReader
    {
        public static string GetBookTitle(string path)
        {
            using PdfDocument document = PdfDocument.Open(path);
            return document.Information.Title;
        }

#pragma warning disable CA1822 // Mark members as static
        public Book? ReadPdf(ILogger logger, string path = "")
#pragma warning restore CA1822 // Mark members as static
        {
            string introText = "Introduction";
            Book? book = default;
            try
            {
                using (PdfDocument document = PdfDocument.Open(path))
                {
                    var fileInfo = new FileInfo(path);

#pragma warning disable CS8601 // Possible null reference assignment.
                    book = new Book
                    {
                        Author = document.Information.Author,
                        Creator = document.Information.Creator,
                        Format = fileInfo.Extension[1..],
                        Keywords = document.Information.Keywords?.Trim(),
                        Pages = document.NumberOfPages,
                        Producer = document.Information.Producer,
                        Size = fileInfo.Length,
                        Title = document.Information.Title?.CleanupFileName(),
                        Subject = document.Information.Subject,
                        Version = document.Version.ToString(),
                        Language = document.Structure.Catalog.CatalogDictionary.Data.ContainsKey("Lang") ? document.Structure.Catalog.CatalogDictionary.Data["Lang"].ToString() : "",
                        AddedDate = fileInfo.CreationTimeUtc,
                        CreatedDate = path.GetPdfCreationDate(),
                        Thumbnail = path.ExtractThumbnail(),
                    };
#pragma warning restore CS8601 // Possible null reference assignment.

                    book.Title = book.Title != null && book.Title.Contains("Microsoft Word") ? fileInfo.Name[0..^fileInfo.Extension.Length] : book.Title!;

                    try
                    {
                        if (document.GetPages().Any())
                        {
                            foreach (Page page in document.GetPages().Where(x => x.Text.Contains(introText)).ToList())
                            {
                                string pageText = page.Text;

                                if (pageText.Contains(introText))
                                {
                                    book.Introduction = pageText[pageText.IndexOf(introText)..];
                                }
                            }
                        }
                    }
                    catch
                    {
                        return book;
                    }
                }
                return book;
            }
            catch (Exception ex)
            {
                logger.LogError("Error Parsing Pdf: {ex}", ex.Message);
                return null;
            }
        }
    }
}