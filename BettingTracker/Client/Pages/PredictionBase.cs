using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Client.Services.PredictionService;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace BettingTracker.Client.Pages
{
    public class PredictionBase : ComponentBase
    {
        [Inject]
        public IPredictionService PredictionService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILeagueService LeagueService { get; set; }
        [Inject]
        public IManageLeaguesLocalStorageService ManageLeaguesLocalStorageService { get; set; }


        [Parameter]
        public int? Id { get; set; }

        public string btnText { get; set; } = string.Empty;

        public PredictionDto Prediction = new();
        public IEnumerable<LeagueDto> Leagues { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id == null)
            {
                btnText = "Create Prediction";
            }
            else
            {
                btnText = "Update Prediction";
            }
            // Call the LeagueService to get the list of leagues
            //This way we are always sure the List will get initialized before it is used in the component's markup.
            if (Leagues == null)
            {
                Leagues = new List<LeagueDto>();
            }
            Leagues = await LeagueService.GetLeagues();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id is not null)
            {
                var result = await PredictionService.GetPredictionById((int)Id);
                if (result is not null)
                {
                    Prediction = result;
                }
                else
                {
                    NavigationManager.NavigateTo("/prediction");
                }
            }
        }

        protected async Task HandleSubmit()
        {
            if (Id is null)
            {
                await PredictionService.CreatePrediction(Prediction);
                // Clear the input fields by resetting the League property to a new instance
                Prediction = new();
            }
            else
            {
                await PredictionService.UpdatePrediction((int)Id, Prediction);
                NavigationManager.NavigateTo("/predictions");
            }
        }

        protected async Task DeletePrediction()
        {
            await PredictionService.DeletePrediction(Prediction.Id);
            NavigationManager.NavigateTo("/predictions");
        }

        protected void HandleSeeListClick()
        {
            NavigationManager.NavigateTo("/predictions");
        }
    }
}