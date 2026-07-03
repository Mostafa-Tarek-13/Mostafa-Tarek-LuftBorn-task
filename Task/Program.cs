using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;
using Task.Repositories.Implementations;
using Task.Repositories.Interfaces;
using Task.Services.Implementations;
using Task.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    await SeedDatabaseAsync(dbContext);
}

app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();

static async System.Threading.Tasks.Task SeedDatabaseAsync(AppDbContext context)
{
    if (!await context.Users.AnyAsync())
    {
        context.Users.Add(new User
        {
            UserName = "demo_user",
            Email = "demo@example.com"
        });
    }

    if (!await context.Songs.AnyAsync())
    {
        context.Songs.Add(new Song
        {
            SongTitle = "Sample Song",
            Singer = "Demo Artist"
        });
    }

    if (context.ChangeTracker.HasChanges())
    {
        await context.SaveChangesAsync();
    }
}
