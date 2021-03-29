using System;
using FluentAssertions;
using LightsOutPuzzle.Infrastructure.Repositories;
using Xunit;

namespace Infrastructure.UnitTests.Repositories
{
    public class CurrentGameRepositoryTests
    {
        [Fact]
        public void GetCurrentGame_ShouldThrowException_WhenCurrentGameIsNull()
        {
            // Arrange
            var sut = new CurrentGameRepository();
            // Act
            Action getCurrentGameAction = () => sut.GetCurrentGame();

            // Assert
            getCurrentGameAction.Should().Throw<Exception>("Game Does not exist. Please create a game");
        }

        [Theory]
        [InlineData("2x2")]
        public void CreateNewCurrentGame_ShouldCreateNewGame_WhenDimensionArgumentIsValid(string dimensionInput)
        {
            // Arrange
            var sut = new CurrentGameRepository();
            // Act
            var result = sut.CreateNewCurrentGame(dimensionInput);
            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData("2x2")]
        public void GetCurrentGame_ShouldReturnCurrentGame_WhenCurrentGameIsCreated(string dimensionInput)
        {
            // Arrange
            var sut = new CurrentGameRepository();
            sut.CreateNewCurrentGame(dimensionInput);
            // Act
            var result = sut.GetCurrentGame();
            // Assert
            result.Should().NotBeNull();
        }

    }
}