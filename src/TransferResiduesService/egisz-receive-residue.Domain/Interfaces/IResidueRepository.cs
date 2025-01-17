using egisz_receive_residue.Domain.Entities;

namespace egisz_receive_residue.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для чтения данных типа <see cref="Residue"/> из БД сервиса остатков.
    /// </summary>
    public interface IResidueRepository
    {
        /// <summary>
        /// Собрать данные об остатках из БД сервиса остатков.
        /// </summary>
        /// <returns>Список объектов <see cref="Residue"/>.</returns>
        Task<IEnumerable<Residue>> CollectDataFromResiduesServiceAsync();
    }
}