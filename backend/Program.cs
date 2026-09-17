using LendingPlatform.Data;
using LendingPlatform.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<LendingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LendingDatabase")));
builder.Services.AddScoped<LoanDecisionService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Creates the local SQLite database on the first run. Migrations would replace this in a larger app.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LendingDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program;
