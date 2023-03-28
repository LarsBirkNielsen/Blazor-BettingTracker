using BettingTracker.Models.Dtos;
using BettingTracker.Server.Data;
using BettingTracker.Server.Entities;
using BettingTracker.Server.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BettingTracker.Server.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly DataContext _context;

        public TeamService(DataContext context)
        {
            _context = context;
        }

        public async Task<Team> CreateTeam(TeamDto teamDto)
        {
            var existingTeam = await _context.Leagues
                .SingleOrDefaultAsync(league => league.Id == teamDto.Id);

            if (existingTeam != null)
            {
                throw new ArgumentException("A team with the same ID already exists", nameof(teamDto));
            }

            var league = new Team
            {
                Id = teamDto.Id,
                Name = teamDto.Name,
                Country = leagueDto.Country
            };

            await _context.Leagues.AddAsync(league);
            await _context.SaveChangesAsync();

            return league;
        }

        public async Task<bool> DeleteLeague(int leagueId)
        {
            var dbLeague = await GetLeagueById(leagueId);
            if (dbLeague == null)
            {
                return false;
            }
            else
            {
                _context.Remove(dbLeague);
                await _context.SaveChangesAsync();
                return true;
            }
        }


        public async Task<League> GetLeagueById(int leagueId)
        {
            var dbLeague = await _context.Leagues.FindAsync(leagueId);

            if (dbLeague != null)
            {
                return dbLeague;
            }

            return null;
        }

        public async Task<List<League>> GetLeagues()
        {
            return await _context.Leagues.ToListAsync();
        }

        public async Task<League> UpdateLeague(int id, LeagueDto updatedLeague)
        {
            try
            {
                var leagueToUpdate = await _context.Leagues.FindAsync(id);
                if (leagueToUpdate == null)
                {
                    throw new Exception($"League with id {id} not found.");
                }

                leagueToUpdate.Name = updatedLeague.Name;
                leagueToUpdate.Country = updatedLeague.Country;

                _context.Entry(leagueToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return leagueToUpdate;
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }
    }
}
