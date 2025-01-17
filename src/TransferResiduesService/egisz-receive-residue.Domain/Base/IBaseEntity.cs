namespace egisz_receive_residue.Domain.Base
{
    /// <summary>
    /// Базовый интерфейс для сущностей из БД рег. ФРЛЛО.
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор сущности в БД.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Источник загрузки сведений.
        /// </summary>
        public string SourceSystem { get; set; }
    }
}