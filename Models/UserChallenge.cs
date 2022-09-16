using System;
using System.Collections.Generic;

namespace AdventureChallenge.Models
{
    public partial class UserChallenge
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public string? Beschrijving { get; set; }
        public int? FotoId { get; set; }
        public bool Afgerond { get; set; }

        public virtual Challenge Challenge { get; set; } = null!;
        public virtual Foto? Foto { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
