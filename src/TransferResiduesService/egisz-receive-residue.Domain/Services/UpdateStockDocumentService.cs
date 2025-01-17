using egisz_receive_residue.Domain.Entities;

namespace egisz_receive_residue.Domain.Services
{
    /// <summary>
    /// Класс для обновления сущности <see cref="StockDocument"/>.
    /// </summary>
    public static class UpdateStockDocumentService
    {
        /// <summary>
        /// Обновить сущность <see cref="StockDocument"/> на основе данных из сервиса остатков.
        /// </summary>
        /// <param name="stockDocumentEntity">Данные из stock_document из БД рег. ФРЛЛО.</param>
        /// <param name="currentDateTime">Дата и время обновления данных.</param>
        /// <returns></returns>
        public static StockDocument UpdateStockDocumentEntity(StockDocument stockDocumentEntity, DateTime currentDateTime)
        {
            stockDocumentEntity.IsUpload = false;
            stockDocumentEntity.LastChangeDate = currentDateTime;
            return stockDocumentEntity;
        }
    }
}
