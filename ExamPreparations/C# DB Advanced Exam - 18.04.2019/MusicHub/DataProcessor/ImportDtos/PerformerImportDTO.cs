namespace MusicHub.DataProcessor.ImportDtos
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using static Data.Models.Validations.DataValidator.Performer;

    [XmlType("Performer")]
    public class PerformerImportDTO
    {

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        [XmlElement("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        [XmlElement("LastName")]
        public string LastName { get; set; }

        [Range(MinAgeValue, MaxAgeValue)]
        [XmlElement("Age")]
        public int Age { get; set; }

        [Range(MinNetWorthValue, double.MaxValue)]
        [XmlElement("NetWorth")]
        public decimal NetWorth { get; set; }

        [XmlArray("PerformersSongs")]
        public SongPerformerDTO[] PerformersSongs { get; set; }
    }
}
