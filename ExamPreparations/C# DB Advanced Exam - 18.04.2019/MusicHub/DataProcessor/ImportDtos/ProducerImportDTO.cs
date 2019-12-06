namespace MusicHub.DataProcessor.ImportDtos
{
    using Newtonsoft.Json;

    using System.ComponentModel.DataAnnotations;

    using static Data.Models.Validations.DataValidator.Producer;

    public class ProducerImportDTO
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [RegularExpression(PseudonymPattern)]
        public string Pseudonym { get; set; }

        [RegularExpression(PhoneNumberPattern)]
        public string PhoneNumber { get; set; }

        [JsonProperty("Albums")]
        public AlbumProducerImportDTO[] AlbumsDtos { get; set; } 
    }
}
