using System;
using System.Collections.Generic;
using Xunit;

namespace Curiozippy.Tests
{
    public class RoverCoordinatorTests
    {
        [Fact]
        public void Deploy_rover_requires_valid_input()
        {
            var coordinator = new RoverCoordinator(5, 5);

            Assert.Throws<ArgumentException>(() => coordinator.DeployRover(6, 0, 'N'));
            Assert.Throws<ArgumentException>(() => coordinator.DeployRover(0, 0, 'i'));
            Assert.Throws<ArgumentException>(() => coordinator.DeployRover(0, 6, 'S'));
        }
    }

    public class RoverCoordinator
    {
        private int _plateauX;
        private int _plateauY;

        private Dictionary<char, Direction> _directions = new Dictionary<char, Direction>()
        {
            { 'N', Direction.North },
            { 'E', Direction.East },
            { 'S', Direction.South },
            { 'W', Direction.West }
        };

        public Dictionary<int, Rover> _rovers = new Dictionary<int, Rover>();

        public RoverCoordinator(int plateauX, int plateauY)
        {
            _plateauX = plateauX;
            _plateauY = plateauY;
        }

        public int DeployRover(int x, int y, char direction)
        {
            if (x < 0 || x > _plateauX)
            {
                throw new ArgumentException("Invalid X coordinate", nameof(x));
            }

            if (y < 0 || y > _plateauY)
            {
                throw new ArgumentException("Invalid Y coordinate", nameof(y));
            }

            if (!_directions.ContainsKey(direction))
            {
                throw new ArgumentException("Invalid direction", nameof(direction));
            }

            var roverId = _rovers.Count + 1;
            _rovers.Add(roverId, new Rover(x, y, _directions[direction]));

            return roverId;
        }
    }
}