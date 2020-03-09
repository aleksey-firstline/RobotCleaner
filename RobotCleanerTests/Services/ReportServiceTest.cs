using System;
using NUnit.Framework;
using RobotCleaner.Services;
using Moq;
using RobotCleaner;

namespace RobotCleanerTests.Services
{
    [TestFixture]
    public class ReportServiceTest
    {
        private Mock<IRobot> mockRobot;

        [SetUp]
        public void SetUp()
        {
            this.mockRobot = new Mock<IRobot>();
        }

        [Test]
        [TestCase(0, "=> Cleaned: 0")]
        [TestCase(10, "=> Cleaned: 10")]
        public void CreateReport_ShouldReturnExpectedResult(int cleanedPlaces, string expected)
        {
            mockRobot.Setup(x => x.CleanedPlaces)
                .Returns(cleanedPlaces);

            var report = new ReportService();
            var result = report.CreateReport(mockRobot.Object);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CreateReport_ShouldThrowExceptionWhenArgumentIsNull()
        {
            var report = new ReportService();
            Assert.Throws<ArgumentNullException>(() => report.CreateReport(null));
        }
    }
}