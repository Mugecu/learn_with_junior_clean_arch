using egisz_receive_residue.Domain.Entities;

namespace egisz_receive_residue.Domain.Services
{
    /// <summary>
    /// Класс для обновления сущности <see cref="Stock"/>.
    /// </summary>
    public static class UpdateStockService
    {
        /// <summary>
        /// Обновить сущность <see cref="Stock"/> на основе данных из сервиса остатков.
        /// </summary>
        /// <param name="residueEntity">Данные из сервиса остатков.</param>
        /// <param name="stockEntity">Данные из stock из БД рег. ФРЛЛО.</param>
        /// <param name="currentDateTime">Дата и время обновления данных.</param>
        public static void UpdateStockEntity(Residue residueEntity, Stock stockEntity, DateTime currentDateTime)
        {
            stockEntity.MedOrgOid = residueEntity.MedOrgOid;
            stockEntity.SeNum = residueEntity.SeNum;
            stockEntity.SeName = residueEntity.SeName;
            stockEntity.StockDate = currentDateTime.ToString("MM.yyyy");
            stockEntity.DrugKlpCode = residueEntity.EsklpKlpCode;
            stockEntity.MedEquipCode = residueEntity.MedEquipCode;
            stockEntity.NutritionCode = residueEntity.NutritionCode;
            stockEntity.PackQty = residueEntity.PackQty;
            stockEntity.ItemQty = residueEntity.ItemQty;
            stockEntity.DrugExpirationDt = residueEntity.DrugExpirationDt;
            stockEntity.DrugSeria = residueEntity.DrugSeria;
            stockEntity.StockDocument = UpdateStockDocumentService.UpdateStockDocumentEntity(stockEntity.StockDocument, currentDateTime);
        }
    }
}
