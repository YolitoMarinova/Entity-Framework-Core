namespace MusicHub.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Models.Validations.DataValidator.Album;

    public class AlbumProducerImportDTO
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        public string ReleaseDate { get; set; }
    }
}
