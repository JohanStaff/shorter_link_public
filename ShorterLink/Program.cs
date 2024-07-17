using Microsoft.AspNetCore.Components;
using ShorterLink;
using ShorterLink.Code.ActionFileters;
using ShorterLink.Code.DataObjects.Subscriptions;
using ShorterLink.Code.Devices;
using ShorterLink.Code.Events;
using ShorterLink.Code.Links;
using ShorterLink.Code.Links.LinkStats;
using ShorterLink.Code.Subscriptions;
using ShorterLink.Code.Subscriptions.Logic;
using ShorterLink.Code.Users.Crypto;
using ShorterLink.Code.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromDays(7);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

string connstr = builder.Configuration.GetConnectionString("DefaultConnection");
ArgumentException.ThrowIfNullOrEmpty(connstr);

try {
    var database = AppDatabase.Create(connstr);

    builder.Services.AddSingleton<AppDatabase>(database);
} catch {
    throw;
}

builder.Services.AddHostedService<BannedDomainsWorker>();

builder.Services.AddSingleton<IPasswordService, PasswordService>();
builder.Services.AddSingleton<DeviceService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<EventsService>();
builder.Services.AddSingleton<LinksService>();
builder.Services.AddSingleton<LinkStatsService>();
builder.Services.AddSingleton<LinksOverall>();
builder.Services.AddSingleton<ActualUsersSubscriptionsService>();
builder.Services.AddSingleton<SubscriptionsService>();
builder.Services.AddSingleton<UserSubscriptionService>();
builder.Services.AddScoped<UserSession>();
builder.Services.AddScoped<ISubscriptionLogic, InitialSubscriptionLogic>();

// Add services to the container.
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add<StartupFilterAttribute>();
    options.Filters.Add<CookieFilterAttribute>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "user",
    pattern: "{controller=User}/{action=Account}"
);
app.MapControllerRoute(
    name: "shorter",
    pattern: "shorter/{action}/{source}"
);

app.Run();
