using BettingTracker.Client.Services.AuthService;
using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Client.Services.PredictionService;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace BettingTracker.Client.Pages
{
    public class HighScoreBase : ComponentBase
    {
        [Inject]
        public IPredictionService PredictionService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }

        [Inject]
        public IManagePredictionsLocalStorageService ManagePredictionsLocalStorageService { get; set; }
        public IManageTeamsLocalStorageService ManageLeaguesLocalStorageService { get; set; }
        [Inject]
        public ILeagueService LeagueService { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IList<UserDto> Predictions { get; set; }
        public string ErrorMessage { get; set; }

        protected string CurrentUserEmail { get; set; }
        public int? CurrentUserPosition { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ClearLocalStorage();
                var allUsersByProfit = await PredictionService.GetTopUsersByProfit();
                Predictions = allUsersByProfit.Take(10).ToList();
                CurrentUserEmail = await AuthService.GetUserEmail();

                // Calculate current user position
                CurrentUserPosition = allUsersByProfit.FindIndex(u => u.Email.Equals(CurrentUserEmail, StringComparison.OrdinalIgnoreCase)) + 1;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }
        }

        protected void CreateNewPrediction_Click()
        {
            NavigationManager.NavigateTo("/prediction");
        }

        private async Task ClearLocalStorage()
        {
            await ManagePredictionsLocalStorageService.RemoveCollection();
        }
    }
}
