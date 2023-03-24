using System.Security.Cryptography;

namespace HideAndSeek
{
    public static class House
    {
        public static Location Entry { get; private set; } = new Location("Entry");

        public static List<Location> LocationsList { get; set; } = new List<Location>();

        public static Random Random = new Random();

        static House()
        {
            Entry.AddExit(Direction.Out, new LocationWithHidingPlace("Garage", "under the car"));
            Entry.AddExit(Direction.East, new Location("Hallway"));
            Location hallway = Entry.GetExit(Direction.East);
            hallway.AddExit(Direction.South, new LocationWithHidingPlace("Living Room", "behind sofa"));
            hallway.AddExit(Direction.North, new LocationWithHidingPlace("Bathroom", "in the shower"));
            hallway.AddExit(Direction.Northwest, new LocationWithHidingPlace("Kitchen", "behind stove"));
            hallway.AddExit(Direction.Up, new Location("Landing"));
            Location landing = hallway.GetExit(Direction.Up);
            landing.AddExit(Direction.Up, new LocationWithHidingPlace("Attic", "in the closet"));
            landing.AddExit(Direction.South, new LocationWithHidingPlace("Pantry", "in the closet"));
            landing.AddExit(Direction.Southeast, new LocationWithHidingPlace("Kids Room", "under the desk"));
            landing.AddExit(Direction.Southwest, new LocationWithHidingPlace("Nursery", "behind curtain"));
            landing.AddExit(Direction.West, new LocationWithHidingPlace("Second Bathroom", "in the shower"));
            landing.AddExit(Direction.Northwest, new LocationWithHidingPlace("Master Bedroom", "inside the bed"));
            Location masterBedroom= landing.GetExit(Direction.Northwest);
            masterBedroom.AddExit(Direction.East, new LocationWithHidingPlace("Master Bath", "in the shower"));

            

            LocationsList.Add(Entry.GetExit(Direction.Out));
            LocationsList.Add(Entry.GetExit(Direction.East));

            LocationsList.Add(hallway.GetExit(Direction.West));
            LocationsList.Add(hallway.GetExit(Direction.South));
            LocationsList.Add(hallway.GetExit(Direction.North));
            LocationsList.Add(hallway.GetExit(Direction.Northwest));
            LocationsList.Add(hallway.GetExit(Direction.Up));

           
            LocationsList.Add(landing.GetExit(Direction.Up));
            LocationsList.Add(landing.GetExit(Direction.South));
            LocationsList.Add(landing.GetExit(Direction.Southeast));
            LocationsList.Add(landing.GetExit(Direction.Southwest));
            LocationsList.Add(landing.GetExit(Direction.West));
            LocationsList.Add(landing.GetExit(Direction.Northwest));

            LocationsList.Add(masterBedroom.GetExit(Direction.East));

        }


        public static Location GetLocationByName(string name) => LocationsList.FirstOrDefault(x => x.Name == name, LocationsList[2]);

        public static Location RandomExit(Location location)
        {
            var exitsList = location.ExitList.ToList();
            if (location.ExitList.Count() == 1) return GetLocationByName(location.ExitList.First());

            return GetLocationByName(exitsList[Random.Next(0, (exitsList.Count() + 1))]);
        }

        public static void ClearHidingPlaces()
        {
            foreach (var item in LocationsList)
            {
                if (item is LocationWithHidingPlace itemHidingPlace)
                {
                    itemHidingPlace.HidingOpponents.Clear();
                }
            }
        }
    }
}