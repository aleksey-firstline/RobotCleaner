using System;
using RobotCleaner.Services.Interfaces;

namespace RobotCleaner.Services
{
    public class ReportService : IReportService
    {
        public string CreateReport(IRobot robot)
        {
            if (robot == null)
                throw new ArgumentNullException(nameof(robot));

            return $"=> Cleaned: {robot.CleanedPlaces}";
        }
    }
}