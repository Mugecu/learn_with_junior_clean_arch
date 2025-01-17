using egisz_receive_residue;
using egisz_receive_residue.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// ����������� ������������
builder.Services.AddControllers();

// ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ��������� Entity Framework Core ��� ������ � PostgreSQL
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PharmEgiszContext>(options => 
    options.UseNpgsql(configuration.GetConnectionString("PharmEgiszConnection")).UseSnakeCaseNamingConvention(), ServiceLifetime.Transient);

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PharmacyResidueContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("PharmacyResidueConnection")).UseSnakeCaseNamingConvention(), ServiceLifetime.Transient);

// ��������� ��������� ����������� ��������� ��� ��������� ����� � Npgsql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// ����������� MediatR � �������������� ������ �� Bootstrap
var assemblies = Bootstrap.AddAssembliesWithResiduesUseCases();
builder.Services.AddMediatR(assemblies, (options) => {
    options.AsTransient();
});

// ����������� ������������
builder.Services.AddRepositories();

// �������� ������������ � Bootstrap
Bootstrap.SetConfiguration(configuration);

// ��������� �����������
builder.Host.ConfigureLogging((loggingBuilder) =>
{
    var Logger = new LoggerConfiguration()
      .WriteTo.File(@$"..\Logs\log-{DateTime.Now:yyyy-MM-dd}.txt")
      .CreateLogger();

    loggingBuilder.AddSerilog(Logger);
});

// ����������� �������� ��������
builder.Services.AddHostedService<Worker>();

// ���������� ����������
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // ��������� Swagger UI ������ � ������ ����������
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ��������� HTTPS-���������
app.UseHttpsRedirection();

// ����������� ��������
app.UseAuthorization();

// ������������� ������������
app.MapControllers();

// ������ ����������
app.Run();