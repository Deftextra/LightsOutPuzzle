using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LightsOutPuzzle.Infrastructure.Models;
using LightsOutPuzzle.Infrastructure.Repositories;
using Xunit;

namespace Infrastructure.UnitTests.Models
{
    // TODO: Add to readme board orientation.
    public class CurrentLightPuzzleGameDtoTests
    {
        [Theory]
        [InlineData("2x1x4")]
        [InlineData("2x3x1x6")]
        public void LightPuzzleGame_ShouldThrowException_WhenNotTwoDimensional(string dimension)
        {
            // Arrange
            Action createLightPuzzle = () =>
            {
                var lightPuzzleGame = new CurrentLightPuzzleGameDto(dimension);
            };

            //Assert
            createLightPuzzle.Should().Throw<ArgumentException>()
                .WithMessage($"{nameof(CurrentLightPuzzleGameDto)} Should be two dimensional");
        }

        [Theory]
        [InlineData("2x1")]
        public void LightPuzzleGame_ShouldThrowException_WhenDimensionNotSquare(string dimension)
        {
            // Arrange
            Action createLightPuzzle = () =>
            {
                var lightPuzzleGame = new CurrentLightPuzzleGameDto(dimension);
            };

            //Assert
            createLightPuzzle.Should().Throw<ArgumentException>()
                .WithMessage($"{nameof(CurrentLightPuzzleGameDto)} dimension should be square");
        }

        [Theory]
        [InlineData("2x2", 2)]
        [InlineData("3x3", 3)]
        public void LightPuzzleGame_ShouldSetXDimension_WhenLightPuzzleGameIsValid(string dimension,
            int expectXDimensionValue)
        {
            // Arrange
            var game = new CurrentLightPuzzleGameDto(dimension);

            // Assert
            expectXDimensionValue.Should().Be(game.NumRows);
        }

        [Theory]
        [InlineData("2x2", 2)]
        [InlineData("3x3", 3)]
        public void LightPuzzleGame_ShouldSetYDimension_WhenLightPuzzleGameIsValid(string dimension,
            int expectYDimensionValue)
        {
            // Arrange
            var game = new CurrentLightPuzzleGameDto(dimension);

            // Assert
            expectYDimensionValue.Should().Be(game.NumColumns);
        }

        [Theory]
        [InlineData("2x2", 2)]
        [InlineData("3x3", 3)]
        public void InLightPuzzleGame_LightsShouldBeWithinDimensionOfBoard_WhenLightPuzzleGameIsValid(string dimension,
            int expectYDimensionValue)
        {
            // Arrange
            var game = new CurrentLightPuzzleGameDto(dimension);

            // Assert
            expectYDimensionValue.Should().Be(game.NumColumns);
        }

        [Theory]
        [InlineData("5x5")]
        public void ToggleAdjacentLights_ShouldThrow_WhenViewAndModelOutOfSync(string dimensions)
        {
            // Arrange
            var sut = new CurrentLightPuzzleGameDto(dimensions);
            var light = new CurrentLightDto
            {
                PositionX = 2,
                PositionY = 2
            };
            light.TurnOff();
            sut.Lights[2][2].TurnOn();

            // Act
            Action toggle = () => sut.ToggleAdjacentLights(light);

            // Assert
            toggle.Should()
                .Throw<ArgumentException>().WithMessage(
                    $"pressed light at position {light.PositionX}x{light.PositionY} is out of Sync");
        }

        [Theory]
        // Toggle all adjacent lights.
        [InlineData("5x5", new[] {2, 2}, new[] {-1, 0})]
        [InlineData("5x5", new[] {2, 2}, new[] {+1, 0})]
        [InlineData("5x5", new[] {2, 2}, new[] {0, +1})]
        [InlineData("5x5", new[] {2, 2}, new[] {0, -1})]
        public void ToggleLightRelativeTo_ShouldToggleLight_ToggleTargetExists(string dimensions,
            IList<int> lightPosition, IList<int> toggleDirection)

        {
            // Arrange
            var sut = new CurrentLightPuzzleGameDto(dimensions);

            var light = new CurrentLightDto
            {
                PositionX = lightPosition[0],
                PositionY = lightPosition[1],
                Value = sut.GetLight(lightPosition).Value
            };

            var lightForToggle = sut.GetLightRelative(lightPosition, toggleDirection);

            var lightValueBeforeToggle = lightForToggle.IsOn();

            // Act 
            sut.ToggleLightRelativeTo(light, toggleDirection);

            var lightValueAfterToggle = lightForToggle.IsOn();
            // Assert
            lightValueBeforeToggle.Should().Be(!lightValueAfterToggle);
        }

        [Theory]
        [InlineData("5x5", new[] {0, 2}, new[] {-1, 2})]
        [InlineData("5x5", new[] {4, 2}, new[] {+1, 2})]
        [InlineData("5x5", new[] {2, 0}, new[] {0, -1})]
        [InlineData("5x5", new[] {4, 2}, new[] {0, +1})]
        public void ToggleLightRelativeTo_ShouldDoNothing_WhenToggleTargetDoesNotExist(string dimensions,
            IList<int> lightPosition, IList<int> toggleDirection)

        {
            // Arrange
            var sut = new CurrentLightPuzzleGameDto(dimensions);

            var toggleLight = new CurrentLightDto
            {
                PositionX = lightPosition[0],
                PositionY = lightPosition[1],
                Value = sut.GetLight(lightPosition).Value
            };

            var lightsBeforeToggle = sut.Lights.Select(row => row.Select(
                light => new CurrentLightDto
                {
                    Id = light.Id,
                    Value = light.Value,
                    PositionX = light.PositionX,
                    PositionY = light.PositionY
                }));

            // Act 
            sut.ToggleLightRelativeTo(toggleLight, toggleDirection);
            // Assert
            sut.Lights.Should().BeEquivalentTo(lightsBeforeToggle);
        }
        
        
        


        [Theory]
        [InlineData("5x5")]
        public void CheckIfComplete_ShouldReturnTrue_WhenAllLightsAreOff(string dimensions)
        {
            // Arrange
            var sut = new CurrentLightPuzzleGameDto(dimensions);

            // Act
            sut.TurnAllLightsOff();
            
            // Assert
            sut.CheckIfComplete().Should().BeTrue();
        }
        
    }
}