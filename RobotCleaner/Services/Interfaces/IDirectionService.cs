using RobotCleaner.Models;

namespace RobotCleaner.Services.Interfaces
{
    public interface IDirectionService
    {
        Location GetDirection(char direction);
    }
}