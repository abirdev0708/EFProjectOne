using System;
using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using Microsoft.AspNetCore.Builder;
using TaskTrackerApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//     {
//         Title = "EFprojectOne",
//         Version = "v1"
//     });
// });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/tasks", async (AppDbContext db) =>
    await db.Tasks.ToListAsync());

app.MapPost("/tasks", async (AppDbContext db, TaskItem task) =>
{
    db.Tasks.Add(task);
    await db.SaveChangesAsync();
    return Results.Created($"/tasks/{task.Id}", task);
});

app.MapPut("/tasks/{id}/complete", async (AppDbContext db, int id) =>
{
    var task = await db.Tasks.FindAsync(id);
    if (task is null) return Results.NotFound();
    task.IsComplete = true;
    await db.SaveChangesAsync();
    return Results.Ok(task);
});

app.Run();