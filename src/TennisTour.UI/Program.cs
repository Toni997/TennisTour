﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TennisTour.Core.Enums;
using TennisTour.Core.Helpers;
using TennisTour.UI;
using TennisTour.UI.AuthProviders;
using TennisTour.UI.Common;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(ApiConstants.BaseUrl) });
builder.Services.AddAuthorizationCore();

builder.Services.AddMudServices();
builder.Services.AddScoped<AuthenticationStateProvider, UiAuthStateProvider>();
builder.Services.AddScoped<TennisRules>();

await builder.Build().RunAsync();
