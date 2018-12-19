using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Scoreboards.Models.NewGame
{
    public class AddNewGameModel
    {
        public string Id { get; set; }
        //Game Detail
        public string GameName { get; set; }
        public IFormFile GameLogo { get; set; }
    }
}
