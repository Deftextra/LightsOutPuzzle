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

        private bool IsComplete { get; set; } = true;
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

            light.Toggle();
            ToggleLeftLight(light);
            ToggleRightLight(light);
            ToggleTopLight(light); 
            ToggleBottomLight(light);
        }

        public bool CheckIfComplete()
        {
            foreach (var rowLights in Lights)
            {
                foreach (var light in rowLights)
                {
                    IsComplete &= !light.IsOn();
                }
            }

            return IsComplete;
        }


        public void GiveUp()
        {
            throw new NotImplementedException();
        }

        private void ParseDimensionalValue(string dimension)
        {
            var splitDimensions = dimension.Split('x');

            if (splitDimensions.Length != 2)
                throw new ArgumentException($"{nameof(CurrentLightPuzzleGameDto)} Should be two dimensional");

            var parsedDimensions = splitDimensions.Select(int.Parse)
                .ToList();

            if (parsedDimensions[0] != parsedDimensions[1])
                throw new ArgumentException($"{nameof(CurrentLightPuzzleGameDto)} dimension Should be square");

            NumRows = parsedDimensions[0];
            NumColumns = parsedDimensions[1];
        }

        private void ConstructLightBoard()
        {
            Lights = new CurrentLightDto[NumRows][];
            for (int i = 0; i < NumRows; ++i)
            {
                Lights[i] = new CurrentLightDto[NumColumns];
            }
        }

        private void PopulateLightBoard()
        {
            for (int i = 0; i < NumRows; i++)
            {
                for (int j = 0; j < NumColumns; j++)
                {
                    Lights[i][j] = CreateLightInRandomState(i, j);
                }
            }
        }

        private void ToggleLeftLight(CurrentLightDto light)
        {
            if (light.PositionX != 0)
            {
                Lights[light.PositionX - 1][light.PositionY].Toggle();
            }
            
        }

        private void ToggleRightLight(CurrentLightDto light)
        {
            if (light.PositionX != NumColumns)
            {
                Lights[light.PositionX + 1][light.PositionY].Toggle();
            }
            
        }

        private void ToggleTopLight(CurrentLightDto light)
        {
            if (light.PositionY != 0)
            {
                Lights[light.PositionX][light.PositionY - 1].Toggle();
            }
        }

        private void ToggleBottomLight(CurrentLightDto light)
        {
            if (light.PositionY != NumRows)
            {
                Lights[light.PositionX][light.PositionY + 1].Toggle();
            }
        }
        

        private CurrentLightDto CreateLightInRandomState(int positionX, int positionY)
        {
            var light = new CurrentLightDto();
            var rnd = new Random();

            // Set position on the board.
            light.PositionX = positionX;
            light.PositionX = positionY;

            // Toggle light Randomly.
            if (rnd.Next() > 0.5) light.Toggle();
            return light;
        }
    }
}
