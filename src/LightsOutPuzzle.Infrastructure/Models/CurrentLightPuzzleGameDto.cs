using System;
using System.Collections.Generic;
using System.Linq;
using LightsOutPuzzle.Infrastructure.Interfaces;

namespace LightsOutPuzzle.Infrastructure.Models
{
    public class CurrentLightPuzzleGameDto : IBoardActions
    {
        public CurrentLightPuzzleGameDto(string dimension)
        {
            ParseDimensionalValue(dimension);

            ConstructLightBoard();

            PopulateLightBoard();
        }

        public int Id { get; set; }
        public string Dimension { get; set; }
        public int NumRows { get; private set; }
        public int NumColumns { get; private set; }

        public IList<IList<CurrentLightDto>> Lights { get; private set; }

        public void ToggleAdjacentLights(CurrentLightDto light)
        {
            if (light.IsOn() != Lights[light.PositionX][light.PositionY].IsOn())
            {
                throw new ArgumentException(
                    $"pressed light at position {light.PositionX}x{light.PositionY} is out of Sync");
            }

            Lights[light.PositionX][light.PositionY].Toggle();
            ToggleLeftLight(light);
            ToggleRightLight(light);
            ToggleTopLight(light);
            ToggleBottomLight(light);
        }

        public bool CheckIfComplete()
        {
            var isComplete = true;

            foreach (var rowLights in Lights)
            {
                foreach (var light in rowLights)
                {
                    isComplete &= !light.IsOn();
                }
            }

            return isComplete;
        }

        public CurrentLightDto GetLightRelative(IList<int> position, IList<int> vector)
        {
            // Note: In the future we might want to support multi dimensional boards.
            if (vector.Count != 2 || position.Count > 2)
            {
                throw new ArgumentException("Vector must be two dimensional");
            }

            if (
                lightPositionIsOnBoard(position)
                && lightPositionIsOnBoard(AddTwoPosition(position, vector).ToList())
            )
            {
                return Lights[position[1] + vector[1]][position[0] + vector[0]];
            }

            return null;
        }

        public CurrentLightDto GetLight(IList<int> position)
        {
            if (position.Count != 2)
            {
                throw new ArgumentException("Vector must be two dimensional");
            }

            // Note: This will throw an argument out of range exception if position is not on board.
            return Lights[position[1]][position[0]];
        }

        public void ToggleLightRelativeTo(CurrentLightDto light, IList<int> vector)
        {
            // Note: In the future we might want to support multi dimensional boards.
            if (vector.Count != 2)
            {
                throw new ArgumentException("Vector must be two dimensional");
            }

            var relativeLight = GetLightRelative(new List<int>
                {
                    light.PositionX,
                    light.PositionY
                },
                vector);

            relativeLight?.Toggle();
        }

        public void TurnAllLightsOff()
        {
            foreach (var lightsRow in Lights)
            {
                foreach (var light in lightsRow)
                {
                    light.TurnOff();
                }
            }
        }

        private void ParseDimensionalValue(string dimension)
        {
            var splitDimensions = dimension.Split('x');

            if (splitDimensions.Length != 2)
            {
                throw new ArgumentException($"{nameof(CurrentLightPuzzleGameDto)} Should be two dimensional");
            }

            var parsedDimensions = splitDimensions.Select(int.Parse)
                .ToList();

            if (parsedDimensions[0] != parsedDimensions[1])
            {
                throw new ArgumentException($"{nameof(CurrentLightPuzzleGameDto)} dimension Should be square");
            }

            NumRows = parsedDimensions[0];
            NumColumns = parsedDimensions[1];
        }

        private void ConstructLightBoard()
        {
            Lights = new CurrentLightDto[NumRows][];
            for (var i = 0; i < NumRows; ++i)
            {
                Lights[i] = new CurrentLightDto[NumColumns];
            }
        }

        private void PopulateLightBoard()
        {
            for (var i = 0; i < NumRows; i++)
            for (var j = 0; j < NumColumns; j++)
            {
                Lights[i][j] = CreateLightInRandomState(i, j);
            }
        }

        private void ToggleLeftLight(CurrentLightDto light)
        {
            if (light.PositionX != 0)
            {
                Lights[light.PositionY][light.PositionX - 1].Toggle();
            }
        }

        private void ToggleRightLight(CurrentLightDto light)
        {
            if (light.PositionX != NumColumns - 1)
            {
                Lights[light.PositionY][light.PositionX + 1].Toggle();
            }
        }

        private void ToggleTopLight(CurrentLightDto light)
        {
            if (light.PositionY != 0)
            {
                Lights[light.PositionY - 1][light.PositionX].Toggle();
            }
        }

        private void ToggleBottomLight(CurrentLightDto light)
        {
            if (light.PositionY != NumRows - 1)
            {
                Lights[light.PositionY + 1][light.PositionX].Toggle();
            }
        }

        private CurrentLightDto CreateLightInRandomState(int positionX, int positionY)
        {
            var light = new CurrentLightDto();
            var rnd = new Random();

            light.PositionX = positionX;
            light.PositionY = positionY;

            if (rnd.Next(0, 2) == 1)
            {
                light.Toggle();
            }

            return light;
        }

        private bool lightPositionIsOnBoard(IList<int> lightPosition)
        {
            return lightPosition[0] > 0 && lightPosition[1] > 0 && lightPosition[0] < NumColumns &&
                   lightPosition[1] < NumRows;
        }

        private IEnumerable<int> AddTwoPosition(IList<int> position1, IList<int> position2)
        {
            return new List<int>
            {
                position1[0] + position2[0],
                position1[1] + position2[1]
            };
        }
    }
}