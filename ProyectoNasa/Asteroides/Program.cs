using Asteroides.Mappers;
using Asteroides.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAsterorideService, AsteroideService>();

IConfiguration configuration = new ConfigurationBuilder()
                                       .AddJsonFile("appsettings.json")
                                       .Build();

builder.Services.AddHttpClient("AsteroideService", asteroide =>
{
    asteroide.BaseAddress = new Uri(configuration["ConnectionString:UrlApiNasa"]);
});

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Automapper
//builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Creaci�n del Mapper
var automapper = new MapperConfiguration(item => item.AddProfile(new AsteroideProfile()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
