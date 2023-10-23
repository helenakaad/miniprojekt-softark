using Microsoft.EntityFrameworkCore;
using Data;
using Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<PostContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.
/*
builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed data hvis nødvendigt.
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // Fylder data på, hvis databasen er tom. Ellers ikke.
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);

// Middlware der kører før hver request. Sætter ContentType for alle responses til "JSON".
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});

// DataService fås via "Dependency Injection" (DI)
app.MapGet("/", (DataService service) =>
{
    return new { message = "Hello World!" };
});

app.MapGet("/api/posts", (DataService service) =>
{
    return service.GetPosts().Select(b => new {
        postId = b.PostId,
        user = b.User,
        title = b.Title,
        content = b.Content,
        votes = b.Votes,
        comments = b.Comments
    });
});

app.MapGet("/api/posts/{id}", (DataService service, int id) =>
{
    return service.GetPost(id);
});

app.MapPost("/api/posts/{id}/comments", (DataService service, NewCommentData data, int id) =>
{
    string result = service.CreateComment(data.Text, data.User, id);
    return new { message = result };
});

app.MapPut("/api/posts/{pid}/comments/{cid}/{vote}", (DataService service, int pid, int cid, int vote) =>
{
    service.VoteComment(pid, cid, vote);
});

app.MapPost("/api/posts", (DataService service, NewPostData data) =>
{
    string result = service.CreatePost(data.Text, data.User, data.Content);
    return new { message = result };
});

app.MapPut("/api/posts/{pid}/{vote}", (DataService service, int pid, int vote) =>
{
    service.VotePost(pid, vote);
});

app.UseAuthorization();

app.MapControllers();

app.Run();

record NewCommentData(string Text, string User);
record NewPostData(string Text, string User, string Content);

