namespace MusicHub.Data.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Validations.DataValidator.Album;

    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price => 
            this.Songs.Sum(s => s.Price);

        public int? ProducerId { get; set; }

        public Producer Producer { get; set; }

        public ICollection<Song> Songs { get; set; } = new HashSet<Song>();
    }
}
