using System;
using System.Collections.Generic;

namespace AdventureChallenge.Models
{
    public partial class ChallengeHint
    {
        public int Id { get; set; }
        public int ChallengeId { get; set; }
        public int HintId { get; set; }

        public virtual Challenge Challenge { get; set; } = null!;
        public virtual Hint Hint { get; set; } = null!;
    }
}
