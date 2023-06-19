using SplatDev.DigitalBookCurator.Core.Models;

namespace SplatDev.DigitalBookCurator.Core.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book?> GetBookByTitleAsync(string title);
        Task<Book?> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<Book?> DeleteBookAsync(int id);
    }
}
