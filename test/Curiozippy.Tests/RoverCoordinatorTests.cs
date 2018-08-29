using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        [Fact]
        public void Command_should_be_valid()
        {
            var coordinator = new RoverCoordinator(5, 5);
            var roverId = coordinator.DeployRover(0, 1, 'N');

            coordinator.CommandRover(roverId, "RRMLLMMMMRR");
            coordinator.CommandRover(roverId, "RMLLLMMMMMR");
            coordinator.CommandRover(roverId, "LMMMRMLLMMMMRR");

            Assert.Throws<ArgumentException>(() => coordinator.CommandRover(roverId, "LRMr"));
        }

        [Fact]
        public void Test_outputs()
        {
            var coordinator = new RoverCoordinator(5, 5);
            var roverId1 = coordinator.DeployRover(1, 2, 'N');
            var status1 = coordinator.CommandRover(roverId1, "LMLMLMLMM");
            
            var roverId2 = coordinator.DeployRover(3, 3, 'E');
            var status2 = coordinator.CommandRover(roverId2, "MMRMMRMRRM");

            Assert.Equal("1 3 N", status1);
            Assert.Equal("5 1 E", status2);
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

        public string CommandRover(int roverId, string command)
        {
            if (!_rovers.ContainsKey(roverId))
            {
                throw new InvalidOperationException($"Rover {roverId} disconnected!!");
            }

            var validCommand = !string.IsNullOrEmpty(command) && Regex.IsMatch(command, @"^[LRM]*$");

            if (!validCommand)
            {
                throw new ArgumentException("Invalid command", nameof(command));
            }

            var rover = _rovers[roverId];

            command
                .Select(c => c)
                .ToList()
                .ForEach(c =>
                {
                    switch (c)
                    {
                        case 'L':
                            rover.RotateLeft();
                            break;
                        case 'R':
                            rover.RotateRight();
                            break;
                        case 'M':
                            rover.Move();
                            break;
                        default:
                            break;
                    }
                });

            return rover.ToString();
        }
    }
}