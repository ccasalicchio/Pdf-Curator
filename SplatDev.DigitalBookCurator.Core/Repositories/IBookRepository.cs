using SplatDev.DigitalBookCurator.Core.Models;
using SplatDev.DigitalBookCurator.Core.ViewModels;
using SplatDev.Umbraco.Plugins.PdfCurator.Models;

namespace SplatDev.DigitalBookCurator.Core.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<PagedResults<BookViewModel>> GetFilteredBooksAsync(PagedFilter filter);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book?> GetBookByTitleAsync(string title);
        Task<Book?> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<Book?> DeleteBookAsync(int id);
    }
}
