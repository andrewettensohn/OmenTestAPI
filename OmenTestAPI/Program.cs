using OmenTestAPI;
using OmenTestAPI.Data;
using OmenTestAPI.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
builder.Configuration.AddJsonFile("C:\\APPLICATIONS\\Configuration\\Global.json");

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOmenRepository, OmenRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(Constants.AllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("http://omentestui.jessepecar.com")
            .AllowAnyMethod()
            .AllowAnyHeader();

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
