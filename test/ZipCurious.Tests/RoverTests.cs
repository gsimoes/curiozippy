using System;
using System.Collections.Generic;
using Xunit;

namespace ZipCurious.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Rover_has_initial_direction()
        {
            var rover = new Rover(0, 0, Direction.South);

            Assert.Equal(Direction.South, rover.Peek());
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
        private LinkedListNode<Direction> _currentAction;

        public Rover(int x, int y, Direction direction)
        {
            _x = x;
            _y = y;
            _direction = direction;

            var actions = new LinkedList<Direction>();

            actions.AddLast(Direction.North);
            actions.AddLast(Direction.East);
            actions.AddLast(Direction.South);
            actions.AddLast(Direction.West);

            _currentAction = actions.Find(direction);
        }

        public Direction Peek()
        {
            return _currentAction.Value;
        }

        public void RotateLeft()
        {

        }
    }
}
