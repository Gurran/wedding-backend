using Wedding.Web.Services;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add Key Vault to configuration
var keyVaultName = builder.Configuration["KeyVaultName"];
var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net");
https://weddingosabackend.vault.azure.net/

builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());

// Add services to the container.
builder.Services.AddControllers();

// Add the TableService as a singleton
string connectionString = builder.Configuration.GetConnectionString("AzureStorage");
builder.Services.AddSingleton(new TableService(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
