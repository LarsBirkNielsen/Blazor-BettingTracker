using BettingTracker.Client.Services.PredictionService;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace BettingTracker.Client.Pages
{
    public class PredictionsBase : ComponentBase
    {
        [Inject]
        public IPredictionService PredictionService { get; set; }

        [Inject]
        public IManagePredictionsLocalStorageService ManagePredictionsLocalStorageService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<PredictionDto> Predictions { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ClearLocalStorage();

                Predictions = await ManagePredictionsLocalStorageService.GetCollection();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }
        }

        protected void ShowPrediction_Click(int id)
        {
            NavigationManager.NavigateTo($"prediction/{id}");
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
