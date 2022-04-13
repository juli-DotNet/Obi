using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Core.Services;
using ObiMenagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ObiManagementDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ObiManagementDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITrackBaseService, TrackBaseService>();
builder.Services.AddScoped<ITrackContainerService, TrackContainerService>();
builder.Services.AddScoped<IExpenseTypeService, ExpenseTypeService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRoadClientService, RoadClientService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IRoadDataService, RoadDataService>();
builder.Services.AddScoped<IRoadExpenseService, RoadExpenseService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();