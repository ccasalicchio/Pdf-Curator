using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace SplatDev.DigitalBookCurator.Core.Models
{
    [Table(TABLENAME)]
    [PrimaryKey("Id")]
    [Index(nameof(FileName), nameof(Title))]
    public class Book
    {
        public const string TABLENAME = "Books";

        [Key]
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Creator { get; set; }
        public string? Keywords { get; set; }
        public string? Producer { get; set; }
        public string? Subject { get; set; }
        public string Title { get; set; } = string.Empty;

        [Column("Introduction", TypeName = "ntext")]
        [MaxLength(500)]
        public string? Introduction { get; set; }
        public string? Version { get; set; }
        public int? Pages { get; set; }
        public long Size { get; set; }
        public string Format { get; set; } = string.Empty;
        public string? Language { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? AddedDate { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        public byte[] Thumbnail { get; set; } = Array.Empty<byte>();
    }
}
