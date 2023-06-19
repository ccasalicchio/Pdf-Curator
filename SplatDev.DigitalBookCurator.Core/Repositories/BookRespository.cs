using Microsoft.EntityFrameworkCore;

using SplatDev.DigitalBookCurator.Core.Context;
using SplatDev.DigitalBookCurator.Core.Models;

namespace SplatDev.DigitalBookCurator.Core.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly CuratorDbContext context;

        public BookRepository(CuratorDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<Book?> GetBookByTitleAsync(string title)
        {
            return await context.Books.FirstOrDefaultAsync(b => b.Title == title);
        }

        public async Task<Book?> AddBookAsync(Book book)
        {
            var result = await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            var result = context.Books.Update(book);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Book?> DeleteBookAsync(int id)
        {
            var result = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (result != null)
            {
                context.Books.Remove(result);
                await context.SaveChangesAsync();
            }
            return result;
        }
    }
}
