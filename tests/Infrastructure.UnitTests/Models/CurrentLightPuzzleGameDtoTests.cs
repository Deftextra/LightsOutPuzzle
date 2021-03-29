using System;
using FluentAssertions;
using LightsOutPuzzle.Infrastructure.Models;
using Xunit;

namespace Infrastructure.UnitTests.Models
{
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
        [InlineData("5*5")]
        public void ToggleAdjacentLights_ShouldToggleAllAdjacentLights_WhenLightHasFourAdjacentLights(string dimensions)
        {
            // Arrange
            // Act
            // Assert
        }

        [Theory]
        [InlineData("5*5")]
        public void IsComplete_ShouldReturnTrue_WhenAllLightsAreOff(string dimensions)
        {
            // Arrange
            // Act
            // Assert
        }
    }
}