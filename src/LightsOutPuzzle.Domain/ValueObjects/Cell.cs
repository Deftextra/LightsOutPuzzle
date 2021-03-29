namespace LightsOutPuzzle.Domain.ValueObjects
{
    public class Cell 
    {
        public int Id { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        
        public int BoardId { get; set; }
        public LightValue Value { get; set; }
    }
}