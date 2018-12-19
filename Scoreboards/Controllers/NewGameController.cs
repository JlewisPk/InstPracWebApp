using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scoreboards.Data;
using Scoreboards.Data.Models;
using Scoreboards.Models.NewGame;

namespace Scoreboards.Controllers
{
    public class NewGameController : Controller
    {
        private readonly IGame _game;

        public NewGameController(
              IGame game)
        {
            _game = game;
        }

        public IActionResult Index()
        {
            // Wrap them into the model
            var gameList = _game.GetAll();

            var model = new NewGameIndexModel
            {
                gameList = gameList
            };
            return View(model);
        }
        public IActionResult AddNewGame()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadNewGame(IFormFile file, string GameName)
        {
            string fileName;
            Game newGameObj = new Game();
            newGameObj.GameName = GameName;
            if (file != null && file.Length > 0)
            {
                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                System.Diagnostics.Debug.WriteLine(fileName);
                newGameObj.GameLogo = fileName;
            }
            // right now saving fileName instead of url to image

            System.Diagnostics.Debug.WriteLine(newGameObj);
            await _game.AddGameAsync(newGameObj);
            return Ok();
        }
    }
}