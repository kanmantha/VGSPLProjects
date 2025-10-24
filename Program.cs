using System.IO;
using Microsoft.EntityFrameworkCore;
using RailwayReservation.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Determine a cross-platform data directory.
// If running in container the environment variable DATA_DIR can be set to /data
var dataDirEnv = Environment.GetEnvironmentVariable("DATA_DIR");
string dataDir = !string.IsNullOrWhiteSpace(dataDirEnv)
    ? dataDirEnv
    : Path.Combine(Directory.GetCurrentDirectory(), "data");

// ensure the folder exists and is writable
if (!Directory.Exists(dataDir))
{
    Directory.CreateDirectory(dataDir);
}

// build the sqlite file path
var dbFilePath = Path.Combine(dataDir, "railway.db");

// log for troubleshooting
builder.Logging.AddConsole();
var loggerFactory = LoggerFactory.Create(lb => lb.AddConsole());
var logger = loggerFactory.CreateLogger("Program");
logger.LogInformation("Using SQLite DB path: {dbPath}", dbFilePath);

// Connection string uses the computed path
var connectionString = $"Data Source={dbFilePath}";

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure DB is created & migrations (for demo, just ensure created)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
