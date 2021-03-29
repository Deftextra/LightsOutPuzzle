using LightsOutPuzzle.Application.Interfaces;
using LightsOutPuzzle.Domain.Entities;
using LightsOutPuzzle.Domain.Interfaces.Repositories;
using LightsOutPuzzle.Domain.ValueObjects;

namespace LightsOutPuzzle.Application.Service
{
    public class LightsPuzzleGameService : ILightsPuzzleGameService
    {
        // private readonly ILightsPuzzleGameRepository _boardRepository;
        private readonly ICurrentGameRepository _currentGameRepository;

        public LightsPuzzleGameService(
            // ILightsPuzzleGameRepository lightsPuzzleGameRepository,
            ICurrentGameRepository currentGameRepository)
        {
            _currentGameRepository = currentGameRepository;
            // _boardRepository = boardRepository;
        }

        public Board ToggleAdjacentLights(Cell light)
        {
            return _currentGameRepository.ToggleAdjacentLights(light);
        }

        // public async Task SaveCurrentGame()
        // {
        //     var currentGame = _currentGameRepository.GetCurrentGame();
        //
        //     await _boardRepository.SaveGame(currentGame);
        // }

        public Board StartNewGame(string dimensions)
        {
            return _currentGameRepository.CreateNewCurrentGame(dimensions);
        }

        public Board RestartGame(string dimension)
        {
            return _currentGameRepository.CreateNewCurrentGame(dimension);
        }
    }
}