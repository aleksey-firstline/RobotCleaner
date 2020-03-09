using System.Collections.Generic;
using RobotCleaner.Models;

namespace RobotCleaner
{
    public interface IRobot
    {
        void Clean(Location primaryLocation, ICollection<Instruction> instructions);
        int CleanedPlaces { get; }
    }
}