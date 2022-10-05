namespace AdventureChallenge.Models.FormModels
{
    public class ChallengeForm
    {
        public ChallengeForm()
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
        public string Beschrijving { get; set; }
        public System.Drawing.Image? Image { get; set; }
        public List<Hint>? hints { get; set; }

        public virtual ICollection<ChallengeHint> ChallengeHints { get; set; }
        public virtual ICollection<UserChallenge> UserChallenges { get; set; }
    }
}
