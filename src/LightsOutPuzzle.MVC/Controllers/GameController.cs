using System;
using System.Linq;
using LightsOutPuzzle.Application.Interfaces;
using LightsOutPuzzle.Domain.Entities;
using LightsOutPuzzle.Domain.ValueObjects;
using LightsOutPuzzle.MVC.Models;
using LightsOutPuzzle.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace LightsOutPuzzle.MVC.Controllers
{
    public class GameController : Controller
    {
        private readonly ILightsPuzzleGameService _lightsPuzzleGameService;
        
        public GameController(ILightsPuzzleGameService lightsPuzzleGameService)
        {
            _lightsPuzzleGameService = lightsPuzzleGameService;
        }
        
        public IActionResult StartGame()
        {
            try {
                var game = _lightsPuzzleGameService.StartNewGame("5x5");
                var viewGame = MapToViewModel(game);
                
                return View("Game",viewGame);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ToggleAdjacentLights(LightTogglePosition position)
        {
            try
            {
                var cell = new Cell()
                {
                    PositionX = position.PositionX,
                    PositionY = position.PositionY,
                    Value = position.IsOn ? LightValue.On : LightValue.Off
                };

                var toggledBoard = _lightsPuzzleGameService.ToggleAdjacentLights(cell);

                return View("Game",MapToViewModel(toggledBoard));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private LightPuzzleGameViewModel MapToViewModel(Board board)
        {
            var viewLights = board.Lights.Select(cells => cells.Select(cell =>
                    new LightViewModel()
                    {
                        IsOn = cell.Value == LightValue.On,
                        PositionX = cell.PositionX,
                        PositionY = cell.PositionY,
                    }
                )
            );

            return new LightPuzzleGameViewModel()
            {
                Lights = viewLights,
                IsCompleted = board.IsCompleted
            };
        }
    }
}