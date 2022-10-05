using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using Xunit;

namespace AdventureChallenge.Models.FormModels
{
    public class SearchForm
    {
        public SearchForm()
        {
            ChallengeHints = new HashSet<ChallengeHint>();
            UserChallenges = new HashSet<UserChallenge>();
        }

        public int Id { get; set; }
        public decimal Prijs { get; set; }
        public string Tijdstip { get; set; }
        public int Personen { get; set; }
        public string Status { get; set; }
        public decimal Tijdduur { get; set; }
        public bool Icon0 { get; set; }
        public bool Icon1 { get; set; }
        public bool Icon2 { get; set; }
        public bool Icon3 { get; set; }
        public bool Icon4 { get; set; }
        public bool Icon5 { get; set; }
        public bool Icon6 { get; set; }
        public bool Icon7 { get; set; }
        public bool Icon8 { get; set; }


        public virtual ICollection<ChallengeHint> ChallengeHints { get; set; }
        public virtual ICollection<UserChallenge> UserChallenges { get; set; }
    }
}
