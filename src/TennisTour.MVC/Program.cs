using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using TennisTour.Application.Common.Email;
using TennisTour.Application.Models.Validators;
using TennisTour.Application.Services;
using TennisTour.Application.Services.Impl;
using TennisTour.Core.Enums;
using TennisTour.MVC.AuthProviders;
using TennisTour.MVC.Controllers;
using TennisTour.MVC.Data;
using TennisTour.MVC.Filters;
using TennisTour.Shared.Services.Impl;
using TennisTour.Shared.Services;
using Microsoft.AspNetCore.Identity;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<DatabaseContext>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddScoped<AuthenticationStateProvider, UiAuthStateProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UsersController>();

builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<SmtpSettings>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
