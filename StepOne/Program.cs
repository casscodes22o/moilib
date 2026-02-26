using Microsoft.EntityFrameworkCore;
using StepOne.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// added this too much to comprehend
builder.Services.AddDbContext<BookAPIContext>(optionsAction => 
    optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// after I did all this i download tools from NuGet
// went to console entered these commands
// Add-Migration "book model added" (Migraion in Migration folder) 
// Update-Database (after this I went to SQL server and checked the database, the table was created with the correct columns and data types)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
