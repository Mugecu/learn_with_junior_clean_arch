using egisz_receive_residue;
using egisz_receive_residue.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Регистрация контроллеров
builder.Services.AddControllers();

// Документация API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Настройка Entity Framework Core для работы с PostgreSQL
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PharmEgiszContext>(options => 
    options.UseNpgsql(configuration.GetConnectionString("PharmEgiszConnection")).UseSnakeCaseNamingConvention(), ServiceLifetime.Transient);

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PharmacyResidueContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("PharmacyResidueConnection")).UseSnakeCaseNamingConvention(), ServiceLifetime.Transient);

// Включение поддержки устаревшего поведения для временных меток в Npgsql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Регистрация MediatR с использованием сборок из Bootstrap
var assemblies = Bootstrap.AddAssembliesWithResiduesUseCases();
builder.Services.AddMediatR(assemblies, (options) => {
    options.AsTransient();
});

// Регистрация репозиториев
builder.Services.AddRepositories();

// Передача конфигурации в Bootstrap
Bootstrap.SetConfiguration(configuration);

// Настройка логирования
builder.Host.ConfigureLogging((loggingBuilder) =>
{
    var Logger = new LoggerConfiguration()
      .WriteTo.File(@$"..\Logs\log-{DateTime.Now:yyyy-MM-dd}.txt")
      .CreateLogger();

    loggingBuilder.AddSerilog(Logger);
});

// Регистрация рабочего процесса
builder.Services.AddHostedService<Worker>();

// Построение приложения
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Включение Swagger UI только в режиме разработки
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Обработка HTTPS-редиректа
app.UseHttpsRedirection();

// Авторизация запросов
app.UseAuthorization();

// Маршрутизация контроллеров
app.MapControllers();

// Запуск приложения
app.Run();