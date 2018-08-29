using System;
using System.Collections.Generic;

namespace Curiozippy
{
    public enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public class Rover
    {
        private int _x;
        private int _y;
        private LinkedList<Direction> _directions;
        private LinkedListNode<Direction> _direction;

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

            _directions = new LinkedList<Direction>();

            _directions.AddLast(Direction.North);
            _directions.AddLast(Direction.East);
            _directions.AddLast(Direction.South);
            _directions.AddLast(Direction.West);

            _direction = _directions.Find(direction);
        }

        public (int x, int y, Direction direction) PeekNext()
        {
            var action = _actions[_direction.Value];
            var next = action(_x, _y);

            return (x: next.x, y: next.y, direction: _direction.Value);
        }

        public (int x, int y, Direction direction) Move()
        {
            var action = _actions[_direction.Value];
            var next = action(_x, _y);

            _x = next.x;
            _y = next.y;

            return (x: _x, y: _y, direction: _direction.Value);
        }

        public void RotateLeft()
        {
            _direction = _direction.Previous ?? _directions.Last;
        }

        public void RotateRight()
        {
            _direction = _direction.Next ?? _directions.First;
        }
    }
}