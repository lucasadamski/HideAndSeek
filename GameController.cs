namespace HideAndSeek
{
    public class GameController
    {
        /// <summary>
        /// The player's current location in the house
        /// </summary>
        public Location CurrentLocation { get; private set; }
        /// <summary>
        /// Returns the the current status to show to the player
        /// </summary>
        public string Status => $"You are in the {CurrentLocation.Name}. You see the following exits:{Environment.NewLine}{CurrentLocation.TextExitList()}";
        /// <summary>
        /// A prompt to display to the player
        /// </summary>
        public string Prompt => "Which direction do you want to go: ";
        public GameController()
        {
            CurrentLocation = House.Entry;
            
        }
        /// <summary>
        /// Move to the location in a direction
        /// </summary>
        /// <param name="direction">The direction to move</param>
        /// <returns>True if the player can move in that direction, false oterwise</returns>
        public bool Move(Direction direction)
        {
            if (CurrentLocation.GetExit(direction) == CurrentLocation) return false;

            CurrentLocation = CurrentLocation.GetExit(direction);
            return true;
        }
        /// <summary>
        /// Parses input from the player and updates the status
        /// </summary>
        /// <param name="input">Input to parse</param>
        /// <returns>The results of parsing the input</returns>
        public string ParseInput(string input)
        {
            /*string[] validInputs = new string[] { "north", "northeast", "east", "southeast", "south", "southwest", "west", "northwest", "up", "down", "in", "out" };
            bool isFound = false;
            foreach (var item in validInputs)
            {
                if (input.ToLower().Trim() == item) { isFound = true; break; }
            }
            if (!isFound) return "That's not a valid direction";*/
            Direction directions;
            if (Enum.TryParse(input.ToLower().Trim(), true, out directions))
            {
                if (Move(directions))
                {
                    return "Moving " + directions.ToString();
                }
                else
                {
                    return "There's no exit in that direction";
                }


            }
            else
            {
                return "That's not a valid direction";
            }




        }
    }
}