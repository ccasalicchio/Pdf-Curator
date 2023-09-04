namespace SplatDev.DigitalBookCurator.Core.Models
{
    public interface IBook
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Creator { get; set; }
        public string? Keywords { get; set; }
        public string? Producer { get; set; }
        public string? Subject { get; set; }
        public string Title { get; set; }
        public string? Introduction { get; set; }
        public string? Version { get; set; }
        public int? Pages { get; set; }
        public long Size { get; set; }
        public string Format { get; set; }
        public string? Language { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? AddedDate { get; set; }
        public string FileName { get; set; }
    }
}
