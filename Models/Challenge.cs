using System;
using System.Collections.Generic;

namespace AdventureChallenge.Models
{
    public partial class Challenge
    {
        public Challenge()
        {
            ChallengeHints = new HashSet<ChallengeHint>();
            UserChallenges = new HashSet<UserChallenge>();
        }

        public int Id { get; set; }
        public decimal? Prijs { get; set; }
        public string? Tijdstip { get; set; }
        public string? Personen { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<ChallengeHint> ChallengeHints { get; set; }
        public virtual ICollection<UserChallenge> UserChallenges { get; set; }
    }
}
