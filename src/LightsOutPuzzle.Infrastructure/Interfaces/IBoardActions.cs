using LightsOutPuzzle.Infrastructure.Models;

namespace LightsOutPuzzle.Infrastructure.Interfaces
{
    public interface IBoardActions
    {
        public void ToggleAdjacentLights(CurrentLightDto light);
        public bool CheckIfComplete();
        public void GiveUp();
    }
}