using Scoreboards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scoreboards.Models.NewGame
{
    public class NewGameIndexModel
    {
        public IEnumerable<Game> gameList { get; set; }
    }
}
