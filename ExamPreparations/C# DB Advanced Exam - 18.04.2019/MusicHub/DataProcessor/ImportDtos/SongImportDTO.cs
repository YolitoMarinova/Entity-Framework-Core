namespace MusicHub.DataProcessor.ImportDtos
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using static Data.Models.Validations.DataValidator.Song;

    [XmlType("Song")]
    public class SongImportDTO
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("Duration")]
        public string Duration { get; set; }

        [Required]
        [XmlElement("CreatedOn")]
        public string CreatedOn { get; set; }

        [Required]
        [XmlElement("Genre")]
        public string Genre { get; set; }

        [XmlElement("AlbumId")]
        public int? AlbumId { get; set; }

        [XmlElement("WriterId")]
        public int WriterId { get; set; }
        
        [Range(MinPriceValue, double.MaxValue)]
        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
