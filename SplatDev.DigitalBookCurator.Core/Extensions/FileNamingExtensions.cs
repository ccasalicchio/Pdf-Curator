using System.Runtime.CompilerServices;
using System.Text;

using SplatDev.DigitalBookCurator.Core.Constants;

namespace SplatDev.DigitalBookCurator.Core.Extensions
{
    public static class FileNamingExtensions
    {
        public static string CleanupFileName(this string fileName)
        {
            var sb = new StringBuilder(fileName);
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                sb.Replace(c, ' ');
            }
            return sb
                .Replace('-', ' ')
                .Replace('_', ' ')
                .Replace('(', '-')
                .Replace(')', '-')
                .Replace(',', ' ')
                .Replace(':', ' ')
                .Replace("\\r\\n", " ")
                .Replace(FileExtensions.PDF[1..], "")
                .Replace(FileExtensions.PDF[1..].ToUpper(), "")
                .Replace(FileExtensions.CHM[1..], "")
                .Replace(FileExtensions.CHM[1..].ToUpper(), "")
                .Replace("  "," ")
                .ToString()
                .CapitalizeFirstLetter()
                .Trim();
        }

        private static string CapitalizeFirstLetter(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return char.ToUpper(text[0]) + text[1..];
        }
    }
}