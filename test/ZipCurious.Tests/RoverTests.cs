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

            Assert.Equal(Direction.South, rover.Peek());
        }

        [Fact]
        public void When_rotate_right_direction_goes_clockwise()
        {
            var rover = new Rover(0, 0, Direction.North);

            rover.RotateLeft();
            Assert.Equal(Direction.East, rover.Peek());
            
            rover.RotateLeft();
            Assert.Equal(Direction.South, rover.Peek());

            rover.RotateLeft();
            Assert.Equal(Direction.West, rover.Peek());

            rover.RotateLeft();
            Assert.Equal(Direction.North, rover.Peek());
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
        private LinkedList<Direction> _actions;
        private LinkedListNode<Direction> _currentAction;

        public Rover(int x, int y, Direction direction)
        {
            _x = x;
            _y = y;
            _direction = direction;

            _actions = new LinkedList<Direction>();

            _actions.AddLast(Direction.North);
            _actions.AddLast(Direction.East);
            _actions.AddLast(Direction.South);
            _actions.AddLast(Direction.West);

            _currentAction = _actions.Find(direction);
        }

        public Direction Peek()
        {
            return _currentAction.Value;
        }

        public void RotateLeft()
        {
            _currentAction = _currentAction.Next ?? _actions.First;
        }
    }
}
