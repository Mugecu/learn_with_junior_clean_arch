using egisz_receive_residue.Domain.Entities;

namespace egisz_receive_residue.Domain.Services
{
    /// <summary>
    /// Класс для создания сущности <see cref="Stock"/>.
    /// </summary>
    public static class AddStockService
    {
        /// <summary>
        /// Источник загрузки сведений.
        /// </summary>
        private const string SOURCE_VALUE = "DocumentStorage";

        /// <summary>
        /// Создать сущность <see cref="Stock"/> на основе данных из сервиса остатков.
        /// </summary>
        /// <param name="residueEntity">Данные из сервиса остатков.</param>
        /// <param name="stockDocumentEntity">Связанная со <see cref="Stock"/> сущность StockDocument.</param>
        /// <param name="currentDateTime">Дата и время вставки данных.</param>
        /// <param name="codOkato">Код ОКАТО из конфигурации.</param>
        /// <returns>Сущность <see cref="Stock"/>.</returns>
        public static Stock AddStockEntity(Residue residueEntity, StockDocument stockDocumentEntity, DateTime currentDateTime, string codOkato)
            => new Stock
            {
                IsDelete = false,
                StockId = null,
                StockExtId = residueEntity.StockExtId.ToString(),
                MedOrgOid = residueEntity.MedOrgOid,
                SeNum = residueEntity.SeNum,
                SeName = residueEntity.SeName,
                Region = codOkato,
                StockDate = currentDateTime.ToString("MM.yyyy"),
                DrugKlpCode = residueEntity.EsklpKlpCode,
                MedEquipCode = residueEntity.MedEquipCode,
                NutritionCode = residueEntity.NutritionCode,
                PackQty = residueEntity.PackQty,
                ItemQty = residueEntity.ItemQty,
                DrugExpirationDt = residueEntity.DrugExpirationDt,
                DrugSeria = residueEntity.DrugSeria,
                Guid = residueEntity.Guid,
                StockDocumentId = stockDocumentEntity.Id,
                SourceSystem = SOURCE_VALUE,
                StockDocument = stockDocumentEntity,
            };
    }
}
