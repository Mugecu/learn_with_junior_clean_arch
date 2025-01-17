using egisz_receive_residue.Domain.Entities;
using egisz_receive_residue.Domain.Interfaces;
using MediatR;

namespace egisz_receive_residue.Application.UseCases.Queries.Handlers
{
    /// <summary>
    /// Обработчик запроса <see cref="ResiduesDataQuery"/>.
    /// </summary>
    /// <remarks>
    /// Конструктор обработчика <see cref="ResiduesDataQuery"/>.
    /// </remarks>
    /// <param name="residueRepository">Репозиторий данных <see cref="IResidueRepository"/>.</param>
    public class ResiduesDataQueryHandler(IResidueRepository residueRepository)
        : IRequestHandler<ResiduesDataQuery, IEnumerable<Residue>>
    {
        /// <summary>
        /// Обработать запрос на получение списка данных из <see cref="IResidueRepository"/>.
        /// </summary>
        /// <param name="request">Запрос на получение данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список объектов <see cref="Residue"/> с данными.</returns>
        public async Task<IEnumerable<Residue>> Handle(ResiduesDataQuery request, CancellationToken cancellationToken)
        {
            return await residueRepository.CollectDataFromResiduesServiceAsync();
        }
    }
}
