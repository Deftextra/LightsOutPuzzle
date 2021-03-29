using FluentAssertions;
using LightsOutPuzzle.Infrastructure.Models;
using Xunit;

namespace Infrastructure.UnitTests.Models
{
    public class CurrentLightDtoTests
    {
       [Fact]
        public void TurnOn_ShouldTurnLightOn()
        {
            // Arrange
            var light = new CurrentLightDto();
            // Act
            light.TurnOn();
            // Assert
            light.IsOn().Should().BeTrue();
        }
        [Fact]
        public void TurnOff_ShouldTurnLightOff()
        {
            // Arrange
            var light = new CurrentLightDto();
            // Act
            light.TurnOff();
            // Assert
            light.IsOn().Should().BeFalse();
        }
        
        [Fact]
        public void Toggle_ShouldTurnLightOff_WhenOn()
        {
            // Arrange
            var light = new CurrentLightDto();
            light.IsOn();
            // Act
            light.Toggle();
            // Assert 
            light.IsOn().Should().BeTrue();
        }
        
        [Fact]
        public void Toggle_ShouldTurnLightOn_WhenOff()
        {
            // Arrange
            var light = new CurrentLightDto();
            light.TurnOff();
            // Act
            light.Toggle();
            // Assert 
            light.IsOn().Should().BeTrue();
        }
        
    }
}