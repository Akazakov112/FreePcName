namespace FreePcName.Model
{
    /// <summary>
    /// Подразделение.
    /// </summary>
    struct Division
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Город.
        /// </summary>
        public string City { get; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// Префикс.
        /// </summary>
        public string Prefix { get; }

        /// <summary>
        /// Конструктор для базы данных.
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="city">Город</param>
        /// <param name="address">Адрес</param>
        /// <param name="prefix">Префикс</param>
        public Division(string name, string city, string address, string prefix)
        {
            Name = name;
            City = city;
            Address = address;
            Prefix = prefix;
        }
    }
}