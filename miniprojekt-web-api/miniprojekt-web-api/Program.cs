using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using miniprojekt_web_api.Data;
using miniprojekt_web_api.Service;
using Shared.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Allow = "Allow";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Allow, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();

    });
});

builder.Services.AddDbContext<PostContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));
/*
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
*/

builder.Services.AddScoped<DataService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData();

    // Configure the HTTP request pipeline.
    /*
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    */
    app.UseHttpsRedirection();
    app.UseCors(Allow);

    app.Use(async (context, next) =>
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        await next(context);
    });


    //app.UseAuthorization();

    //app.MapControllers();

    app.MapGet("/", (DataService service) =>
    {
        return new { message = "velkommen til tråden!" };
    });

    app.MapGet("/api/Post", (DataService service) =>
    {
        return service.GetPosts();
    });

    app.MapGet("/api/Post/{id}", (DataService service, int id) =>
    {
        return service.GetPost(id);
    });

    app.MapGet("/api/Post{id}/comment", (DataService service, int id) =>
    {
        return service.GetComments(id);
    });

    app.MapGet("/api/Post{postId}/comment/{commentId}", (DataService service, int postId, int commentId) =>
    {
        return service.GetComment(postId, commentId);
    });


    app.MapPost("/api/Post{postId}/comments", (DataService service, NewPoComment data, int postId) =>
    {
        string result = service.CreateComment(data.Text, data.DateTime, data.Name, data.Upvote, data.Downvote, postId);
        return new { message = result };
    });

}
    app.Run();

record NewPoComment(string Text, DateTime DateTime, string Name, int Upvote, int Downvote, int postId);

