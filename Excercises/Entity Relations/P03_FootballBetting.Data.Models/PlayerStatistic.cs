﻿namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PlayerStatistic
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }

        [Required]
        public int ScoredGoals { get; set; }

        [Required]
        public int Assists { get; set; }

        [Required]
        public int MinutesPlayed { get; set; }
    }
}
