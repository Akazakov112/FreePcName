using System.Collections.Generic;
using System.Data.SqlClient;

namespace FreePcName.Model
{
    /// <summary>
    /// Получает список подразделений из SQL БД.
    /// </summary>
    class DivisionListBuilder
    {
        /// <summary>
        /// Подключение.
        /// </summary>
        private readonly SqlConnection connection;

        /// <summary>
        /// Команда.
        /// </summary>
        private readonly SqlCommand command;

        /// <summary>
        /// Построитель строки подключения.
        /// </summary>
        private readonly SqlConnectionStringBuilder conStr;

        /// <summary>
        /// Приватный конструктор.
        /// </summary>
        private DivisionListBuilder()
        {
            conStr = new SqlConnectionStringBuilder()
            {
                DataSource = Properties.Settings.Default.DbServerUrl,
                InitialCatalog = Properties.Settings.Default.DbName,
                IntegratedSecurity = true,
                Pooling = true
            };
            connection = new SqlConnection(conStr.ConnectionString);
            command = new SqlCommand()
            {
                Connection = connection
            };
        }

        /// <summary>
        /// Создает список подразделений.
        /// </summary>
        /// <returns></returns>
        public List<Division> GetDivisionsList()
        {
            var list = new List<Division>();
            command.CommandText = 
                $"SELECT DISTINCT {Properties.Settings.Default.DivisionFieldName}, {Properties.Settings.Default.CityFieldName}, {Properties.Settings.Default.AddressFieldName}, {Properties.Settings.Default.PrefixFieldName} " +
                $"FROM {Properties.Settings.Default.TableName} " +
                $"WHERE({Properties.Settings.Default.CityFieldName} IS NOT NULL AND {Properties.Settings.Default.CityFieldName} != '') " +
                $"AND({Properties.Settings.Default.PrefixFieldName} IS NOT NULL AND {Properties.Settings.Default.PrefixFieldName} != '' AND {Properties.Settings.Default.PrefixFieldName} != 'NULL') " +
                $"AND({Properties.Settings.Default.AddressFieldName} IS NOT NULL AND {Properties.Settings.Default.AddressFieldName} != '') " +
                $"ORDER BY OSP";
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Division(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
            }
            connection.Close();
            return list;
        }

        /// <summary>
        /// Построитель списка подразделений.
        /// </summary>
        private static DivisionListBuilder builder;

        /// <summary>
        /// Построитель списка подразделений.
        /// </summary>
        public static DivisionListBuilder Builder
        {
            get
            {
                if (builder is null)
                {
                    builder = new DivisionListBuilder();
                }
                return builder;
            }
        }
    }
}