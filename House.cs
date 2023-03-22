namespace HideAndSeek
{
    public static class House
    {
        public static Location Entry { get; private set; } = new Location("Entry");

        static House()
        {
            Entry.AddExit(Direction.Out, new Location("Garage"));
            Entry.AddExit(Direction.East, new Location("Hallway"));
            Location hallway = Entry.GetExit(Direction.East);
            hallway.AddExit(Direction.South, new Location("Living Room"));
            hallway.AddExit(Direction.North, new Location("Bathroom"));
            hallway.AddExit(Direction.Northwest, new Location("Kitchen"));
            hallway.AddExit(Direction.Up, new Location("Landing"));
            Location landing = hallway.GetExit(Direction.Up);
            landing.AddExit(Direction.Up, new Location("Attic"));
            landing.AddExit(Direction.South, new Location("Pantry"));
            landing.AddExit(Direction.Southeast, new Location("Kids Room"));
            landing.AddExit(Direction.Southwest, new Location("Nursery"));
            landing.AddExit(Direction.West, new Location("Second Bathroom"));
            landing.AddExit(Direction.Northwest, new Location("Master Bedroom"));
            Location masterBedroom= landing.GetExit(Direction.Northwest);
            masterBedroom.AddExit(Direction.East, new Location("Master Bath"));
        }
    }
}