using System;
using System.Collections.Generic;

namespace AdventureChallenge.Models
{
    public partial class Foto
    {
        public Foto()
        {
            UserChallenges = new HashSet<UserChallenge>();
        }

        public int Id { get; set; }
        public byte[] Foto1 { get; set; } = null!;

        public virtual ICollection<UserChallenge> UserChallenges { get; set; }
    }
}
