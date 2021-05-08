using System;
using System.Linq;
using LightsOutPuzzle.Domain.Entities;
using LightsOutPuzzle.Domain.Interfaces.Repositories;
using LightsOutPuzzle.Domain.ValueObjects;
using LightsOutPuzzle.Infrastructure.Models;

namespace LightsOutPuzzle.Infrastructure.Repositories
{
    public class CurrentGameRepository : ICurrentGameRepository
    {
        // TODO: Move storage externally.
        private CurrentLightPuzzleGameDto _currentGame;

        public Board ToggleAdjacentLights(Cell light)
        {
            var currentLight = new CurrentLightDto
            {
                Id = light.Id,
                PositionX = light.PositionX,
                PositionY = light.PositionY,
                Value = light.Value
            };

            _currentGame.ToggleAdjacentLights(currentLight);
            
            return MapToBoard();
        }

        public Board GetCurrentGame()
        {
            if (_currentGame == null)
            {
                throw new Exception("Game Does not exist. Please create a game");
            }

            return MapToBoard();
        }

        public Board CreateNewCurrentGame(string dimensions)
        {
            _currentGame = new CurrentLightPuzzleGameDto(dimensions);

            return MapToBoard();
        }

        // TODO: Review use case for automapper.
        private Board MapToBoard()
        {
            var currentGameLightsMapped = _currentGame.Lights
                .Select(rows => rows
                    .Select(light => new Cell
                    {
                        Id = light.Id,
                        Value = light.Value,
                        PositionX = light.PositionX,
                        PositionY = light.PositionY
                    }));

            return new Board
            {
                Id = _currentGame.Id,
                IsCompleted = _currentGame.CheckIfComplete(),
                Dimension = _currentGame.Dimension,
                Lights = currentGameLightsMapped
            };
        }
    }
}