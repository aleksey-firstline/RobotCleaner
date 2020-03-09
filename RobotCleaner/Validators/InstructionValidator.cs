using System;
using System.Collections.Generic;
using RobotCleaner.Constants;
using RobotCleaner.Models;
using RobotCleaner.Validators.Interfaces;

namespace RobotCleaner.Validators
{
    public class InstructionValidator : IValidator<ICollection<Instruction>>
    {
        private const int MinStepsValue = 1;
        private const int MaxStepsValue = 99999;

        private readonly string InvalidDirectionExceptionMessage = 
            "Invalid value for the direction argument {0}";

        private static readonly HashSet<char> Directions = new HashSet<char>
        {
            DirectionConstants.East,
            DirectionConstants.West,
            DirectionConstants.South,
            DirectionConstants.North
        };

        public void ThrowIfInvalid(ICollection<Instruction> instructions)
        {
            if (instructions == null)
                throw new ArgumentNullException(nameof(instructions));

            foreach (var instruction in instructions)
            {
                if (instruction.Steps < MinStepsValue || instruction.Steps > MaxStepsValue)
                    throw new ArgumentOutOfRangeException(nameof(instruction.Steps));

                if (!Directions.Contains(instruction.Direction))
                    throw new ArgumentException(string.Format(InvalidDirectionExceptionMessage, instruction.Direction));
            }
        }
    }
}