using Microsoft.Extensions.Logging;

using Moq;
using Moq.EntityFrameworkCore;

using SplatDev.DigitalBookCurator.Core.Context;
using SplatDev.DigitalBookCurator.Core.Models;
using SplatDev.DigitalBookCurator.Core.Readers;
using SplatDev.DigitalBookCurator.Core.Repositories;

namespace SplatDev.DigitalBookCurator.Tests
{
    [TestClass]
    public class BookReader_Tests
    {
        [TestMethod]
        public void BookReader_ReadPdf()
        {
            //Arrange
            Mock<ILogger> logger = new();
            var fullText = new PdfReader().ReadPdf(logger: logger.Object);

            //Act

            //Assert
            Assert.IsNotNull(fullText);
        }

        [TestMethod]
        public async Task BookReader_GetCatalogBooks()
        {
            //Arrange
            var db = new Mock<CuratorDbContext>();
            db.Setup(x => x.Books).ReturnsDbSet(TestDataHelper.GetFakeBookList());

            //Act
            var bookRepo = new BookRepository(db.Object);
            var books = await bookRepo.GetAllBooksAsync();

            //Assert
            Assert.IsNotNull(books);
            Assert.AreEqual(2, books.Count());
        }
    }
}