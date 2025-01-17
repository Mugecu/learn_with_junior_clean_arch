using egisz_receive_residue.Domain.Entities;
using MediatR;

namespace egisz_receive_residue.Application.UseCases.Commands
{
    /// <summary>
    /// Запрос для обработки данных в случаях, когда запись есть в БД сервиса остатков, но нет в БД рег. ФРЛЛО
    /// и residue_unloads.download_date > даты последней выгрузки.
    /// </summary>
    public class InResiduesCaseCommand : IRequest<Unit>
    {
        /// <summary>
        /// Список остатков, которых нет в БД рег.ФРЛЛО.
        /// </summary>
        public IEnumerable<Residue>? ResidueNotInStockEntities { get; set; }

        /// <summary>
        /// Дата и время вставки данных.
        /// </summary>
        public DateTime CurrentDateTime { get; set; }
    }
}
