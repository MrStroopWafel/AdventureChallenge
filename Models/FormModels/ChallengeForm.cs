namespace AdventureChallenge.Models.FormModels
{
    public class ChallengeForm : UserChallenge
    {
        public ChallengeForm()
        {
            ChallengeHints = new HashSet<ChallengeHint>();
            UserChallenges = new HashSet<UserChallenge>();
        }

        public decimal Prijs { get; set; }
        public string Tijdstip { get; set; }
        public string Personen { get; set; }
        public string Status { get; set; }
        public decimal Tijdduur { get; set; }
        public string Opdracht { get; set; }

        public List<Hint>? Hints { get; set; }

        public virtual ICollection<ChallengeHint> ChallengeHints { get; set; }
        public virtual ICollection<UserChallenge> UserChallenges { get; set; }
    }
}
