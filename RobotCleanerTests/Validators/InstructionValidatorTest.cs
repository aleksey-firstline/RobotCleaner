using System;
using System.Collections.Generic;
using NUnit.Framework;
using RobotCleaner.Models;
using RobotCleaner.Validators;

namespace RobotCleanerTests.Validators
{
    [TestFixture]
    public class InstructionValidatorTest
    {
        public static IEnumerable<object[]> InvalidStepsCases = new[]
        {
            new object[] { new List<Instruction> { new Instruction('E', 0) } },
            new object[] { new List<Instruction> { new Instruction('S', 100000)} }
        };

        public static IEnumerable<object[]> InvalidDirectionCases = new[]
        {
            new object[] { new List<Instruction> { new Instruction('A', 1) } },
            new object[] { new List<Instruction> { new Instruction('C', 1)} }
        };

        [Test]
        public void ValidateInstruction_ShouldNotThrowException()
        {
            var instructions = new List<Instruction>
            {
                new Instruction('E', 1),
                new Instruction('S', 99999)
            };

            var instructionValidator = new InstructionValidator();

            Assert.DoesNotThrow(() => instructionValidator.ThrowIfInvalid(instructions));
        }

        [Test]
        public void ValidateInstruction_ShouldThrowExceptionWhenInstructionsIsNull()
        {
            var instructionValidator = new InstructionValidator();
            Assert.Throws<ArgumentNullException>(() => instructionValidator.ThrowIfInvalid(null));
        }

        [Test]
        [TestCaseSource(nameof(InvalidStepsCases))]
        public void ValidateInstruction_ShouldThrowExceptionWhenStepsIsInvalid(ICollection<Instruction> instructions)
        {
            var instructionValidator = new InstructionValidator();
            Assert.Throws<ArgumentOutOfRangeException>(() => instructionValidator.ThrowIfInvalid(instructions));
        }

        [Test]
        [TestCaseSource(nameof(InvalidDirectionCases))]
        public void ValidateInstruction_ShouldThrowExceptionWhenDirectionIsInvalid(ICollection<Instruction> instructions)
        {
            var instructionValidator = new InstructionValidator();
            Assert.Throws<ArgumentException>(() => instructionValidator.ThrowIfInvalid(instructions));
        }
    }
}
