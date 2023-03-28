using BettingTracker.Client;
using BettingTracker.Client.Services.AuthService;
using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Client.Services.PredictionService;
using BlazorEcommerce.Client;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//My Services
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IPredictionService, PredictionService>();


//My LocalStorage Services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IManageLeaguesLocalStorageService, ManageLeaguesLocalStorageService>();
builder.Services.AddScoped<IManagePredictionsLocalStorageService, ManagePredictionsLocalStorageService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();




await builder.Build().RunAsync();
