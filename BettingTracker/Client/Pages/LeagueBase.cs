using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BettingTracker.Client.Pages;

public class LeagueBase : ComponentBase
{

    [Inject]
    public ILeagueService LeagueService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Parameter]
    public int? Id { get; set; }

    public string btnText { get; set; } = string.Empty;

    public LeagueDto League = new();

    protected override void OnInitialized()
    {
        if (Id == null)
        {
            btnText = "Create League";
        }
        else
        {
            btnText = "Update League";
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (Id is not null)
        {
            var result = await LeagueService.GetLeagueById((int)Id);
            if (result is not null)
                League = result;
            else
                NavigationManager.NavigateTo("/league");
        }
    }

    protected async Task HandleSubmit()
    {
        if (Id is null)
        {
            await LeagueService.CreateLeague(League);
            // Clear the input fields by resetting the League property to a new instance
            League = new LeagueDto();
        }
        else
        {
            await LeagueService.UpdateLeague((int)Id, League);
            NavigationManager.NavigateTo("/leagues");
        }
    }

    protected async Task DeleteLeague()
    {
        await LeagueService.DeleteLeague(League.Id);
        NavigationManager.NavigateTo("/leagues");
    }

    protected void HandleSeeListClick()
    {
        NavigationManager.NavigateTo("/leagues");
    }




}
