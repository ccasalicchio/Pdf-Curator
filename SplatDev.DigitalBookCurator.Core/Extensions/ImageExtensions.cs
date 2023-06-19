namespace SplatDev.DigitalBookCurator.Core.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] StreamToByteArray(MemoryStream? stream)
        {
            if (stream is null) return Array.Empty<byte>();
            byte[] byteArray = stream.ToArray();
            return byteArray;
        }
    }
}
