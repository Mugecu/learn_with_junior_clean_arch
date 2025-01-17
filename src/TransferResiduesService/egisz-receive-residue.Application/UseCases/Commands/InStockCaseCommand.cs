using egisz_receive_residue.Domain.Entities;
using MediatR;

namespace egisz_receive_residue.Application.UseCases.Commands
{
    /// <summary>
    /// Запрос для обработки данных в случаях, когда запись есть в БД рег. ФРЛЛО, но нет в БД сервиса остатков.
    /// </summary>
    public class InStockCaseCommand : IRequest<Unit>
    {
        /// <summary>
        /// Список остатков, которых нет в БД сервиса остатков.
        /// </summary>
        public IEnumerable<Stock>? StockNotInResidueEntities { get; set; }

        /// <summary>
        /// Дата и время обновления данных.
        /// </summary>
        public DateTime CurrentDateTime { get; set; }
    }
}