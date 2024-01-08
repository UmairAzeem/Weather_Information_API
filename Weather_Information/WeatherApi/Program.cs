using BusinessLogic.BLService;
using BusinessLogic.I_BLService;
using DataAccessEF.DbEfModels;
using ExternalServices.OpenWeatherMap;
using Microsoft.EntityFrameworkCore;
using WeatherApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString(configuration["ConnectionStrings:Connectionstring"]);
builder.Services.AddDbContext<DbWeatherConditionsContext>(x => x.UseSqlServer(connectionString));

//Register DI Service
builder.Services.AddTransient<OpenWeatherMapApi>();
builder.Services.AddTransient<I_CountryInfo, CountryInfo_Service>();
builder.Services.AddTransient<I_CityInfo, CityInfo_Service>();
builder.Services.AddTransient<I_Weather, Weather_Service>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlerMiddleware();

app.UseCors("AllowCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
