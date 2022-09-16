using System;
using System.Collections.Generic;

namespace AdventureChallenge.Models
{
    public partial class Hint
    {
        public Hint()
        {
            ChallengeHints = new HashSet<ChallengeHint>();
        }

        public int Id { get; set; }
        public string Beschrijving { get; set; } = null!;
        public string FontIcoon { get; set; } = null!;

        public virtual ICollection<ChallengeHint> ChallengeHints { get; set; }
    }
}
