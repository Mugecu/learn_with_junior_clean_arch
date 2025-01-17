using System.Globalization;
using System.Text;

namespace egisz_receive_residue.Domain.Entities.Services
{
    /// <summary>
    /// Валидация сущности <see cref="Stock"/>.
    /// </summary>
    public static class ValidationStockService
    {
        /// <summary>
        /// Провалидировать <see cref="Stock"/>.
        /// </summary>
        /// <param name="stockEntity">Сущность <see cref="Stock"/>.</param>
        /// <returns>Список ошибок.</returns>
        public static List<StringBuilder> StockValidation(Stock stockEntity)
        {
            List<StringBuilder> errors = new();

            if (!string.IsNullOrWhiteSpace(stockEntity.StockId) && stockEntity.StockId.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в {nameof(stockEntity.StockId)}."));
            }
            if (!string.IsNullOrWhiteSpace(stockEntity.StockExtId) && stockEntity.StockExtId.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.id."));
            }

            if (string.IsNullOrWhiteSpace(stockEntity.MedOrgOid)
                && string.IsNullOrWhiteSpace(stockEntity.SeNum)
                && string.IsNullOrWhiteSpace(stockEntity.SeName))
            {
                errors.Add(new StringBuilder(
                    $"Отсутствует значение в subdivisions.nsi_oid, organizations.ogrn, subdivisions.name."));
            }
            else if (string.IsNullOrWhiteSpace(stockEntity.MedOrgOid)
                && (string.IsNullOrWhiteSpace(stockEntity.SeNum) || string.IsNullOrWhiteSpace(stockEntity.SeName)))
            {
                errors.Add(new StringBuilder($"Отсутствует значение в organizations.ogrn, subdivisions.name."));
            }

            if (!string.IsNullOrWhiteSpace(stockEntity.MedOrgOid) && stockEntity.MedOrgOid.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в subdivisions.nsi_oid."));
            }
            if (!string.IsNullOrWhiteSpace(stockEntity.SeNum) && stockEntity.SeNum.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в organizations.ogrn."));
            }
            if (!string.IsNullOrWhiteSpace(stockEntity.SeName) && stockEntity.SeName.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в subdivisions.name."));
            }

            if (string.IsNullOrWhiteSpace(stockEntity.Region))
            {
                errors.Add(new StringBuilder($"Отсутствует значение в {nameof(stockEntity.Region)}."));
            }
            if (!string.IsNullOrWhiteSpace(stockEntity.Region) && stockEntity.Region.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в {nameof(stockEntity.Region)}."));
            }

            if (string.IsNullOrWhiteSpace(stockEntity.StockDate) 
                || !DateTime.TryParseExact(
                    stockEntity.StockDate,
                    "MM.yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _))
            {
                errors.Add(new StringBuilder($"Отсутствует значение в {nameof(stockEntity.StockDate)}."));
            }
            if (DateTime.ParseExact(
                    stockEntity.StockDate,
                    "MM.yyyy",
                    CultureInfo.InvariantCulture
                    ) < new DateTime(2020, 01, 01))
            {
                errors.Add(new StringBuilder($"Некорректное значение в {nameof(stockEntity.StockDate)}."));
            }

            if (string.IsNullOrWhiteSpace(stockEntity.DrugKlpCode)
                && string.IsNullOrWhiteSpace(stockEntity.MedEquipCode)
                && string.IsNullOrWhiteSpace(stockEntity.NutritionCode))
            {
                errors.Add(new StringBuilder(
                    $"Отсутствуют значения в residues.medication_type, residues.esklp_klp_code."));
            }
            if (!string.IsNullOrWhiteSpace(stockEntity.DrugKlpCode) && stockEntity.DrugKlpCode.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.esklp_klp_code."));
            }
            if (!string.IsNullOrWhiteSpace(stockEntity.MedEquipCode) && stockEntity.MedEquipCode.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.esklp_klp_code."));
            }
            if (!string.IsNullOrWhiteSpace(stockEntity.NutritionCode) && stockEntity.NutritionCode.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.esklp_klp_code."));
            }

            if (stockEntity.PackQty <= 0)
            {
                errors.Add(new StringBuilder($"Отсутствует или неверное значение в residues.count."));
            }
            if (stockEntity.ItemQty <= 0)
            {
                errors.Add(new StringBuilder($"Отсутствует или неверное значение в residues.pack_num."));
            }

            if (stockEntity.DrugExpirationDt != DateTime.MinValue
                && stockEntity.DrugExpirationDt < new DateTime(2020, 01, 01))
            {
                errors.Add(new StringBuilder($"Некорректное значение в residues.series_expiration_date."));
            }

            if (!string.IsNullOrWhiteSpace(stockEntity.DrugSeria) && stockEntity.DrugSeria.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.series_num."));
            }
            return errors;
        }
    }
}
