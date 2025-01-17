using egisz_receive_residue.Domain.Entities;
using egisz_receive_residue.Domain.Entities.Services;
using egisz_receive_residue.Domain.Interfaces;
using egisz_receive_residue.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

namespace egisz_receive_residue.Application.UseCases.Commands.Handlers
{
    /// <summary>
    /// Обработчик для команды InResiduesCaseCommand.
    /// </summary>
    /// <remarks>
    /// Конструктор обработчика.
    /// </remarks>
    /// <param name="stockRepository">Репозиторий остатков из БД рег. ФРЛЛО.</param>
    /// <param name="logger">Логгер для записи сообщений.</param>
    public class InResiduesCaseCommandHandler(
        IStockRepository stockRepository,
        ILogger<InResiduesCaseCommand> logger)
        : IRequestHandler<InResiduesCaseCommand, Unit>
    {
        /// <summary>
        /// Обработка случая добавления записей в stock и stock_document в БД рег. ФРЛЛО.
        /// </summary>
        /// <param name="request">Команда для обработки.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Единичный объект Unit, указывающий на успешное выполнение операции.</returns>
        public async Task<Unit> Handle(InResiduesCaseCommand request, CancellationToken cancellationToken)
        {
            if (!(request.ResidueNotInStockEntities?.Any() ?? false))
            {
                return Unit.Value;
            }

            var addedEntities = Enumerable.Empty<Stock>();

            foreach (var residueEntity in request.ResidueNotInStockEntities)
            {
                var entity = AddStockService.AddStockEntity(
                    residueEntity,
                    AddStockDocumentService.AddStockDocumentEntity(request.CurrentDateTime),
                    request.CurrentDateTime,
                    stockRepository.GetRegion());

                var errors = new List<StringBuilder>();
                errors.AddRange(ValidationStockService.StockValidation(entity));
                errors.AddRange(ValidationStockDocumentService.StockDocumentValidation(entity.StockDocument));

                if (errors.Any())
                {
                    errors.Insert(0, new StringBuilder($"Запись с resiues.guid = '{entity.Guid}' содержит ошибки."));
                    string errorText = string.Join(" ", errors.Select(i => i));
                    logger.LogError(errorText);
                    continue;
                }

                addedEntities = addedEntities.Append(entity);
            }

            await stockRepository.AddStocksAsync(addedEntities);
            await stockRepository.SaveAsync();
            return Unit.Value;
        }
    }
}
