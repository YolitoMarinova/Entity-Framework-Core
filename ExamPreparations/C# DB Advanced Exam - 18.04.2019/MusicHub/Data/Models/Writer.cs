namespace MusicHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Validations.DataValidator.Writer;

    public class Writer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [RegularExpression(PseudonymPattern)]
        public string Pseudonym { get; set; }

        public ICollection<Song> Songs { get; set; } = new HashSet<Song>();
    }
}
