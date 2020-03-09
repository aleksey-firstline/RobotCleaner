using System.Collections.Generic;
using RobotCleaner.Models;

namespace RobotCleanerConsole.Providers.Interfaces
{
    public interface IInputProvider
    {
        int GetCommandsNumber();
        Location GetPrimaryLocation();
        ICollection<Instruction> GetInstructions(int commandsNumber);
    }
}