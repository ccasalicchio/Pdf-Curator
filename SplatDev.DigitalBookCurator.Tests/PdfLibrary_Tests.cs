using IronPdf;
namespace SplatDev.DigitalBookCurator.Tests
{
    [TestClass]
    public class PdfLibrary_Tests
    {
        [TestMethod]
        public void PdfLibrary_IronPdf()
        {
            try
            {
                var reader = IronPdf.PdfDocument.FromFile(@"D:\Curator\NEW\c++ how to program 8th.edition.pdf");
                Assert.IsNotNull(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}