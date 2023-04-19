using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Client.Services.PredictionService;
using BettingTracker.Client.Services.TeamService;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public ITeamService TeamService { get; set; }


        [Inject]
        public IManageTeamsLocalStorageService ManageLeaguesLocalStorageService { get; set; }
        [Inject]
        public IManagePredictionsLocalStorageService ManagePredictionsLocalStorageService { get; set; }

        [Parameter]
        public int? Id { get; set; }

        public string btnText { get; set; } = string.Empty;

        public PredictionDto Prediction = new();
        public IEnumerable<LeagueDto> Leagues { get; set; }
        public IEnumerable<string> TipOptions { get; } = new List<string>
        {
            "1",
            "1x",
            "x",
            "x2",
            "2"
        };
        public List<string> HomeTeams { get; set; } = new();
        public List<string> AwayTeams { get; set; } = new();
        public List<string> HomeTeamsOriginal { get; set; } = new();
        public List<string> AwayTeamsOriginal { get; set; } = new();
        protected bool ShowValidationMessage { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();

            if (Id == null)
            {
                Prediction.KickOff = DateTime.Now;
                btnText = "Create Prediction";
            }
            else
            {
                btnText = "Update Prediction";
            }

            if (Leagues == null)
            {
                Leagues = new List<LeagueDto>();
            }
            // Call the LeagueService to get the list of leagues
            //This way we are always sure the List will get initialized before it is used in the component's markup.
            Leagues = (await LeagueService.GetLeagues()).OrderBy(l => l.Name);

            // Load teams for the initially selected league
            if (Prediction.LeagueId == 0 && Leagues.Any())
            {
                Prediction.LeagueId = Leagues.First().Id;
            }
            await LoadTeams(Prediction.LeagueId);

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
            if (Prediction.KickOff > DateTime.Now)
            {
                ShowValidationMessage = false;
                if (Id is null)
                {
                    ReplaceDotsWithCommas(Prediction); // replace dots with commas
                    await PredictionService.CreatePrediction(Prediction);
                    // Clear the input fields by resetting the League property to a new instance
                    Prediction = new();
                }
                else
                {
                    ReplaceDotsWithCommas(Prediction); // replace dots with commas
                    await PredictionService.UpdatePrediction((int)Id, Prediction);
                    NavigationManager.NavigateTo("/predictions");
                }
            }
            else
            {
                ShowValidationMessage = true;
            }
        }

        private void ReplaceDotsWithCommas(PredictionDto prediction)
        {
            prediction.Odds = prediction.Odds.Replace(".", ",");
            prediction.Stake = prediction.Stake.Replace(".", ",");
        }

        protected async Task DeletePrediction()
        {
            await PredictionService.DeletePrediction(Prediction.Id);
            NavigationManager.NavigateTo("/predictions");
        }

        protected void CancelClick()
        {
            NavigationManager.NavigateTo("/predictions");
        }
        private List<string> ExtractTeamNames(ICollection<TeamDto> teams)
        {
            return teams.Select(t => t.Name).ToList();
        }

        protected async Task LoadTeams(int leagueId)
        {
            var teams = await TeamService.GetTeamsByLeagueId(leagueId);
            HomeTeams = teams.Select(t => t.Name).OrderBy(t => t).ToList();
            AwayTeams = teams.Select(t => t.Name).OrderBy(t => t).ToList();
            HomeTeamsOriginal = HomeTeams.ToList();
            AwayTeamsOriginal = AwayTeams.ToList();
        }
        protected async Task OnLeagueChanged(ChangeEventArgs e)
        {
            int selectedLeagueId = int.Parse(e.Value.ToString());
            await LoadTeams(selectedLeagueId);
        }

        protected void OnHomeTeamChanged(ChangeEventArgs e)
        {
            Prediction.HomeTeam = e.Value.ToString();
            Console.WriteLine(Prediction.HomeTeam);
            AwayTeams = AwayTeamsOriginal.Where(t => t != Prediction.HomeTeam).ToList();
        }

        protected void OnAwayTeamChanged(ChangeEventArgs e)
        {
            Prediction.AwayTeam = e.Value.ToString();
            HomeTeams = HomeTeamsOriginal.Where(t => t != Prediction.AwayTeam).ToList();
        }
    }
}
