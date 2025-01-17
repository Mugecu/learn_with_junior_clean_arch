using egisz_receive_residue.Domain.Base;

namespace egisz_receive_residue.Domain.Entities
{
    /// <summary>
    /// Документ с остатком в БД рег. ФРЛЛО.
    /// Таблица stock_document.
    /// </summary>
    public class StockDocument : IBaseEntity
    {
        /// <summary>
        /// Идентификатор записи. Первичный ключ.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор документа со сведениями.
        /// </summary>
        public required string DocumentId { get; set; }

        /// <summary>
        /// Дата и время выгрузки сведений.
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Признак выгрузки.
        /// </summary>
        public bool IsUpload { get; set; }

        /// <summary>
        /// Дата последнего изменения.
        /// </summary>
        public DateTime LastChangeDate { get; set; }

        /// <summary>
        /// Идентификатор регистровой записи в ФРЛЛО.
        /// </summary>
        public string? RegNum { get; set; }

        /// <summary>
        /// Источник загрузки сведений.
        /// </summary>
        public required string SourceSystem { get; set; }
    }
}