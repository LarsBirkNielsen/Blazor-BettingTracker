using BettingTracker.Server.Data;
using BettingTracker.Server.Services.AuthService;
using BettingTracker.Server.Services.PredictionService;
using BettingTracker.Server.Services.TeamService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using BettingTracker.Server.Services.ImportService;
using BettingTracker.Server.Services.LeagueService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//My Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddScoped<IImportService, ImportService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();


//My Bearer token Service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:5235", "https://localhost:7235")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);


app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();



app.UseRouting();

//My Auth
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

app.Run();
