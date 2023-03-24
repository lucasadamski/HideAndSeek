namespace HideAndSeek
{

    public class Opponent
    {
        public readonly string Name;
        public Opponent(string name) => Name = name;
        public override string ToString() => Name;
        private Location currentLocation { get; set; } = House.Entry;
        public void Hide()
        {
            for (int i = 0; i < House.Random.Next(10, 51); i++)                 
            {
                currentLocation = House.RandomExit(currentLocation);
            }
            while(true)
            {
                if (currentLocation is LocationWithHidingPlace locationWithHidingPlace)
                {
                    locationWithHidingPlace.Hide(this);
                    break;
                }
                currentLocation = House.RandomExit(currentLocation);
            } 

            System.Diagnostics.Debug.WriteLine($"{Name} is hiding {(currentLocation as LocationWithHidingPlace).HidingPlace} in {currentLocation.Name}");
            
        }
    }
}