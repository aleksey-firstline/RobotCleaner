using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RobotCleaner;
using RobotCleaner.Models;
using RobotCleaner.Services.Interfaces;
using RobotCleanerConsole.Providers.Interfaces;
using RobotCleanerConsole.Services;

namespace RobotCleanerConsoleTests.Services
{
    [TestFixture]
    public class RobotServiceTest
    {
        private Mock<IRobot> robotMock;
        private Mock<IInputProvider> inputProviderMock;
        private Mock<IReportService> reportServiceMock;

        [SetUp]
        public void SetUp()
        {
            this.robotMock = new Mock<IRobot>();
            this.inputProviderMock = new Mock<IInputProvider>();
            this.reportServiceMock = new Mock<IReportService>();
        }

        [Test]
        public void CleanPlaces_ShouldPassExpectedInstructions()
        {
            var instructions = new List<Instruction>
            {
                new Instruction('E', 2),
                new Instruction('N', 3)
            };

            this.inputProviderMock.Setup(x => x.GetInstructions(It.IsAny<int>()))
                .Returns(instructions);

            var robotService = new RobotService(
                robotMock.Object, 
                inputProviderMock.Object,
                reportServiceMock.Object);

            robotService.CleanPlaces();

            this.robotMock.Verify(x => x.Clean(It.IsAny<Location>(),
                It.Is<ICollection<Instruction>>(c => c.Equals(instructions))));
        }

        [Test]
        public void CleanPlaces_ShouldPassExpectedLocations()
        {
            var location = new Location(10, 22);
            this.inputProviderMock.Setup(x => x.GetPrimaryLocation())
                .Returns(location);

            var robotService = new RobotService(
                robotMock.Object, 
                inputProviderMock.Object,
                reportServiceMock.Object);

            robotService.CleanPlaces();

            this.robotMock.Verify(x => x.Clean(
                It.Is<Location>(l => l.Equals(location)),
                It.IsAny<ICollection<Instruction>>()));
        }

        [Test]
        public void CreateReport_ShouldPassExpectedArgument()
        {
            var robotService = new RobotService(
                robotMock.Object,
                inputProviderMock.Object,
                reportServiceMock.Object);

            robotService.CreateReport();

            reportServiceMock.Verify(x => x.CreateReport(
                It.Is<IRobot>(r => r.Equals(robotMock.Object))));
        }

        [Test]
        public void RobotServiceConstructor_ShouldThrowWhenRobotIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new RobotService(
                    null,
                    inputProviderMock.Object,
                    reportServiceMock.Object);
            });
        }

        [Test]
        public void RobotServiceConstructor_ShouldThrowWhenInputProviderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new RobotService(
                    robotMock.Object,
                    null,
                    reportServiceMock.Object);
            });
        }

        [Test]
        public void RobotServiceConstructor_ShouldThrowWhenReportServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new RobotService(
                    robotMock.Object,
                    inputProviderMock.Object,
                    null);
            });
        }
    }
}
