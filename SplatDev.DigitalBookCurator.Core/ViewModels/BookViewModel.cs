using SplatDev.DigitalBookCurator.Core.Models;

namespace SplatDev.DigitalBookCurator.Core.ViewModels
{
    public class BookViewModel : Book
    {
        public string? ThumbnailBase64 { get; set; }


        public BookViewModel(Book book)
        {
            ThumbnailBase64 = book.Thumbnail is not null ? Convert.ToBase64String(book.Thumbnail) : string.Empty;
            Thumbnail = Array.Empty<byte>();
            AddedDate = book.AddedDate;
            CreatedDate = book.CreatedDate;
            FileName = book.FileName;
            Creator = book.Creator;
            Author = book.Author;
            Format = book.Format;
            Id = book.Id;
            Introduction = book.Introduction;
            Keywords = book.Keywords;
            Pages = book.Pages;
            Language = book.Language;
            Producer = book.Producer;
            Size = book.Size;
            Subject = book.Subject;
            Title = book.Title;
            Version = book.Version;
        }
    }
}
