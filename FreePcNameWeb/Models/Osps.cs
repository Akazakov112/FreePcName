namespace FreePcNameWeb.Models
{
    /// <summary>
    /// ОСП.
    /// </summary>
    public partial class Osps
    {
        /// <summary>
        /// Id в бд.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Osp { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Префикс имени устройств.
        /// </summary>
        public string ShortName { get; set; }
    }
}
