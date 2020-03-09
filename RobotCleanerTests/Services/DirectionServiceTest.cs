using System.Collections.Generic;
using NUnit.Framework;
using RobotCleaner.Services;

namespace RobotCleanerTests.Services
{
    [TestFixture]
    public class DirectionServiceTest
    {
        [Test]
        [TestCase('E', 1)]
        [TestCase('W', -1)]
        [TestCase('S', 0)]
        [TestCase('N', 0)]
        public void GetDirection_ReturnExpectedColumn(char direction, int expected)
        {
            var directionService = new DirectionService();
            var result = directionService.GetDirection(direction);

            Assert.AreEqual(expected, result.Column);
        }

        [Test]
        [TestCase('E', 0)]
        [TestCase('W', 0)]
        [TestCase('S', -1)]
        [TestCase('N', 1)]
        public void GetDirection_ReturnExpectedRow(char direction, int expected)
        {
            var directionService = new DirectionService();
            var result = directionService.GetDirection(direction);

            Assert.AreEqual(expected, result.Row);
        }

        [Test]
        public void GetDirection_ShouldThrowExceptionWhenDirectionIsInvalid()
        {
            var directionService = new DirectionService();
            Assert.Throws<KeyNotFoundException>(() => directionService.GetDirection('A'));
        }
    }
}
