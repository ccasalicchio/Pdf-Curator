namespace SplatDev.DigitalBookCurator.Core.Models
{
    public class PagedResults<T> where T : class
    {
        public int TotalPages { get; set; } = 0;
        public int TotalResults { get; set; } = 0;
        public IEnumerable<T>? Results { get; set; }
    }
}
