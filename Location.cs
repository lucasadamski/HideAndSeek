using System.Text;
using System.Text.Json.Serialization;

namespace HideAndSeek
{
    public class Location
    {
        /// <summary>
        /// The name of this location
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The exits out of this location
        /// </summary>
        public IDictionary<Direction, Location> Exits { get; private set; }  = new Dictionary<Direction, Location>();

        /// <summary>
        /// Returns a sequence of descriptions of the exits, sorted by ?direction?
        /// </summary>
        public IEnumerable<string> ExitList => SortExitList();

        /// <summary>
        /// The constructor sets the location name
        /// </summary>
        /// <param name="name">Name of the location</param>
        public Location(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Sorts exits by direction
        /// </summary>
        /// <param name="isShort">Only name of exit, or a full descripition with direction</param>
        /// <returns></returns>
        private IEnumerable<string> SortExitList(bool isShort = true)
        {

            Direction[] order = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West,
                Direction.Northeast, Direction.Southeast, Direction.Southwest, Direction.Northwest, Direction.Up,
                Direction.Down, Direction.Out, Direction.In};
            List<string> orderedList = new List<string>();
            for (int i = 0; i < order.Length; i++)
            {
                if (isShort)
                {
                    foreach (var item in Exits)
                    { 
                        if (item.Key == order[i])
                        {
                            orderedList.Add(item.Value.Name);
                        }
                    }
                }
                else
                {
                    foreach (var item in Exits)
                    {
                        if (item.Key == order[i])
                        {
                            orderedList.Add(" - the " + item.Value + " is " + DescribeDirection(item.Key));
                        }
                    }
                }
            }
            return orderedList;
        }

        public string TextExitList()
        {
            var fullDescriptionList = SortExitList(false);
            return string.Concat(string.Join(Environment.NewLine, fullDescriptionList));
        }

       
        /// <summary>
        /// Adds an exit to this location
        /// </summary>
        /// <param name="direction">Direction of the connecting location</param>
        /// <param name="connectingLocation">Connecting location to add</param>
        public void AddExit(Direction direction, Location connectingLocation)
        {
            Exits.Add(direction, connectingLocation);
            connectingLocation.AddReturnExit(direction, this);
        }

        /// <summary>
        /// Gets the exit location in a direction
        /// </summary>
        /// <param name="direction">Direciton of the exit location</param>
        /// <returns>The exit location, or this if there is no exit in that direction</returns>
        public Location GetExit(Direction direction)
        {
            Location value;
            if (Exits.TryGetValue(direction, out value))
            {
                return value;
            }
            else
            {
                return this;
            }
        }
        /// <summary>
        /// Adds a return exit to a connecting location
        /// </summary>
        /// <param name="direction">Direction of the connecting location</param>
        /// <param name="connectingLocation">Location to add the return exit to</param>
        private void AddReturnExit(Direction direction, Location connectingLocation) =>
        Exits.Add((Direction)(-(int)direction), connectingLocation);


        /// <summary>
        /// Describes a direction (e.g. "in" vs. "to the North")
        /// </summary>
        /// <param name="d">Direction to describe</param>
        /// <returns>string describing the direction</returns>
        private string DescribeDirection(Direction d) => d switch
        {
            Direction.Up => "Up",
            Direction.Down => "Down",
            Direction.In => "In",
            Direction.Out => "Out",
            _ => $"to the {d}",
        };


        public override string ToString() => Name;

    }
}