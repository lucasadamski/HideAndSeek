namespace HideAndSeek
{
    public class SavedGame
    {
        public int MoveNumber { get; set; }
        public string CurrentLocation { get; set; }
        public List<string> FoundOpponents { get; set; }
        public List<string> Opponents { get; set; }
        public Dictionary<string, string> OpponentLocations { get; set; }
    }
}