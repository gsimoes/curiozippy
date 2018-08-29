using System;
using System.Collections.Generic;
using Xunit;

namespace Curiozippy.Tests
{
    public class RoverTests
    {
        [Fact]
        public void Rover_has_initial_direction()
        {
            var rover = new Rover(0, 0, Direction.South);
            var next = rover.NextPosition();

            Assert.Equal(Direction.South, next.direction);
        }

        [Fact]
        public void When_rotate_right_direction_goes_clockwise()
        {
            var rover = new Rover(0, 0, Direction.North);

            rover.RotateRight();
            Assert.Equal(Direction.East, rover.NextPosition().direction);

            rover.RotateRight();
            Assert.Equal(Direction.South, rover.NextPosition().direction);

            rover.RotateRight();
            Assert.Equal(Direction.West, rover.NextPosition().direction);

            rover.RotateRight();
            Assert.Equal(Direction.North, rover.NextPosition().direction);
        }

        [Fact]
        public void When_rotate_left_direction_goes_counter_clockwise()
        {
            var rover = new Rover(0, 0, Direction.North);

            rover.RotateLeft();
            Assert.Equal(Direction.West, rover.NextPosition().direction);

            rover.RotateLeft();
            Assert.Equal(Direction.South, rover.NextPosition().direction);

            rover.RotateLeft();
            Assert.Equal(Direction.East, rover.NextPosition().direction);

            rover.RotateLeft();
            Assert.Equal(Direction.North, rover.NextPosition().direction);
        }

        [Fact]
        public void NextPosition_should_return_next_planned_movement_and_direction()
        {
            var rover = new Rover(0, 0, Direction.North);
            var next = rover.NextPosition();

            Assert.Equal((x: 0, y: 1, direction: Direction.North), next);

            rover.RotateRight();
            next = rover.NextPosition();
            Assert.Equal((x: 1, y: 0, direction: Direction.East), next);

            rover.RotateRight();
            next = rover.NextPosition();
            Assert.Equal((x: 0, y: -1, direction: Direction.South), next);

            rover.RotateRight();
            next = rover.NextPosition();
            Assert.Equal((x: -1, y: 0, direction: Direction.West), next);
        }

        [Fact]
        public void Move_will_change_rover_coordinates()
        {
            var rover = new Rover(0, 0, Direction.North);
            var status = rover.Move();

            Assert.Equal((x: 0, y: 1, direction: Direction.North), status);

            rover.RotateRight();
            status = rover.Move();
            Assert.Equal((x: 1, y: 1, direction: Direction.East), status);

            rover.RotateRight();
            status = rover.Move();
            Assert.Equal((x: 1, y: 0, direction: Direction.South), status);

            rover.RotateRight();
            status = rover.Move();
            Assert.Equal((x: 0, y: 0, direction: Direction.West), status);
        }
    }

   


}
