using egisz_receive_residue.Application.UseCases.Queries;
using egisz_receive_residue.Domain.Entities;
using egisz_receive_residue.Domain.Interfaces;
using MediatR;

namespace egisz_receive_residue.Application.UseCases.Commands.Handlers
{
    /// <summary>
    /// Обработчик для команды TransferResiduesCommand.
    /// </summary>
    /// <remarks>
    /// Конструктор обработчика.
    /// </remarks>
    /// <param name="mediator">Медиатор для отправки команд.</param>
    /// <param name="stockRepository">Репозиторий остатков из БД рег. ФРЛЛО.</param>
    /// <param name="stockDocumentRepository">Репозиторий документов с остатком из БД рег. ФРЛЛО.</param>
    public class TransferResiduesCommandHandler(
        IMediator mediator,
        IStockRepository stockRepository,
        IStockDocumentRepository stockDocumentRepository)
        : IRequestHandler<TransferResiduesCommand, Unit>
    {
        /// <summary>
        /// Обработка команды передачи информации об остатках из БД сервиса остатков в БД рег. ФРЛЛО.
        /// </summary>
        /// <param name="request">Команда для обработки.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Единичный объект Unit, указывающий на успешное выполнение операции.</returns>
        public async Task<Unit> Handle(TransferResiduesCommand request, CancellationToken cancellationToken)
        {
            var currentDateTime = DateTime.Now;
            var residueEntities = await mediator.Send(new ResiduesDataQuery(), cancellationToken);
            var guidsFromResidueEntities = residueEntities.Select(x => x.Guid);
            var lastUnloadingDate = await stockDocumentRepository.GetLastUnloadingDateAsync();

            var afterLastUnloadingResidueEntities = residueEntities
                .Where(i => i.DownloadDate > lastUnloadingDate);

            var groupedStockInOrNotInResidueEntities = await stockRepository
                .GetByFilterAsync(guidsFromResidueEntities);

            var stockNotInResidueEntities = groupedStockInOrNotInResidueEntities
                .Where(i => i.Key == false)
                .FirstOrDefault()
                ?? Enumerable.Empty<Stock>();

            var stockIntersectingWithResidueEntities = groupedStockInOrNotInResidueEntities
                .Where(i => i.Key == true)
                .FirstOrDefault()
                ?? Enumerable.Empty<Stock>();

            await mediator.Send(new InStockCaseCommand
            {
                StockNotInResidueEntities = stockNotInResidueEntities,
                CurrentDateTime = currentDateTime
            }, cancellationToken);

            var residueIntersectingWithStockEntities = afterLastUnloadingResidueEntities
                .Where(i => stockIntersectingWithResidueEntities
                    .Select(x => x.Guid)
                    .Contains(i.Guid));

            await mediator.Send(new InStockAndResiduesCaseCommand
            {
                StockIntersectingWithResidueEntities = stockIntersectingWithResidueEntities,
                ResidueIntersectingWithStockEntities = residueIntersectingWithStockEntities,
                CurrentDateTime = currentDateTime
            }, cancellationToken);

            var guidsFromStockIntersectingWithResidueEntities = stockIntersectingWithResidueEntities.Select(i => i.Guid);
            var residueNotInStockEntities = afterLastUnloadingResidueEntities
                .Where(i => guidsFromStockIntersectingWithResidueEntities != null
                    && !guidsFromStockIntersectingWithResidueEntities.Contains(i.Guid));

            await mediator.Send(new InResiduesCaseCommand
            {
                ResidueNotInStockEntities = residueNotInStockEntities,
                CurrentDateTime = currentDateTime
            }, cancellationToken);

            await stockRepository.SaveAsync();
            return Unit.Value;
        }
    }
}