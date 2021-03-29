using System.Threading.Tasks;
using LightsOutPuzzle.Domain.Entities;
using LightsOutPuzzle.Domain.ValueObjects;

namespace LightsOutPuzzle.Application.Interfaces
{
    public interface ILightsPuzzleGameService
    {
        public Board ToggleAdjacentLights(Cell cell);
        // public Task SaveCurrentGame();
        public Board StartNewGame(string dimensions);
    }
}