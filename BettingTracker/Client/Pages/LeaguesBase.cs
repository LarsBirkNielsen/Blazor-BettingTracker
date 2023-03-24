using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace BettingTracker.Client.Pages;

public class LeaguesBase : ComponentBase
{
    [Inject]
    public ILeagueService LeagueService { get; set; }

    [Inject]
    public IManageLeaguesLocalStorageService ManageLeaguesLocalStorageService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public IEnumerable<LeagueDto> Leagues { get; set; }
    public string ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await ClearLocalStorage();

            Leagues = await ManageLeaguesLocalStorageService.GetCollection();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;

        }
    }

    protected void ShowLeague_Click(int id)
    {
        NavigationManager.NavigateTo($"league/{id}");
    }

    protected void CreateNewLeague_Click()
    {
        NavigationManager.NavigateTo("/league");
    }

    private async Task ClearLocalStorage()
    {
        await ManageLeaguesLocalStorageService.RemoveCollection();
    }

}
