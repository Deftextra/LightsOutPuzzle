using System.ComponentModel.DataAnnotations;

namespace LightsOutPuzzle.MVC.Models
{
    public class LightTogglePosition
    {
        [Required]
        public int PositionX { get; set; }
        [Required]
        public int PositionY { get; set; }
        [Required]
        public bool IsOn { get; set; }
    }
}