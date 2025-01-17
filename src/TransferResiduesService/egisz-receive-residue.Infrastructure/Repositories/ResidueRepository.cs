using egisz_receive_residue.Domain.Entities;
using egisz_receive_residue.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Reflection;


namespace egisz_receive_residue.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для чтения данных типа <see cref="Residue"/> из БД сервиса остатков.
    /// </summary>
    /// <remarks>
    /// Конструктор репозитория.
    /// </remarks>
    /// <param name="pharmacyResidueContext">Контекст данных БД сервиса остатков.</param>
    internal class ResidueRepository(PharmacyResidueContext pharmacyResidueContext) : IResidueRepository
    {
        private readonly PharmacyResidueContext _pharmacyResidueContext = pharmacyResidueContext;

        /// <summary>
        /// Словарь для маппинга столбцов из запроса к БД сервиса остатков.
        /// </summary>
        private static readonly Dictionary<string, PropertyInfo?> _columnMapping
            = new Dictionary<string, PropertyInfo?>
            {
                {"stock_ext_id", typeof(Residue).GetProperty("StockExtId")},
                {"med_org_oid", typeof(Residue).GetProperty("MedOrgOid")},
                {"se_num", typeof(Residue).GetProperty("SeNum")},
                {"se_name", typeof(Residue).GetProperty("SeName")},
                {"pack_qty", typeof(Residue).GetProperty("PackQty")},
                {"item_qty", typeof(Residue).GetProperty("ItemQty")},
                {"guid", typeof(Residue).GetProperty("Guid")},
                {"drug_expiration_dt", typeof(Residue).GetProperty("DrugExpirationDt")},
                {"drug_seria", typeof(Residue).GetProperty("DrugSeria")},
                {"esklp_klp_code", typeof(Residue).GetProperty("EsklpKlpCode")},
                {"nutrition_code", typeof(Residue).GetProperty("NutritionCode")},
                {"med_equip_code", typeof(Residue).GetProperty("MedEquipCode")},
                {"download_date", typeof(Residue).GetProperty("DownloadDate")},
            };

        /// <summary>
        /// Собрать данные об остатках из БД сервиса остатков.
        /// </summary>
        /// <returns>Список объектов <see cref="Residue"/>.</returns>
        public async Task<IEnumerable<Residue>> CollectDataFromResiduesServiceAsync()
        {
            var data = Enumerable.Empty<Residue>();
            string? connectionString = _pharmacyResidueContext.Database.GetConnectionString();
            string sqlExpression = "select res.id as stock_ext_id," +
                "                          s.nsi_oid as med_org_oid," +
                "                          o.ogrn as se_num," +
                "                          s.name as se_name," +
                "                          res.count as pack_qty," +
                "                          res.pack_num as item_qty," +
                "                          res.guid as guid," +
                "                          res.series_expiration_date as drug_expiration_dt," +
                "                          res.series_num as drug_seria," +
                "                          case when res.medication_type = 0 then res.esklp_klp_code else null end as esklp_klp_code," +
                "                          case when res.medication_type = 1 then res.esklp_klp_code else null end as nutrition_code," +
                "                          case when res.medication_type = 2 then res.esklp_klp_code else null end as med_equip_code," +
                "                          ru.download_date as download_date" +
                "                     from residues res" +
                "                          join residue_unloads ru on res.residue_unload_id = ru.id" +
                "                          join subdivisions s on s.id = ru.subdivision_id" +
                "                          join organizations o on o.id = s.organization_id";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                NpgsqlCommand command = new NpgsqlCommand(sqlExpression, connection);
                using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    data = await ReadDataAsync(reader);
                    await connection.CloseAsync();
                }
            }
            return data;
        }

        /// <summary>
        /// Прочитать данные в список объектов <see cref="Residue"/>.
        /// </summary>
        /// <param name="reader">Поток данных из БД.</param>
        /// <returns>Список объектов <see cref="Residue"/>.</returns>
        public static async Task<IEnumerable<Residue>> ReadDataAsync(NpgsqlDataReader reader)
        {
            var results = new List<Residue>();
            while (await reader.ReadAsync())
            {
                var item = new Residue();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);
                    var value = reader[i];

                    if (!reader.IsDBNull(i))
                    {
                        var mapping = _columnMapping.GetValueOrDefault(columnName, null);
                        if (mapping != null)
                        {
                            var convertedValue = Convert.ChangeType(value, mapping.PropertyType);
                            mapping.SetValue(item, convertedValue);
                        }
                    }
                }
                results.Add(item);
            }
            await reader.CloseAsync();
            return results;
        }
    }
}