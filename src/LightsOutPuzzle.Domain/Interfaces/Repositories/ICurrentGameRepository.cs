using LightsOutPuzzle.Domain.Entities;
using LightsOutPuzzle.Domain.ValueObjects;

namespace LightsOutPuzzle.Domain.Interfaces.Repositories
{
    public interface ICurrentGameRepository
    {
        public Board ToggleAdjacentLights(Cell light);
        public Board GetCurrentGame();
        public Board CreateNewCurrentGame(string dimension);
    }
}