using Scoreboards.Data;
using Scoreboards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboards.Services
{
    public class GameService : IGame
    {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games;
        }

        public Game GetById(int gameId)
        {
            return GetAll().FirstOrDefault(game => game.Id == gameId);
        }

        public Game GetByName(string gameName)
        {
            return GetAll().FirstOrDefault(game => game.GameName == gameName);
        }

        public async Task AddGameAsync(Game game)
        {
            /**
            * Entity framwork handls all logic for us
            * all we need to do is to call _context.Add() method
            * and EntityFramwork will figure out where to stick it.
            */
            _context.Add(game);
            await _context.SaveChangesAsync(); // commits changes to DB.
        }
        public Task SetGameImageAsync(string userId, Uri uri)
        {
            throw new NotImplementedException();
        }

        public Task SetGameNameAsync(string userId, string gameName)
        {
            throw new NotImplementedException();
        }
    }
}
