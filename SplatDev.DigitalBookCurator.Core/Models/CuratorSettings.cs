namespace SplatDev.DigitalBookCurator.Core.Models
{
    public class CuratorSettings
    {
        public string Origin { get; set; } = Environment.CurrentDirectory;
        public string Destination { get; set; } = $"..\\{Environment.CurrentDirectory}";
        public bool DeleteEmptyFolders { get; set; } = false;
    }
}
