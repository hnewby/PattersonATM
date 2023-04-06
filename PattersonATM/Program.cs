using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PattersonATM;
using PattersonATM.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddControllers();
builder.Services.AddDbContext<ATMDbContext>();
builder.Services.AddScoped<AppData>();
builder.Services.AddHttpClient("LocalApi", client => client.BaseAddress = new Uri("https://localhost:7055/"));

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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.MapControllers(); // the order is important, this ensures API takes precedence.
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
});

app.Run();

