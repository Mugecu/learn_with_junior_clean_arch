using System.ComponentModel.DataAnnotations;

namespace egisz_receive_residue.Domain.Entities
{
    /// <summary>
    /// Сведения об остатках в БД сервиса остатков.
    /// </summary>
    public class Residue
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        public int StockExtId { get; set; }

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
        /// Количество потребительских упаковок медицинской продукции.
        /// </summary>
        public decimal PackQty { get; set; }

        /// <summary>
        /// Количество потребительских единиц медицинской продукции в потребительской единице измерения.
        /// </summary>
        public decimal ItemQty { get; set; }

        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Срок годности остатков медицинской продукции.
        /// </summary>
        public DateTime DrugExpirationDt { get; set; }

        /// <summary>
        /// Серия ЛП.
        /// </summary>
        public string? DrugSeria { get; set; }

        /// <summary>
        /// Код КЛП товарной позиции лекарственного препарата.
        /// </summary>
        [StringLength(10485760)]
        public string? EsklpKlpCode { get; set; }

        /// <summary>
        /// Код специализированного продукта лечебного питания.
        /// </summary>
        [StringLength(10485760)]
        public string? NutritionCode { get; set; }

        /// <summary>
        /// Код медицинского изделия.
        /// </summary>
        [StringLength(10485760)]
        public string? MedEquipCode { get; set; }

        /// <summary>
        /// Дата последней выгрузки.
        /// </summary>
        public DateTime DownloadDate { get; set; }
    }
}