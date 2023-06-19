namespace SplatDev.DigitalBookCurator.Core.Models
{
    public class Keywords
    {
        public string FullString { get; set; } = string.Empty;
        public IEnumerable<string> List { get; set; } = Enumerable.Empty<string>();
    }
}
