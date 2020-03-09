using System;
using System.Linq;
using NUnit.Framework;
using RobotCleanerConsole.Providers;

namespace RobotCleanerConsoleTests.Providers
{
    [TestFixture]
    public class InputProviderTest
    {
        [Test]
        [TestCase("1", 1)]
        [TestCase("20", 20)]
        public void GetCommandsNumber_ReturnExpectedResult(string value, int expected)
        {
            var settingsProvider = new InputProvider(() => value);
            var result = settingsProvider.GetCommandsNumber();
            
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("-1")]
        [TestCase("10001")]
        public void GetCommandsNumber_ShouldThrowExceptionWhenCommandsNumberIsInvalid(string value)
        {
            var inputProvider = new InputProvider(() => value);
            Assert.Throws<ArgumentOutOfRangeException>(() => inputProvider.GetCommandsNumber());
        }

        [Test]
        [TestCase("1 0", 1)]
        [TestCase("3 0", 3)]
        public void GetPrimaryLocation_ReturnExpectedColumnValue(string value, int expected)
        {
            var settingsProvider = new InputProvider(() => value);
            var result = settingsProvider.GetPrimaryLocation();

            Assert.NotNull(result);
            Assert.AreEqual(expected, result.Column);
        }

        [Test]
        [TestCase("0 1", 1)]
        [TestCase("0 3", 3)]
        public void GetPrimaryLocation_ReturnExpectedRowValue(string value, int expected)
        {
            var settingsProvider = new InputProvider(() => value);
            var result = settingsProvider.GetPrimaryLocation();

            Assert.NotNull(result);
            Assert.AreEqual(expected, result.Row);
        }

        [Test]
        [TestCase("E 0", 'E')]
        [TestCase("N 0", 'N')]
        public void GetInstruction_ReturnExpectedDirectionValue(string value, char expected)
        {
            var settingsProvider = new InputProvider(()=> value);
            var result = settingsProvider.GetInstructions(1).FirstOrDefault();

            Assert.NotNull(result);
            Assert.AreEqual(expected, result.Direction);
        }

        [Test]
        [TestCase("E 1", 1)]
        [TestCase("N 5", 5)]
        public void GetInstruction_ReturnExpectedStepsValue(string value, int expected)
        {
            var settingsProvider = new InputProvider(() => value);
            var result = settingsProvider.GetInstructions(1).FirstOrDefault();

            Assert.NotNull(result);
            Assert.AreEqual(expected, result.Steps);
        }

        [Test]
        [TestCase(1, "E 1")]
        [TestCase(3, "N 1")]
        public void GetInstruction_ShouldReturnExpectedAmount(int commandsNumber, string value)
        {
            var settingsProvider = new InputProvider(() => value);
            var result = settingsProvider.GetInstructions(commandsNumber);

            Assert.NotNull(result);
            Assert.AreEqual(commandsNumber, result.Count);
        }

        [Test]
        public void InputProviderConstructor_ShouldThrowWhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new InputProvider(null));
        }
    }
}