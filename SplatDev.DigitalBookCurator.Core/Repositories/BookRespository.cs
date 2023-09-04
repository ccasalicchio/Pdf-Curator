using Microsoft.EntityFrameworkCore;

using SplatDev.DigitalBookCurator.Core.Context;
using SplatDev.DigitalBookCurator.Core.Models;
using SplatDev.DigitalBookCurator.Core.ViewModels;
using SplatDev.Umbraco.Plugins.PdfCurator.Models;

using System.Linq.Dynamic.Core;

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

        public async Task<PagedResults<BookViewModel>> GetFilteredBooksAsync(PagedFilter filter)
        {
            var books = context.Books.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Term))
            {
                books = books.Where(b =>
                b.Title.Contains(filter.Term) ||
                (b.Author != null && b.Author.Contains(filter.Term)) ||
                (b.Introduction != null && b.Introduction.Contains(filter.Term)));
            }
            var pagedResults = new PagedResults<BookViewModel>
            {
                TotalResults = books.Count(),
                TotalPages = (int)Math.Ceiling((double)books.Count() / filter.PageSize)
            };
            books = books.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);
            books = books.OrderBy($"{filter.Order} {filter.Direction}");
            var list = await books.Select(x => new BookViewModel(x)).ToListAsync();
            pagedResults.Results = list;
            return pagedResults;
        }
    }
}
