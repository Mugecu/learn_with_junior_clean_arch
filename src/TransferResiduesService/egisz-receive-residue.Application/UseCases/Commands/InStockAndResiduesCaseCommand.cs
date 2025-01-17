using egisz_receive_residue.Domain.Entities;
using MediatR;

namespace egisz_receive_residue.Application.UseCases.Commands
{
    /// <summary>
    /// Запрос для обработки данных в случаях, когда запись есть и в БД сервиса остатков, и в БД рег. ФРЛЛО
    /// и residue_unloads.download_date > даты последней выгрузки.
    /// </summary>
    public class InStockAndResiduesCaseCommand : IRequest<Unit>
    {
        /// <summary>
        /// Список остатков из БД рег. ФРЛЛО, которые есть в БД сервиса остатков.
        /// </summary>
        public IEnumerable<Stock>? StockIntersectingWithResidueEntities { get; set; }

        /// <summary>
        /// Список остатков из БД сервиса остатков, которые есть в БД рег. ФРЛЛО.
        /// </summary>
        public IEnumerable<Residue>? ResidueIntersectingWithStockEntities { get; set; }

        /// <summary>
        /// Дата и время обновления данных.
        /// </summary>
        public DateTime CurrentDateTime { get; set; }
    }
}
