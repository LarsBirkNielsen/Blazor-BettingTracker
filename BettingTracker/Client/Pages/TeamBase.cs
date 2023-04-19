using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Client.Services.TeamService;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BettingTracker.Client.Pages;

public class TeamBase : ComponentBase
{
    [Inject]
    public ITeamService TeamService { get; set; }

    [Inject]
    public ILeagueService LeagueService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Parameter]
    public int? Id { get; set; }

    public List<LeagueDto> Leagues { get; set; } = new();
    public string btnText { get; set; } = string.Empty;

    public TeamDto Team = new();

    protected override async Task OnInitializedAsync()
    {

        Leagues = ( await LeagueService.GetLeagues() ).ToList();
        if (Id == null)
        {
            btnText = "Create Team";
        }
        else
        {
            btnText = "Update Team";
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (Id is not null)
        {
            var result = await TeamService.GetTeamById((int)Id);
            if (result is not null)
                Team = result;
            else
                NavigationManager.NavigateTo("/team");
        }
    }

    protected async Task HandleSubmit()
    {
        if (Id is null)
        {
            await TeamService.CreateTeam(Team);
            // Clear the input fields by resetting the Team property to a new instance
            Team = new TeamDto();
        }
        else
        {
            await TeamService.UpdateTeam((int)Id, Team);
            NavigationManager.NavigateTo("/teams");
        }
    }

    protected async Task DeleteTeam()
    {
        await TeamService.DeleteTeam(Team.Id);
        NavigationManager.NavigateTo("/teams");
    }

    protected void HandleSeeListClick()
    {
        NavigationManager.NavigateTo("/teams");
    }
}
