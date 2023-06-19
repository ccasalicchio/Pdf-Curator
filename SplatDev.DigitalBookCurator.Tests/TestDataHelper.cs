using SplatDev.DigitalBookCurator.Core.Models;

namespace SplatDev.DigitalBookCurator.Tests
{
    public static class TestDataHelper
    {
        public static List<Book> GetFakeBookList()
        {
            return new List<Book>()
            {
                new Book
                {
                    Id = 1
                },
                new Book
                {
                    Id = 2
                }
            };
        }
    }
}
