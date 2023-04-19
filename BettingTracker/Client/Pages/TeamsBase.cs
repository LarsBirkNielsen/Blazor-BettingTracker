using BettingTracker.Client.Services.TeamService;
using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace BettingTracker.Client.Pages;

public class TeamsBase : ComponentBase
{
    [Inject]
    public ITeamService TeamService { get; set; }

    [Inject]
    public ILeagueService LeagueService { get; set; }

    [Inject]
    public IManageTeamsLocalStorageService ManageTeamsLocalStorageService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public IEnumerable<TeamDto> Teams { get; set; }
    public IEnumerable<LeagueDto> Leagues { get; set; }
    public string ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await ClearLocalStorage();

            Teams = await ManageTeamsLocalStorageService.GetCollection();
            Leagues = await LeagueService.GetLeagues();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    protected void ShowTeam_Click(int id)
    {
        NavigationManager.NavigateTo($"team/{id}");
    }

    protected void CreateNewTeam_Click()
    {
        NavigationManager.NavigateTo("/team");
    }

    private async Task ClearLocalStorage()
    {
        await ManageTeamsLocalStorageService.RemoveCollection();
    }
}