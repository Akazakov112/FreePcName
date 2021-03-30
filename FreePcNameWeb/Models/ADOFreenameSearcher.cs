using ADODB;
using FreePcNameWeb.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace FreePcNameWeb.Models
{
    /// <summary>
    /// Поисковик свободных имен.
    /// </summary>
    public class ADOFreenameSearcher : IFreenameSearcher
    {
        /// <summary>
        /// Конфигураия приложения.
        /// </summary>
        private readonly IConfiguration config;

        /// <summary>
        /// Минимальное значение числовой части имени устройства.
        /// </summary>
        private const int NAME_COUNT_MIN = 1;

        /// <summary>
        /// Максимальное значение числовой части имени устройства.
        /// </summary>
        private readonly int nameCountMax;

        /// <summary>
        /// Массив допустимых числовых частей доменных имен устройств.
        /// </summary>
        private readonly int[] numbersOfNames;

        /// <summary>
        /// Подключение к Active Directory.
        /// </summary>
        private readonly Connection connection;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ADOFreenameSearcher(IConfiguration configuration)
        {
            config = configuration;
            connection = new Connection();
            nameCountMax = config.GetValue<int>("MaxNamesCount");
            numbersOfNames = Enumerable.Range(NAME_COUNT_MIN, nameCountMax).ToArray();
        }


        /// <summary>
        /// Возвращает объект свободных имен устройств.
        /// </summary>
        /// <param name="prefix">Префикс подразделения</param>
        /// <returns></returns>
        public Freenames GetFreenames(string prefix)
        {
            connection.Open("Provider=ADsDSOObject");
            var newFreeNames = new Freenames(GetFreename($"{prefix}-"), GetFreename($"N-{prefix}-"), GetFreename($"ARM-{prefix}-"));
            connection.Close();
            return newFreeNames;
        }

        /// <summary>
        /// Возвращает первое свободное имя устройства по префиксу имени устройства и ОСП.
        /// </summary>
        /// <param name="devicePrefix">Префикс имени устройства и ОСП, пример ARM-PC-</param>
        /// <returns></returns>
        private string GetFreename(string devicePrefix)
        {
            // Создает коллецию всех возможных имен вида устройства по префиксу.
            var allNames = new List<string>();
            foreach (int num in numbersOfNames)
            {
                string numberPart = num.ToString().Length == 4 ? num.ToString("0000") : num.ToString("000");
                allNames.Add((devicePrefix + numberPart).ToUpper());
            }
            // Получает коллекцию занятых имен в Active Directory.
            var busyNames = new List<string>();
            string command = $"SELECT cn FROM '{config.GetSection("DomainLDAP").Value}' WHERE objectClass='computer' AND sAMAccountName='{devicePrefix}*' Order by Name";
            Recordset set = connection.Execute(command, out object _, 0);
            for (; !set.EOF; set.MoveNext())
            {
                string pcnum = set.Fields["cn"].Value.ToString();
                busyNames.Add(pcnum.ToUpper());
            }
            // Возвращает первое свободное имя из списка свободных имен, 
            // получаемого из разницы списков всех возможных и занятых имен.
            return allNames.Except(busyNames).FirstOrDefault();
        }
    }
}
