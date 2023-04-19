using BettingTracker.Models.Dtos;
using BettingTracker.Server.Data;
using BettingTracker.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingTracker.Server.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly DataContext _context;

        public TeamService(DataContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Team> CreateTeamAsync(TeamDto teamDto)
        {
            var team = new Team
            {
                Name = teamDto.Name,
                LeagueId = teamDto.LeagueId
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return team;
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);

            if (team == null)
            {
                return false;
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Team> GetTeamById(int teamId)
        {
            return await _context.Teams.FindAsync(teamId);
        }

        public async Task<List<Team>> GetTeamsAsync(int? leagueId = null)
        {
            IQueryable<Team> teams = _context.Teams.Include(team => team.League);

            if (leagueId.HasValue)
            {
                teams = teams.Where(team => team.LeagueId == leagueId.Value);
            }
            return await teams.ToListAsync();
            //else
            //{
            //    return await _context.Teams.ToListAsync();
            //}
        }

        public async Task<List<Team>> GetTeamsByLeagueId(int leagueId)
        {
            return await _context.Teams.Where(t => t.LeagueId == leagueId && t.IsCurrentInLeague == true).ToListAsync();
        }

        public async Task<Team> UpdateTeamAsync(int teamId, TeamDto teamDto)
        {
            var team = await _context.Teams.FindAsync(teamId);

            if (team == null)
            {
                return null;
            }

            team.Name = teamDto.Name;

            await _context.SaveChangesAsync();

            return team;
        }
    }
}
