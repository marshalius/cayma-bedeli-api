using CaymaBedeliAPI.Models;
using CaymaBedeliAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<CancellationCalculator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.MapPost("api/cancellation/calculate", ([FromBody]CancellationRequest request, CancellationCalculator calculator) =>
{
    if(request.StartDate > request.EndDate)
    {
        return Results.BadRequest("Taahhüt baþlangýç tarihi bitiþ tarihinden büyük olamaz!");
    }
    var result = calculator.Calculate(request);
    return Results.Ok(result);
});

app.Run();
