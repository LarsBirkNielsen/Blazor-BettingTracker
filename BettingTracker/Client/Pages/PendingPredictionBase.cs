using BettingTracker.Client.Enums;
using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Client.Services.PredictionService;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace BettingTracker.Client.Pages
{
    public class PendingPredictionsBase : ComponentBase
    {
        [Inject]
        public IPredictionService PredictionService { get; set; }

        [Inject]
        public IManagePredictionsLocalStorageService ManagePredictionsLocalStorageService { get; set; }
        public IManageTeamsLocalStorageService ManageLeaguesLocalStorageService { get; set; }
        [Inject]
        public ILeagueService LeagueService { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<PredictionDto> Predictions { get; set; }
        public string ErrorMessage { get; set; }
        private string returnUrl = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ClearLocalStorage();

                var predictionList = await ManagePredictionsLocalStorageService.GetCollection();
                Predictions = GetPendingPredictions(predictionList);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }
        }
        private IEnumerable<PredictionDto> GetPendingPredictions(IEnumerable<PredictionDto> predictionList)
        {
            DateTime now = DateTime.Now;
            List<PredictionDto> pendingPredictions = predictionList.Where(x =>
                x.Status.Equals("Pending") && x.KickOff < now).ToList();
            return pendingPredictions;
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
        protected async Task DeletePredictionClick(int id)
        {
            await PredictionService.DeletePrediction(id);
            var deletedPrediction = Predictions.FirstOrDefault(p => p.Id == id);
            if (deletedPrediction != null)
            {
                var predictionsList = Predictions.ToList();
                predictionsList.Remove(deletedPrediction);
                Predictions = predictionsList;
            }
            NavigationManager.NavigateTo("/predictions");
        }
    }
}
