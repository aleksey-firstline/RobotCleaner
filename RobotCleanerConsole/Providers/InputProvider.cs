using System;
using System.Collections.Generic;
using RobotCleaner.Models;
using RobotCleanerConsole.Providers.Interfaces;

namespace RobotCleanerConsole.Providers
{
    public class InputProvider : IInputProvider
    {
        private const int MaxCommandValue = 10000;
        private const int MinCommandValue = 0;

        private readonly Func<string> readLine;

        public InputProvider(Func<string> readLine)
        {
            this.readLine = readLine ?? throw new ArgumentNullException(nameof(readLine));
        }

        public int GetCommandsNumber()
        {
            var commandsNumber = int.Parse(this.readLine());

            if (commandsNumber < MinCommandValue || commandsNumber > MaxCommandValue)
                throw new ArgumentOutOfRangeException(nameof(commandsNumber));

            return commandsNumber;
        }

        public Location GetPrimaryLocation()
        {
            var points = this.readLine().Split(' ');

            var column = int.Parse(points[0]);
            var row = int.Parse(points[1]);

            return new Location(column, row);
        }

        public ICollection<Instruction> GetInstructions(int commandsNumber)
        {
            var instructions = new List<Instruction>();
            for (var i = 0; i < commandsNumber; i++)
            {
                var instruction = GetInstruction();
                instructions.Add(instruction);
            }

            return instructions;
        }

        private Instruction GetInstruction()
        {
            var operations = this.readLine();
            var direction = operations[0];
            var steps = int.Parse(operations.Substring(2));

            return new Instruction(direction, steps);
        }
    }
}
