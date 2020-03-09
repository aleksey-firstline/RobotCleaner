using System;
using System.Collections.Generic;
using RobotCleaner.Models;
using RobotCleaner.Services.Interfaces;
using RobotCleaner.Validators.Interfaces;

namespace RobotCleaner
{
    public class Robot : IRobot
    {
        private readonly IDirectionService directionService;
        private readonly IValidator<Location> locationValidator;
        private readonly IValidator<ICollection<Instruction>> instructionValidator;

        public Robot(
            IDirectionService directionService,
            IValidator<Location> locationValidator,
            IValidator<ICollection<Instruction>> instructionValidator)
        {
            this.directionService = directionService ?? throw new ArgumentNullException(nameof(directionService));
            this.locationValidator = locationValidator ?? throw new ArgumentNullException(nameof(locationValidator));
            this.instructionValidator = instructionValidator ?? throw new ArgumentNullException(nameof(instructionValidator));
        }

        public int CleanedPlaces { get; private set; }

        public void Clean(Location primaryLocation, ICollection<Instruction> instructions)
        {
            this.instructionValidator.ThrowIfInvalid(instructions);
            this.locationValidator.ThrowIfInvalid(primaryLocation);

            this.CleanedPlaces++;
            var cleanedPlaces = new HashSet<Location> {primaryLocation};

            var column = primaryLocation.Column;
            var row = primaryLocation.Row;

            foreach (var instruction in instructions)
            {
                var direction = this.directionService.GetDirection(instruction.Direction);
                for (var i = 0; i < instruction.Steps; i++)
                {
                    column += direction.Column;
                    row += direction.Row;

                    var currentLocation = new Location(column, row);
                    this.locationValidator.ThrowIfInvalid(currentLocation);

                    if (!cleanedPlaces.Contains(currentLocation))
                    {
                        cleanedPlaces.Add(currentLocation);
                        this.CleanedPlaces++;
                    }
                }
            }
        }
    }
}
