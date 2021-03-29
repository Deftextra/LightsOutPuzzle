using System.Threading.Tasks;
using LightsOutPuzzle.Domain.Entities;

namespace LightsOutPuzzle.Domain.Interfaces.Repositories
{
    public interface ILightsPuzzleGameRepository
    {
        public Task<Board> GetGameById(int id);
        public Task SaveGame(Board id);
    }
}