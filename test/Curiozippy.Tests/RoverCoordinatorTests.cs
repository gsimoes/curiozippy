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
}