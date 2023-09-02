using SplatDev.DigitalBookCurator.Core.Models;

namespace SplatDev.Umbraco.Plugins.Models
{
    public class BookImportResult
    {
        public Book? Book { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
