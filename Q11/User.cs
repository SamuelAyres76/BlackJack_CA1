namespace BlackJack
{
    public class User
    {
        public string Username { get; set; }

        // Achievements
        public bool WinGame { get; set; }
        public bool LoseGame { get; set; }
        public bool Lose3InRow { get; set; }
        public bool Win3InRow { get; set; }
        public bool ScoreBlackJack { get; set; }
        public int WinStreak { get; set; }
        public int LossStreak { get; set; }

        // Achievement Unlocked Flags
        public bool WinGameUnlocked { get; set; }
        public bool LoseGameUnlocked { get; set; }
        public bool Lose3InRowUnlocked { get; set; }
        public bool Win3InRowUnlocked { get; set; }
        public bool BlackJackUnlocked { get; set; }
    }
}


