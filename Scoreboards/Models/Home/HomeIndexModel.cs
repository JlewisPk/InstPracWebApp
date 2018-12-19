using Scoreboards.Models.UserGames;
using System.Collections.Generic;

namespace Scoreboards.Models.Home
{
    public class HomeIndexModel
    {
        public IEnumerable<LeaderboardUserModel> UsersData { get; set; }
        public string x { get; set; }
    }
}
