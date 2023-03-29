namespace HideAndSeek
{
    public class LocationWithHidingPlace : Location
    {
        public string HidingPlace { get; set; }
        public List<Opponent> HidingOpponents { get; private set; } = new List<Opponent>();
        public LocationWithHidingPlace(string locationName, string hidingPlaceName) : base(locationName)
        {
            HidingPlace = hidingPlaceName;
        }

        public void Hide(Opponent opponent)
        {
            HidingOpponents.Add(opponent);
            opponent.currentLocation = this;
        }

        public IEnumerable<Opponent> CheckHidingPlace()
        {
            List<Opponent> tempList = new List<Opponent>();
            tempList.AddRange(HidingOpponents);
            HidingOpponents.Clear();
            return tempList;
        }

    }
}