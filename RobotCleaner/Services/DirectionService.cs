using System.Collections.Generic;
using RobotCleaner.Constants;
using RobotCleaner.Models;
using RobotCleaner.Services.Interfaces;

namespace RobotCleaner.Services
{
    public class DirectionService : IDirectionService
    {
        private static readonly Dictionary<char, Location> Directions = new Dictionary<char, Location>
        {
            [DirectionConstants.East] = new Location(1, 0),
            [DirectionConstants.West] = new Location(-1, 0),
            [DirectionConstants.South] = new Location(0, -1),
            [DirectionConstants.North] = new Location(0, 1)
        };

        public Location GetDirection(char direction)
        {
            return Directions[direction];
        }
    }
}
