using BettingTracker.Client;
using BettingTracker.Client.Services.AuthService;
using BettingTracker.Client.Services.DashboardService;
using BettingTracker.Client.Services.ExcelService;
using BettingTracker.Client.Services.Import;
using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Client.Services.PredictionService;
using BettingTracker.Client.Services.TeamService;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tewr.Blazor.FileReader;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//My Services
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IDataService, DataService>();


//My LocalStorage Services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IManageTeamsLocalStorageService, ManageTeamsLocalStorageService>();
builder.Services.AddScoped<IManagePredictionsLocalStorageService, ManagePredictionsLocalStorageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IImportService, ImportService>();
builder.Services.AddScoped<IExcelService, ExcelService>();

builder.Services.AddFileReaderService(options => options.InitializeOnFirstCall = true);


builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();




await builder.Build().RunAsync();
