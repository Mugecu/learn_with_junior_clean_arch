using egisz_receive_residue.Domain.Entities;
using egisz_receive_residue.Domain.Interfaces;
using MediatR;

namespace egisz_receive_residue.Application.UseCases.Queries
{
    /// <summary>
    /// Запрос для получения списка данных из репозитория <see cref="IResidueRepository"/>.
    /// </summary>
    public class ResiduesDataQuery : IRequest<IEnumerable<Residue>> { }
}