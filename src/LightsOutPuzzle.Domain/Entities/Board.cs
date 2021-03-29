using System.Collections.Generic;
using LightsOutPuzzle.Domain.ValueObjects;

namespace LightsOutPuzzle.Domain.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public string Dimension { get; set; }
        public IEnumerable<IEnumerable<Cell>> Lights { get; set; }
    }
}