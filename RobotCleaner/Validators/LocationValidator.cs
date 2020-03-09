using System;
using RobotCleaner.Models;
using RobotCleaner.Validators.Interfaces;

namespace RobotCleaner.Validators
{
    public class LocationValidator : IValidator<Location>
    {
        private const int MaxPointValue = 100000;
        private const int MinPointValue = -100000;

        public void ThrowIfInvalid(Location location)
        {
            if (location == null)
                throw new ArgumentNullException(nameof(location));

            if (location.Column < MinPointValue || location.Column > MaxPointValue)
                throw new ArgumentOutOfRangeException(nameof(location.Column));

            if (location.Row < MinPointValue || location.Row > MaxPointValue)
                throw new ArgumentOutOfRangeException(nameof(location.Row));
        }
    }
}