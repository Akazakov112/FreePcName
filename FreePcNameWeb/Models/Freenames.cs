namespace FreePcNameWeb.Models
{
    /// <summary>
    /// Свободные имена хостов.
    /// </summary>
    public class Freenames
    {
        /// <summary>
        /// Свободное имя пк.
        /// </summary>
        public string PcName { get; }

        /// <summary>
        /// Свободное имя ноутбука.
        /// </summary>
        public string NotebookName { get; }

        /// <summary>
        /// Свободное имя АРМ.
        /// </summary>
        public string ArmName { get; }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="pcname"></param>
        /// <param name="notename"></param>
        /// <param name="armname"></param>
        public Freenames(string pcname, string notename, string armname)
        {
            PcName = pcname;
            NotebookName = notename;
            ArmName = armname;
        }
    }
}
