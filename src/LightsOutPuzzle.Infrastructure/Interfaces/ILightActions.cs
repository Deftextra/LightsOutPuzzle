using LightsOutPuzzle.Domain.ValueObjects;

namespace LightsOutPuzzle.Infrastructure.Interfaces
{
    public interface ILightActions
    {
        public LightValue TurnOn();
        public LightValue TurnOff();
        public bool Toggle();
        public bool IsOn();
    }
}