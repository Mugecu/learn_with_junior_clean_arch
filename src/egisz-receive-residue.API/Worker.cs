using egisz_receive_residue.Application.UseCases.Commands;
using MediatR;

namespace egisz_receive_residue
{
    /// <summary>
    /// Сервис для выполнения периодических задач по передаче информации об остатках.
    /// </summary>
    /// <remarks>
    /// Конструктор класса Worker.
    /// </remarks>
    /// <param name="logger">Логгер для записи сообщений.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <param name="serviceScopeFactory">Фабрика для создания области службы.</param>
    public class Worker(ILogger<Worker> logger,
        IConfiguration configuration,
        IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        /// <summary>
        /// Логгер для записи сообщений.
        /// </summary>
        private readonly ILogger<Worker> _logger = logger;

        /// <summary>
        /// Конфигурация приложения.
        /// </summary>
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Фабрика для создания области службы.
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        /// <summary>
        /// Метод для асинхронного выполнения работы сервиса.
        /// </summary>
        /// <param name="stoppingToken">Токен отмены для отслеживания состояния операции.</param>
        /// <returns>Задача, представляющая результат выполнения.</returns>
        /// <exception cref="Exception">Сбрасывается, если введено некорректное время для выполнения задачи.</exception>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            int.TryParse(_configuration.GetSection("ResedueServiceExecuteIntervalInDays").Value, out int timer);

            if (timer == 0)
            {
                var errorText = "Введено некорректное время.";

                _logger.LogError(errorText);
                throw new Exception(errorText);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await mediator.Send(new TransferResiduesCommand());

                await Task.Delay(TimeSpan.FromDays(timer), stoppingToken);
            }
        }
    }
}
