using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scoreboards.Data;
using Scoreboards.Data.Models;
using Scoreboards.Models;
using Scoreboards.Models.Home;
using Scoreboards.Models.Users;
using Scoreboards.Models.UserGames;

namespace Scoreboards.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserGame _userGameService;
        private readonly IApplicationUser _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
              IUserGame userGameService
            , IApplicationUser userService
            , UserManager<ApplicationUser> userManager)
        {
            _userGameService = userGameService;
            _userManager = userManager;
            _userService = userService;
        }
        public IEnumerable<MatchHistoryModel> GetMatchDataWithId(string userId)
        {
            var history = _userGameService.GetAll().Where(user=>user.User_01_Id==userId || user.User_02_Id == userId).Select(user => new MatchHistoryModel
            {
                UserId = userId
            });
            return history;
        }
        public IActionResult Index()
        {
            var leaderBoardData = _userService.GetAll().Select(user => new LeaderboardUserModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Wins = _userGameService.getWinsById(user.Id).ToString(),
                Loses = _userGameService.getLosesById(user.Id).ToString(),
                Ratio = _userGameService.getRatioWithId(user.Id).ToString(),
                History = GetMatchDataWithId(user.Id)
            });

            var model = new HomeIndexModel
            {
                UsersData = leaderBoardData.OrderBy(user => decimal.Parse(user.Ratio)).Reverse(),
                x = "0"
            };

            // Display to the page
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
