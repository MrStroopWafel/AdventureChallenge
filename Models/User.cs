using System;
using System.Collections.Generic;

namespace AdventureChallenge.Models
{
    public partial class User
    {
        public User()
        {
            UserChallenges = new HashSet<UserChallenge>();
        }

        public int Id { get; set; }
        public string Naam { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Wachtwoord { get; set; } = null!;
        public bool Beheer { get; set; }

        public virtual ICollection<UserChallenge> UserChallenges { get; set; }
    }
}
