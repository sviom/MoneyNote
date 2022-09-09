using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary5.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(x => x.WithOrigins("https://localhost:44327", "https://moneynote.azurewebsites.net")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddScoped<MoneyContext>();
builder.Services.AddDbContext<MoneyContext>(options =>
                    options.UseSqlServer(AzureKeyVault.OnGetAsync(KeyVaultName.MoneyNoteConnectionString).Result),
                    ServiceLifetime.Scoped,
                    ServiceLifetime.Scoped);

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

app.UseAuthorization();

app.UseCors();

app.MapRazorPages();

app.MapControllers();

app.Run();