using LightsOutPuzzle.Domain.ValueObjects;
using LightsOutPuzzle.Infrastructure.Interfaces;

namespace LightsOutPuzzle.Infrastructure.Models
{
    public class CurrentLightDto : ILightActions
    {
        public int Id { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public LightValue Value { get; set; }

        public LightValue TurnOn()
        {
            return Value = LightValue.On;
        }

        public LightValue TurnOff()
        {
            return Value = LightValue.Off;
        }

        public bool IsOn()
        {
            return Value == LightValue.On;
        }

        public bool Toggle()
        {
            if (IsOn())
            {
                return TurnOff() == LightValue.On;
            }
            
            return TurnOn() == LightValue.On;
        }
    }
}