using System;
using RobotCleaner;
using RobotCleaner.Services.Interfaces;
using RobotCleanerConsole.Providers.Interfaces;

namespace RobotCleanerConsole.Services
{
    public class RobotService
    {
        private readonly IRobot robot;
        private readonly IInputProvider inputProvider;
        private readonly IReportService reportService;

        public RobotService(IRobot robot, IInputProvider inputProvider, IReportService reportService)
        {
            this.robot = robot ?? throw new ArgumentNullException(nameof(robot));
            this.inputProvider = inputProvider ?? throw new ArgumentNullException(nameof(inputProvider));
            this.reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        public void CleanPlaces()
        {
            var commandsNumber = this.inputProvider.GetCommandsNumber();
            var primaryLocation = this.inputProvider.GetPrimaryLocation();
            var instructions = this.inputProvider.GetInstructions(commandsNumber);

            this.robot.Clean(primaryLocation, instructions);
        }

        public string CreateReport()
        {
            return this.reportService.CreateReport(this.robot);
        }
    }
}
