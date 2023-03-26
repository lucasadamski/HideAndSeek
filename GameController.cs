using System.ComponentModel;
using System.Numerics;

namespace HideAndSeek
{
    public class GameController
    {
        /// <summary>
        /// The number of moves the player has made
        /// </summary>
        public int MoveNumber { get; private set; } = 1;
        /// <summary>
        /// Private list of opponents the player needs to find
        /// </summary>
        public readonly IEnumerable<Opponent> Opponents = new List<Opponent>()
        {
            new Opponent("Joe"),
            new Opponent("Bob"),
            new Opponent("Ana"),
            new Opponent("Owen"),
            new Opponent("Jimmy"),
        };
        /// <summary>
        /// Private list of opponents the player has found so far
        /// </summary>
        private readonly List<Opponent> foundOpponents = new List<Opponent>();
        /// <summary>
        /// Returns true if the game is over
        /// </summary>
        public bool GameOver => Opponents.Count() == foundOpponents.Count();
        /// <summary>
        /// A prompt to display to the player
        /// </summary>
        /// <summary>
        /// The player's current location in the house
        /// </summary>
        public Location CurrentLocation { get; private set; }
        /// <summary>
        /// Returns the the current status to show to the player
        /// </summary>
        public string Status => generateStatus();
        /// <summary>
        /// A prompt to display to the player
        /// </summary>
        public string Prompt => $"{MoveNumber}: Which direction do you want to go (or type 'check'): ";
        public GameController()
        {
            House.ClearHidingPlaces();
            foreach (var opponent in Opponents)
                opponent.Hide();
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
            if (input.ToLower().Trim() == "check")
            {
                MoveNumber++;
                if (CurrentLocation is LocationWithHidingPlace locationWithHidingPlace)
                {
                    var hidingOpponentsCount = locationWithHidingPlace.HidingOpponents.Count();
                    if (hidingOpponentsCount == 0)
                    {
                        return "Nobody was hiding " + locationWithHidingPlace.HidingPlace;
                    }
                    else
                    {
                        foundOpponents.AddRange(locationWithHidingPlace.HidingOpponents);
                        locationWithHidingPlace.CheckHidingPlace();
                        return $"You found {hidingOpponentsCount} {opponentPlural(hidingOpponentsCount)} hiding {locationWithHidingPlace.HidingPlace}";
                    }
                }
                else
                {
                    return "There is no hiding place in the " + CurrentLocation.Name;
                }
            }

            Direction directions;
            if (Enum.TryParse(input.ToLower().Trim(), true, out directions))
            {
                if (Move(directions))
                {
                    MoveNumber++;
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

        private string opponentPlural(int number) => number > 1 ? "opponents" : "opponent";

        private string statusOpponentsFound()
        {
            if (foundOpponents.Count() == 0)
            {
                return "You have not found any opponents";
            }
            else
            {
                return $"You have found {foundOpponents.Count()} of {Opponents.Count()} opponents: {string.Join(' ', (Opponents.Select(x=>x.Name)))}";
            }
        }

        private string generateStatus()
        {
            string stringFoundOpponents;
            if(foundOpponents.Count() == 0) 
                stringFoundOpponents = "You have not found any opponents";
            else                            
                stringFoundOpponents = $"You have found {foundOpponents.Count()} of {Opponents.Count()} opponents: {string.Join(", ", (foundOpponents.Select(x => x.Name)))}";


            if (CurrentLocation is LocationWithHidingPlace locationWithHidingPlace)
            {
                return $"You are in the {CurrentLocation.Name}. You see the following exits:{Environment.NewLine}{CurrentLocation.TextExitList()}{Environment.NewLine}Someone could hide {locationWithHidingPlace.HidingPlace}{Environment.NewLine}{stringFoundOpponents}";
            }
            else
            {
                return $"You are in the {CurrentLocation.Name}. You see the following exits:{Environment.NewLine}{CurrentLocation.TextExitList()}{Environment.NewLine}{stringFoundOpponents}";
            }
        }

    }
}