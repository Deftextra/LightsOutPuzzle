using System.Collections.Generic;

namespace LightsOutPuzzle.MVC.ViewModels
{
    public class LightPuzzleGameViewModel
    {
        public bool IsCompleted { get; set; }
        public IEnumerable<IEnumerable<LightViewModel>> Lights { get; set; }
    }
}