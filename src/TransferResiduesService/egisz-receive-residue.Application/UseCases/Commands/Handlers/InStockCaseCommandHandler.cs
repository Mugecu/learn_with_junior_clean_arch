using egisz_receive_residue.Domain.Interfaces;
using egisz_receive_residue.Domain.Services;
using MediatR;

namespace egisz_receive_residue.Application.UseCases.Commands.Handlers
{
    /// <summary>
    /// Обработчик для команды InStockCaseCommand.
    /// </summary>
    /// <remarks>
    /// Конструктор обработчика.
    /// </remarks>
    /// <param name="stockRepository">Репозиторий остатков из БД рег. ФРЛЛО.</param>
    public class InStockCaseCommandHandler(IStockRepository stockRepository)
        : IRequestHandler<InStockCaseCommand, Unit>
    {
        private const bool TRUE_VALUE = true;

        /// <summary>
        /// Обработка случая обновления записей в stock и stock_document в БД рег. ФРЛЛО.
        /// </summary>
        /// <param name="request">Команда для обработки.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Единичный объект Unit, указывающий на успешное выполнение операции.</returns>
        public async Task<Unit> Handle(InStockCaseCommand request, CancellationToken cancellationToken)
        {
            if (!(request.StockNotInResidueEntities?.Any() ?? false))
            {
                return Unit.Value;
            }

            foreach (var stockEntity in request.StockNotInResidueEntities)
            {
                stockEntity.IsDelete = TRUE_VALUE;
                UpdateStockDocumentService.UpdateStockDocumentEntity(stockEntity.StockDocument, request.CurrentDateTime);
            }
            await stockRepository.SaveAsync();
            return Unit.Value;
        }
    }
}
