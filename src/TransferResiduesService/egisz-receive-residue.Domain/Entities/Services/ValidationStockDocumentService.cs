using System.Text;

namespace egisz_receive_residue.Domain.Entities.Services
{
    /// <summary>
    /// Валидация сущности <see cref="StockDocument"/>.
    /// </summary>
    public static class ValidationStockDocumentService
    {
        /// <summary>
        /// Провалидировать <see cref="StockDocument"/>.
        /// </summary>
        /// <param name="stockDocumentEntity">Сущность <see cref="StockDocument"/>.</param>
        /// <returns>Список ошибок.</returns>
        public static List<StringBuilder> StockDocumentValidation(StockDocument stockDocumentEntity)
        {
            List<StringBuilder> errors = new();

            if (string.IsNullOrWhiteSpace(stockDocumentEntity.DocumentId))
            {
                errors.Add(new StringBuilder($"Отсутствует значение в {nameof(stockDocumentEntity.DocumentId)}."));
            }
            if (!string.IsNullOrWhiteSpace(stockDocumentEntity.DocumentId)
                && stockDocumentEntity.DocumentId.Length > 100)
            {
                errors.Add(new StringBuilder(
                    $"Длина строки превышает 100 символов в {nameof(stockDocumentEntity.DocumentId)}."));
            }
            if (stockDocumentEntity.DocDate == DateTime.MinValue)
            {
                errors.Add(new StringBuilder($"Отсутствует значение в {nameof(stockDocumentEntity.DocDate)}."));
            }

            return errors;
        }
    }
}
