using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using Xunit;

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
        public decimal Prijs { get; set; }
        public string Tijdstip { get; set; }
        public string Personen { get; set; }
        public string Status { get; set; }
        public decimal Tijdduur { get; set; }
        public string Opdracht { get; set; }

        public virtual ICollection<ChallengeHint> ChallengeHints { get; set; }
        public virtual ICollection<UserChallenge> UserChallenges { get; set; }
    }
}
