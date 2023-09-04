using SplatDev.DigitalBookCurator.Core.Models;

namespace SplatDev.Umbraco.Plugins.PdfCurator.Models
{
    public class PagedFilter
    {
        public string? Term { get; set; }
        public string? Order { get; set; }
        public string? Direction { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
