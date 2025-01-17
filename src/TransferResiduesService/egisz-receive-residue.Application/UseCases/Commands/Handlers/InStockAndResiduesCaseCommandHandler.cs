using egisz_receive_residue.Domain.Entities.Services;
using egisz_receive_residue.Domain.Interfaces;
using egisz_receive_residue.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

namespace egisz_receive_residue.Application.UseCases.Commands.Handlers
{
    /// <summary>
    /// Обработчик для команды InStockAndResiduesCaseCommand.
    /// </summary>
    /// <remarks>
    /// Конструктор обработчика.
    /// </remarks>
    /// <param name="stockRepository">Репозиторий остатков из БД рег. ФРЛЛО.</param>
    /// <param name="logger">Логгер для записи сообщений.</param>
    public class InStockAndResiduesCaseCommandHandler(
        IStockRepository stockRepository,
        ILogger<InStockAndResiduesCaseCommand> logger)
        : IRequestHandler<InStockAndResiduesCaseCommand, Unit>
    {
        /// <summary>
        /// Обработка случая обновления записей в stock и stock_document в БД рег. ФРЛЛО.
        /// </summary>
        /// <param name="request">Команда для обработки.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Единичный объект Unit, указывающий на успешное выполнение операции.</returns>
        public async Task<Unit> Handle(InStockAndResiduesCaseCommand request, CancellationToken cancellationToken)
        {
            if (!(request.StockIntersectingWithResidueEntities?.Any() ?? false)
                || !(request.ResidueIntersectingWithStockEntities?.Any() ?? false))
            {
                return Unit.Value;
            }

            foreach (var stockEntity in request.StockIntersectingWithResidueEntities)
            {
                var residueEntity = request.ResidueIntersectingWithStockEntities
                    .Where(i => i.Guid == stockEntity.Guid)
                    .FirstOrDefault();
                if (residueEntity == null)
                {
                    continue;
                }

                var errors = new List<StringBuilder>();
                errors.AddRange(ValidationResidueService.ResiduesDataValidation(residueEntity));
                if (errors.Any())
                {
                    errors.Insert(0, new StringBuilder(
                        $"Запись с stock.guid = '{residueEntity.Guid}' содержит ошибки."));
                    string errorText = string.Join(" ", errors.Select(i => i));
                    logger.LogError(errorText);
                    continue;
                }

                // Обновление stock_document происходит внутри UpdateStockEntity
                UpdateStockService.UpdateStockEntity(residueEntity, stockEntity, request.CurrentDateTime);
            }
            await stockRepository.SaveAsync();
            return Unit.Value;
        }
    }
}
