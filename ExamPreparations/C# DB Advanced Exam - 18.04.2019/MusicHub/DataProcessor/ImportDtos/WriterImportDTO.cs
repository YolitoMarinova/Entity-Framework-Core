namespace MusicHub.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Models.Validations.DataValidator.Writer;

    public class WriterImportDTO
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [RegularExpression(PseudonymPattern)]
        public string Pseudonym { get; set; }
    }
}
