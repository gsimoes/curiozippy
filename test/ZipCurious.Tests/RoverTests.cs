using System;
using System.Collections.Generic;
using Xunit;

namespace ZipCurious.Tests
{
    public class RoverTests
    {
        [Fact]
        public void Rover_has_initial_direction()
        {
            var rover = new Rover(0, 0, Direction.South);
            var next = rover.PeekNext();

            Assert.Equal(Direction.South, next.direction);
        }

        [Fact]
        public void When_rotate_right_direction_goes_clockwise()
        {
            var rover = new Rover(0, 0, Direction.North);

            rover.RotateRight();
            Assert.Equal(Direction.East, rover.PeekNext().direction);

            rover.RotateRight();
            Assert.Equal(Direction.South, rover.PeekNext().direction);

            rover.RotateRight();
            Assert.Equal(Direction.West, rover.PeekNext().direction);

            rover.RotateRight();
            Assert.Equal(Direction.North, rover.PeekNext().direction);
        }

        [Fact]
        public void When_rotate_left_direction_goes_counter_clockwise()
        {
            var rover = new Rover(0, 0, Direction.North);

            rover.RotateLeft();
            Assert.Equal(Direction.West, rover.PeekNext().direction);

            rover.RotateLeft();
            Assert.Equal(Direction.South, rover.PeekNext().direction);

            rover.RotateLeft();
            Assert.Equal(Direction.East, rover.PeekNext().direction);

            rover.RotateLeft();
            Assert.Equal(Direction.North, rover.PeekNext().direction);
        }

        [Fact]
        public void PeekNext_should_return_next_planned_movement_and_direction()
        {
            var rover = new Rover(0, 0, Direction.North);
            var next = rover.PeekNext();

            Assert.Equal((x: 0, y: 1, direction: Direction.North), next);

            rover.RotateRight();
            next = rover.PeekNext();
            Assert.Equal((x: 1, y: 0, direction: Direction.East), next);

            rover.RotateRight();
            next = rover.PeekNext();
            Assert.Equal((x: 0, y: -1, direction: Direction.South), next);

            rover.RotateRight();
            next = rover.PeekNext();
            Assert.Equal((x: -1, y: 0, direction: Direction.West), next);
        }
    }

    enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    class Rover
    {
        private int _x;
        private int _y;
        private Direction _direction;
        private LinkedList<Direction> _directions;
        private LinkedListNode<Direction> _currentAction;

        private static Dictionary<Direction, Func<int, int, (int x, int y)>> _actions
            = new Dictionary<Direction, Func<int, int, (int x, int y)>>()
            {
                { Direction.North, (x, y) => (x: x, y: y + 1)  },
                { Direction.East, (x, y) => (x: x + 1, y: y)  },
                { Direction.South, (x, y) => (x: x, y: y - 1)  },
                { Direction.West, (x, y) => (x: x - 1, y: y)  }
            };

        public Rover(int x, int y, Direction direction)
        {
            _x = x;
            _y = y;
            _direction = direction;

            _directions = new LinkedList<Direction>();

            _directions.AddLast(Direction.North);
            _directions.AddLast(Direction.East);
            _directions.AddLast(Direction.South);
            _directions.AddLast(Direction.West);

            _currentAction = _directions.Find(direction);
        }

        public (int x, int y, Direction direction) PeekNext()
        {
            var action = _actions[_currentAction.Value];
            var next = action(_x, _y);

            return (x: next.x, y: next.y, direction: _currentAction.Value);
        }

        public void RotateLeft()
        {
            _currentAction = _currentAction.Previous ?? _directions.Last;
        }

        public void RotateRight()
        {
            _currentAction = _currentAction.Next ?? _directions.First;
        }
    }
}
