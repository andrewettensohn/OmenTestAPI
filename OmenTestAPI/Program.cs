using Microsoft.EntityFrameworkCore;
using OmenTestAPI;
using OmenTestAPI.Data;
using OmenTestAPI.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OmenContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IOmenRepository, OmenRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(Constants.AllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("https://localhost:5001")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(Constants.AllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();