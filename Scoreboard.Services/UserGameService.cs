﻿using Microsoft.EntityFrameworkCore;
using Scoreboards.Data;
using Scoreboards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Updates UserGame data
 */
namespace Scoreboards.Services
{
    public class UserGameService : IUserGame
    {
        private readonly ApplicationDbContext _context;
        public UserGameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserGame> GetAll()
        {
            return _context.UserGames.Include(game => game.GamePlayed);
        }

        public IEnumerable<UserGame> GetLatest(int numberOfGames)
        {
            return GetAll()
                .OrderByDescending(userGame => userGame.GamePlayedOn)
                .Take(numberOfGames);
        }
        public int getWinsById(string userId)
        {
            return GetAll().Where(userGame => (userGame.User_01_Id == userId || userGame.User_02_Id == userId) && userGame.Winner == userId).Count();
        }
        public int getLosesById(string userId)
        {
            return GetAll().Where(userGame => (userGame.User_01_Id == userId || userGame.User_02_Id == userId) && userGame.Winner != userId && userGame.Winner.ToLower() != "draw").Count();
        }
        public decimal getRatioWithId(string userId)
        {
            int wins = getWinsById(userId);
            int loses = getLosesById(userId);
            int total = wins + loses;
            if (total == 0)
            {
                return 0;
            } else
            {
                decimal ratio = (decimal)wins / (decimal)(total) * 100;
                return Math.Round(ratio, 2);
            }
        }
        public UserGame GetById(int userGameId)
        {
            return GetAll().FirstOrDefault(userGame => userGame.Id == userGameId);
        }

        public async Task AddUserGameAsync(UserGame userGame)
        {
            /**
            * Entity framwork handls all logic for us
            * all we need to do is to call _context.Add() method
            * and EntityFramwork will figure out where to stick it.
            */
            _context.Add(userGame);
            await _context.SaveChangesAsync(); // commits changes to DB.
        }

        public async Task Delete(int userGameId)
        {
            var userGame = GetById(userGameId);

            _context.Remove(userGame);
            await _context.SaveChangesAsync(); // commits changes to DB.
        }

        public async Task EditUserGame(UserGame newUserGameContent)
        {
            _context.Entry(newUserGameContent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public Task SetGameImageAsync(int userGameId, Uri uri)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserGame> GetGamesByGameName(string gameName)
        {
            return _context.UserGames
                .Include(uGame => uGame.GamePlayed)
                .Where(game => game.GamePlayed.GameName == gameName);
        }
    }
}
