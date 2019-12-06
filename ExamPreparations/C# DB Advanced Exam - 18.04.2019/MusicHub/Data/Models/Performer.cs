namespace MusicHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Validations.DataValidator.Performer;

    public class Performer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string LastName { get; set; }

        [Range(MinAgeValue, MaxAgeValue)]
        public int Age { get; set; }

        [Range(MinNetWorthValue, double.MaxValue)]
        public decimal NetWorth { get; set; }

        public ICollection<SongPerformer> PerformerSongs { get; set; } = new HashSet<SongPerformer>();
    }
}
