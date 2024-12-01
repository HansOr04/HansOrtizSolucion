using HansOrtizContactosAPI.Controllers;
using HansOrtizContactosAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<HansOrtizContactosContextContext>(builder.Configuration.GetConnectionString("BurgerConnection"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHO_ContactoEndpoints();

app.Run();
