using System;
using System.Collections.Generic;
using NUnit.Framework;
using RobotCleaner.Models;
using RobotCleaner.Validators;

namespace RobotCleanerTests.Validators
{
    [TestFixture]
    public class LocationValidatorTest
    {
        public static IEnumerable<object[]> InvalidColumnCases = new[]
        {
            new object[] { new Location(-100001, 0) },
            new object[] { new Location(100001, 0) }
        };

        public static IEnumerable<object[]> InvalidRowCases = new[]
        {
            new object[] { new Location(0, -100001) },
            new object[] { new Location(0, 100001) }
        };

        [TestCaseSource(nameof(InvalidColumnCases))]
        public void ValidateLocation_ShouldThrowExceptionWhenColumnIsInvalid(Location location)
        {
            var locationValidator = new LocationValidator();

            Assert.Throws<ArgumentOutOfRangeException>(() => locationValidator.ThrowIfInvalid(location));
        }

        [TestCaseSource(nameof(InvalidRowCases))]
        public void ValidateLocation_ShouldThrowExceptionWhenRowIsInvalid(Location location)
        {
            var locationValidator = new LocationValidator();

            Assert.Throws<ArgumentOutOfRangeException>(() => locationValidator.ThrowIfInvalid(location));
        }

        [Test]
        public void ValidateLocation_ShouldThrowExceptionWhenLocationIsNull()
        {
            var locationValidator = new LocationValidator();

            Assert.Throws<ArgumentNullException>(() => locationValidator.ThrowIfInvalid(null));
        }

        [Test]
        public void ValidateLocation_ShouldNotThrowException()
        {
            var location = new Location(-100000, 100000);
            var locationValidator = new LocationValidator();

            Assert.DoesNotThrow(() => locationValidator.ThrowIfInvalid(location));
        }
    }
}
