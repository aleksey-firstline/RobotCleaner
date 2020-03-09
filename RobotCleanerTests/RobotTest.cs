using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RobotCleaner;
using RobotCleaner.Models;
using RobotCleaner.Services.Interfaces;
using RobotCleaner.Validators.Interfaces;

namespace RobotCleanerTests
{
    [TestFixture]
    public class RobotTest
    {
        private Mock<IDirectionService> mockDirectionService;
        private Mock<IValidator<Location>> mockLocationValidator;
        private Mock<IValidator<ICollection<Instruction>>> mockInstructionsValidator;

        private Dictionary<char, Location> directions;

        [SetUp]
        public void SetUp()
        {
            this.mockDirectionService = new Mock<IDirectionService>();
            this.mockLocationValidator = new Mock<IValidator<Location>>();
            this.mockInstructionsValidator = new Mock<IValidator<ICollection<Instruction>>>();

            this.directions = new Dictionary<char, Location>
            {
                ['E'] = new Location(1, 0),
                ['W'] = new Location(-1, 0),
                ['S'] = new Location(0, -1),
                ['N'] = new Location(0, 1)
            };
        }

        public static IEnumerable<object[]> ValidCases = new[]
        {
            new object[]
            {
                new Location(0, 0),
                new List<Instruction>
                {
                    new Instruction ('E', 1)
                },
                2
            },
            new object[]
            {
                new Location(0, 0),
                new List<Instruction>
                {
                    new Instruction('N', 1)
                },
                2
            },
            new object[]
            {
                new Location(0, 0),
                new List<Instruction>
                {
                    new Instruction('N', 2)
                },
                3
            },
            new object[]
            {
                new Location(0, 0),
                new List<Instruction>
                {
                    new Instruction('N', 2),
                    new Instruction('W', 1)
                },
                4
            },
            new object[]
            {
                new Location(0, 0),
                new List<Instruction>
                {
                    new Instruction('N', 2),
                    new Instruction('W', 1),
                    new Instruction('S', 3),
                    new Instruction('E', 4),
                },
                11
            },
            new object[]
            {
                new Location(10, 22),
                new List<Instruction>
                {
                    new Instruction('E', 2),
                    new Instruction('N', 1)
                },
                4
            },
        };

        [Test]
        [TestCaseSource(nameof(ValidCases))]
        public void Clean_ShouldReturnExpectedResult(
            Location primaryLocation, 
            ICollection<Instruction> instructions, 
            int expected)
        {
            this.mockDirectionService.Setup(x => x.GetDirection(It.IsAny<char>()))
                .Returns((char x) => directions[x]);

            var robot = new Robot(
                mockDirectionService.Object, 
                mockLocationValidator.Object, 
                mockInstructionsValidator.Object);

            robot.Clean(primaryLocation, instructions);

            Assert.AreEqual(expected, robot.CleanedPlaces);
        }

        [Test]
        public void Clean_InstructionsValidatorShouldBeCalledOnce()
        {
            var robot = new Robot(
                mockDirectionService.Object,
                mockLocationValidator.Object,
                mockInstructionsValidator.Object);

            var primaryLocation = new Location(0, 0);
            var instructions = new List<Instruction>();
            robot.Clean(primaryLocation, instructions);

            mockInstructionsValidator.Verify(x => x.ThrowIfInvalid(It.IsAny<ICollection<Instruction>>()), Times.Once);
        }

        [Test]
        public void Clean_LocationValidatorShouldBeCalledOnce()
        {
            var robot = new Robot(
                mockDirectionService.Object,
                mockLocationValidator.Object,
                mockInstructionsValidator.Object);

            var primaryLocation = new Location(0, 0);
            var instructions = new List<Instruction>();

            robot.Clean(primaryLocation, instructions);

            mockLocationValidator.Verify(x => x.ThrowIfInvalid(It.IsAny<Location>()), Times.Once);
        }

        [Test]
        public void Clean_LocationValidatorShouldBeCalledThreeTimes()
        {
            this.mockDirectionService.Setup(x => x.GetDirection(It.IsAny<char>()))
                .Returns((char x) => directions[x]);

            var robot = new Robot(
                mockDirectionService.Object,
                mockLocationValidator.Object,
                mockInstructionsValidator.Object);

            var primaryLocation = new Location(0, 0);
            var instructions = new List<Instruction>
            {
                new Instruction('E', 2)
            };
            robot.Clean(primaryLocation, instructions);

            mockLocationValidator.Verify(x => x.ThrowIfInvalid(It.IsAny<Location>()), Times.Exactly(3));
        }

        [Test]
        public void RobotConstructor_ShouldThrowWhenDirectionServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Robot(
                    null,
                    mockLocationValidator.Object,
                    mockInstructionsValidator.Object);
            });
        }

        [Test]
        public void RobotConstructor_ShouldThrowWhenLocationValidatorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Robot(
                    mockDirectionService.Object,
                    null,
                    mockInstructionsValidator.Object);
            });
        }

        [Test]
        public void RobotConstructor_ShouldThrowWhenInstructionValidatorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Robot(
                    mockDirectionService.Object,
                    mockLocationValidator.Object,
                    null);
            });
        }
    }
}