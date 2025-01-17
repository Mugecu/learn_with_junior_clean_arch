using System.Text;

namespace egisz_receive_residue.Domain.Entities.Services
{
    /// <summary>
    /// Валидация сущности <see cref="Residue"/>.
    /// </summary>
    public static class ValidationResidueService
    {
        /// <summary>
        /// Провалидировать <see cref="Residue"/>.
        /// </summary>
        /// <param name="residueEntity">Сущность <see cref="Residue"/>.</param>
        /// <returns>Список ошибок.</returns>
        public static List<StringBuilder> ResiduesDataValidation(Residue residueEntity)
        {
            List<StringBuilder> errors = new();

            if (!string.IsNullOrWhiteSpace(residueEntity.StockExtId.ToString())
                && residueEntity.StockExtId.ToString().Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.id."));
            }

            if (string.IsNullOrWhiteSpace(residueEntity.MedOrgOid)
                && string.IsNullOrWhiteSpace(residueEntity.SeNum)
                && string.IsNullOrWhiteSpace(residueEntity.SeName))
            {
                errors.Add(new StringBuilder(
                    $"Отсутствует значение в subdivisions.nsi_oid, organizations.ogrn, subdivisions.name."));
            }
            else if (string.IsNullOrWhiteSpace(residueEntity.MedOrgOid)
                && (string.IsNullOrWhiteSpace(residueEntity.SeNum) || string.IsNullOrWhiteSpace(residueEntity.SeName)))
            {
                errors.Add(new StringBuilder($"Отсутствует значение в organizations.ogrn, subdivisions.name."));
            }

            if (!string.IsNullOrWhiteSpace(residueEntity.MedOrgOid) && residueEntity.MedOrgOid.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в subdivisions.nsi_oid."));
            }
            if (!string.IsNullOrWhiteSpace(residueEntity.SeNum) && residueEntity.SeNum.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в organizations.ogrn."));
            }
            if (!string.IsNullOrWhiteSpace(residueEntity.SeName) && residueEntity.SeName.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в subdivisions.name."));
            }

            if (residueEntity.PackQty <= 0)
            {
                errors.Add(new StringBuilder($"Отсутствует или неверное значение в residues.count."));
            }
            if (residueEntity.ItemQty <= 0)
            {
                errors.Add(new StringBuilder($"Отсутствует или неверное значение в residues.pack_num."));
            }

            if (residueEntity.Guid == Guid.Empty)
            {
                errors.Add(new StringBuilder($"Отсутствует значение в residues.guid."));
            }

            if (residueEntity.DrugExpirationDt != DateTime.MinValue
                && residueEntity.DrugExpirationDt < new DateTime(2020, 01, 01))
            {
                errors.Add(new StringBuilder($"Некорректное значение в residues.series_expiration_date."));
            }

            if (!string.IsNullOrWhiteSpace(residueEntity.DrugSeria) && residueEntity.DrugSeria.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.series_num."));
            }

            if (string.IsNullOrWhiteSpace(residueEntity.EsklpKlpCode)
                && string.IsNullOrWhiteSpace(residueEntity.MedEquipCode)
                && string.IsNullOrWhiteSpace(residueEntity.NutritionCode))
            {
                errors.Add(new StringBuilder(
                    $"Отсутствуют значения в residues.medication_type, residues.esklp_klp_code."));
            }
            if (!string.IsNullOrWhiteSpace(residueEntity.EsklpKlpCode) && residueEntity.EsklpKlpCode.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.esklp_klp_code."));
            }
            if (!string.IsNullOrWhiteSpace(residueEntity.MedEquipCode) && residueEntity.MedEquipCode.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.esklp_klp_code."));
            }
            if (!string.IsNullOrWhiteSpace(residueEntity.NutritionCode) && residueEntity.NutritionCode.Length > 100)
            {
                errors.Add(new StringBuilder($"Длина строки превышает 100 символов в residues.esklp_klp_code."));
            }
            return errors;
        }
    }
}
