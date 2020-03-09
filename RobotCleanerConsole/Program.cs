using System;
using RobotCleaner;
using RobotCleaner.Services;
using RobotCleaner.Validators;
using RobotCleanerConsole.Providers;
using RobotCleanerConsole.Services;

namespace RobotCleanerConsole
{
    class Program
    {
        static void Main()
        {
            
            var directionService = new DirectionService();
            var locationValidator = new LocationValidator();
            var instructionValidator = new InstructionValidator();
            var robot = new Robot(directionService, locationValidator, instructionValidator);

            var reportService = new ReportService();
            var inputProvider = new InputProvider(Console.ReadLine);
            var robotService = new RobotService(robot, inputProvider, reportService);

            robotService.CleanPlaces();
            var report = robotService.CreateReport();

            Console.WriteLine(report);
        }
    }
}
