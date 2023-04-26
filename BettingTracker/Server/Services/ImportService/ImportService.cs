using BettingTracker.Models.Dtos;
using BettingTracker.Server.Data;
using BettingTracker.Server.Entities;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace BettingTracker.Server.Services.ImportService
{
    public class ImportService : IImportService
    {
        private readonly DataContext _context;

        public ImportService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<League>> ReadLeaguesAndTeamsXlsxFileAsync(Stream fileStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var leagues = new List<League>();

            using (var excelPackage = new ExcelPackage(fileStream))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var leagueName = worksheet.Cells[row, 1].Value.ToString().Trim();
                    var countryName = worksheet.Cells[row, 2].Value.ToString().Trim();
                    var teamName = worksheet.Cells[row, 3].Value.ToString().Trim();

                    // Check if the league already exists in the list
                    var league = leagues.FirstOrDefault(l => l.Name == leagueName);

                    if (league == null)
                    {
                        league = new League
                        {
                            Name = leagueName,
                            Country = countryName,
                            Teams = new List<Team>()
                        };
                        leagues.Add(league);
                    }

                    var team = new Team { Name = teamName };
                    league.Teams.Add(team);
                }
            }

            return leagues;
        }

        public async Task<List<MatchResultDto>> ReadMatchResultsXlsxFileAsync(Stream fileStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var matchResults = new List<MatchResultDto>();

            using (var excelPackage = new ExcelPackage(fileStream))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var homeTeam = worksheet.Cells[row, 2].Value.ToString().Trim();
                    var awayTeam = worksheet.Cells[row, 3].Value.ToString().Trim();
                    var homeGoals = int.Parse(worksheet.Cells[row, 4].Value.ToString().Trim());
                    var awayGoals = int.Parse(worksheet.Cells[row, 5].Value.ToString().Trim());
                    var homeOdds = worksheet.Cells[row, 6].Value.ToString().Trim();
                    var drawOdds = worksheet.Cells[row, 7].Value.ToString().Trim();
                    var awayOdds = worksheet.Cells[row, 8].Value.ToString().Trim();
                    var homeDrawOdds = worksheet.Cells[row, 9].Value.ToString().Trim();
                    var awayDrawOdds = worksheet.Cells[row, 10].Value.ToString().Trim();


                    matchResults.Add(new MatchResultDto
                    {
                        HomeTeam = homeTeam,
                        AwayTeam = awayTeam,
                        HomeGoals = homeGoals,
                        AwayGoals = awayGoals,
                        HomeOdds = homeOdds,
                        DrawOdds = drawOdds,
                        AwayOdds = awayOdds,
                        HomeDrawOdds = homeDrawOdds,
                        AwayDrawOdds = awayDrawOdds,
                        
                    });
                }
            }

            return matchResults;
        }

        public async Task<List<LeagueDto>> SaveLeaguesAsync(List<LeagueDto> leagueDtos)
        {
            var savedLeagueDtos = new List<LeagueDto>();

            foreach (var leagueDto in leagueDtos)
            {
                // Check if a league with the same name already exists in the database
                var existingLeague = await _context.Leagues.Include(l => l.Teams).FirstOrDefaultAsync(l => l.Name == leagueDto.Name);

                if (existingLeague == null)
                {
                    var newLeague = new League
                    {
                        Name = leagueDto.Name,
                        Country = leagueDto.Country,
                        Teams = leagueDto.Teams.Select(t => new Team
                        {
                            Name = t.Name,
                            IsCurrentInLeague = true
                        }).ToList()
                    };

                    await _context.Leagues.AddAsync(newLeague);
                    await _context.SaveChangesAsync();

                    savedLeagueDtos.Add(new LeagueDto
                    {
                        Id = newLeague.Id,
                        Name = newLeague.Name,
                        Country = newLeague.Country,
                        Teams = newLeague.Teams.Select(t => new TeamDto
                        {
                            Id = t.Id,
                            Name = t.Name,
                            IsCurrentInLeague = t.IsCurrentInLeague
                        }).ToList()
                    });
                }
                else
                {
                    // Mark teams that are no longer part of the league
                    foreach (var team in existingLeague.Teams)
                    {
                        if (!leagueDto.Teams.Any(t => t.Name == team.Name))
                        {
                            team.IsCurrentInLeague = false;
                        }
                    }

                    // Add teams from the imported league to the existing league
                    foreach (var teamDto in leagueDto.Teams)
                    {
                        // Check if a team with the same name already exists in the existing league
                        var existingTeam = existingLeague.Teams.FirstOrDefault(t => t.Name == teamDto.Name);

                        if (existingTeam == null)
                        {
                            var newTeam = new Team
                            {
                                Name = teamDto.Name,
                                LeagueId = existingLeague.Id,
                                IsCurrentInLeague = true
                            };

                            await _context.Teams.AddAsync(newTeam);
                        }
                        else
                        {
                            existingTeam.Name = teamDto.Name;
                            existingTeam.IsCurrentInLeague = true;
                        }
                    }

                    await _context.SaveChangesAsync();

                    savedLeagueDtos.Add(new LeagueDto
                    {
                        Id = existingLeague.Id,
                        Name = existingLeague.Name,
                        Country = existingLeague.Country,
                        Teams = existingLeague.Teams.Where(t => t.IsCurrentInLeague).Select(t => new TeamDto
                        {
                            Id = t.Id,
                            Name = t.Name,
                            IsCurrentInLeague = t.IsCurrentInLeague
                        }).ToList()
                    });
                }
            }

            return savedLeagueDtos;
        }

        public async Task<List<PredictionDto>> UpdatePendingPredictionsAsync(List<PredictionDto> pendingPredictions, List<MatchResultDto> matchResults)
        {
            foreach (var prediction in pendingPredictions)
            {
                var matchResult = matchResults.FirstOrDefault(matchResult => matchResult.HomeTeam == prediction.HomeTeam && matchResult.AwayTeam == prediction.AwayTeam);

                if (matchResult != null)
                {
                    bool homeWin = matchResult.HomeGoals > matchResult.AwayGoals;
                    bool awayWin = matchResult.AwayGoals > matchResult.HomeGoals;
                    bool draw = matchResult.HomeGoals == matchResult.AwayGoals;

                    decimal odds;

                    if (prediction.Tip == "1")
                    {
                        odds = decimal.Parse(matchResult.HomeOdds);
                        if (homeWin)
                        {
                            prediction.Status = "Won";
                        }
                        else
                        {
                            prediction.Status = "Lost";
                        }
                    }
                    else if (prediction.Tip == "1x")
                    {
                        odds = decimal.Parse(matchResult.HomeDrawOdds);
                        if (homeWin || draw)
                        {
                            prediction.Status = "Won";
                        }
                        else
                        {
                            prediction.Status = "Lost";
                        }
                    }
                    else if (prediction.Tip == "2")
                    {
                        odds = decimal.Parse(matchResult.AwayOdds);
                        if (awayWin)
                        {
                            prediction.Status = "Won";
                        }
                        else
                        {
                            prediction.Status = "Lost";
                        }
                    }
                    else if (prediction.Tip == "x2")
                    {
                        odds = decimal.Parse(matchResult.AwayDrawOdds);
                        if (awayWin || draw)
                        {
                            prediction.Status = "Won";
                        }
                        else
                        {
                            prediction.Status = "Lost";
                        }
                    }
                    else if (prediction.Tip == "x")
                    {
                        odds = decimal.Parse(matchResult.DrawOdds);
                        if (draw)
                        {
                            prediction.Status = "Won";
                        }
                        else
                        {
                            prediction.Status = "Lost";
                        }
                    }
                    else
                    {
                        odds = 0;
                        prediction.Status = "Lost";
                    }

                    decimal.TryParse(prediction.Stake, out decimal stake);

                    if (prediction.Status == "Won")
                    {
                        prediction.Profit = (odds * stake) - stake;
                    }
                    else // Lost
                    {
                        prediction.Profit = stake * -1;
                    }
                    prediction.Odds = odds.ToString();

                    var predictionEntity = await _context.Predictions.FindAsync(prediction.Id);
                    if (predictionEntity != null)
                    {
                        predictionEntity.Status = prediction.Status;
                        predictionEntity.Profit = prediction.Profit;
                        predictionEntity.Odds = prediction.Odds;
                        _context.Predictions.Update(predictionEntity);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return pendingPredictions;
        }
    }
}

