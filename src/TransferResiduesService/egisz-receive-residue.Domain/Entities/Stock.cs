using egisz_receive_residue.Domain.Base;

namespace egisz_receive_residue.Domain.Entities
{
    /// <summary>
    /// Остаток в БД рег. ФРЛЛО.
    /// Таблица stock.
    /// </summary>
    public class Stock : IBaseEntity
    {
        /// <summary>
        /// Идентификатор записи. Первичный ключ.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Признак необходимости удаления остатка в ФРЛЛО.
        /// </summary>
        public bool? IsDelete { get; set; }

        /// <summary>
        /// Идентификатор сведений об остатках в системе ФРЛЛО.
        /// </summary>
        public string? StockId { get; set; }

        /// <summary>
        /// Идентификатор сведений об остатках в системе-источнике сведений.
        /// </summary>
        public string? StockExtId { get; set; }

        /// <summary>
        /// OID медицинской или фармацевтической организации.
        /// </summary>
        public string? MedOrgOid { get; set; }

        /// <summary>
        /// ОГРН ИП, на складе которого находятся остатки.
        /// </summary>
        public string? SeNum { get; set; }

        /// <summary>
        /// Наименование ИП, на складе которого находятся остатки.
        /// </summary>
        public string? SeName { get; set; }

        /// <summary>
        /// Код ОКАТО медицинской или фармацевтической организация.
        /// </summary>
        public required string Region { get; set; }

        /// <summary>
        /// Дата, на которую сформированы сведения об остатках.
        /// </summary>
        public required string StockDate { get; set; }

        /// <summary>
        /// Код КЛП товарной позиции лекарственного препарата.
        /// </summary>
        public string? DrugKlpCode { get; set; }

        /// <summary>
        /// Код медицинского изделия.
        /// </summary>
        public string? MedEquipCode { get; set; }

        /// <summary>
        /// Код специализированного продукта лечебного питания.
        /// </summary>
        public string? NutritionCode { get; set; }

        /// <summary>
        /// Количество потребительских упаковок медицинской продукции.
        /// </summary>
        public decimal PackQty { get; set; }

        /// <summary>
        /// Количество потребительских единиц медицинской продукции в потребительской единице измерения.
        /// </summary>
        public decimal ItemQty { get; set; }

        /// <summary>
        /// Срок годности остатков медицинской продукции.
        /// </summary>
        public DateTime DrugExpirationDt { get; set; }

        /// <summary>
        /// Серия ЛП.
        /// </summary>
        public string? DrugSeria { get; set; }

        /// <summary>
        /// Уникальный идентификатор в системе-источнике.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Документ с остатком. Внешний ключ с stock_document.id.
        /// </summary>
        public int StockDocumentId { get; set; }

        /// <summary>
        /// Источник загрузки сведений.
        /// </summary>
        public required string SourceSystem { get; set; }

        /// <summary>
        /// Ссылка на свзяанный StockDocument.
        /// </summary>
        public required StockDocument StockDocument { get; set; }
    }
}