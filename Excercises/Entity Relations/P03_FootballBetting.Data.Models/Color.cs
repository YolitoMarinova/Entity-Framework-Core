﻿namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Color
    {
        [Key]
        public int ColorId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<Team> PrimaryKitTeams { get; set; } = new HashSet<Team>();

        public ICollection<Team> SecondaryKitTeams { get; set; } = new HashSet<Team>();
    }
}
