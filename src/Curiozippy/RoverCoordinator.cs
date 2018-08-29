using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Curiozippy
{
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

        private Dictionary<int, Rover> _rovers = new Dictionary<int, Rover>();
        private HashSet<string> _obstacles = new HashSet<string>();

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

            if (_obstacles.Contains($"{x}:{y}"))
            {
                throw new InvalidOperationException("Rover detected in that coordinate");
            }

            // todo: verify if there is a rover in that coordinate
            var roverId = _rovers.Count + 1;
            _rovers.Add(roverId, new Rover(x, y, _directions[direction]));

            _obstacles.Add($"{x}:{y}");

            return roverId;
        }

        public string CommandRover(int roverId, string commands)
        {
            if (!_rovers.ContainsKey(roverId))
            {
                throw new InvalidOperationException($"Rover {roverId} disconnected!!");
            }

            var validCommand = !string.IsNullOrEmpty(commands) && Regex.IsMatch(commands, @"^[LRM]*$");

            if (!validCommand)
            {
                throw new ArgumentException("Invalid command", nameof(commands));
            }

            var rover = _rovers[roverId];

            foreach (var command in commands)
            {
                SendCommand(rover, command);
            }

            return rover.ToString();
        }

        private void SendCommand(Rover rover, char command)
        {
            switch (command)
            {
                case 'L':
                    rover.RotateLeft();
                    break;
                case 'R':
                    rover.RotateRight();
                    break;
                case 'M':
                    {
                        var position = rover.NextPosition();

                        if (_obstacles.Contains($"{position.x}:{position.y}"))
                        {
                            // don't move if obstable ahead
                            return;
                        }

                        position = rover.Position();

                        _obstacles.Remove($"{position.x}:{position.y}");
                        // todo: colision control / out of bounds
                        position = rover.Move();

                        _obstacles.Add($"{position.x}:{position.y}");
                        break;
                    }
                default:
                    break;
            }
        }
    }
}