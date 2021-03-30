using System.Collections.Generic;

namespace FreePcName.Model
{
    /// <summary>
    /// Свободные имена устройств.
    /// </summary>
    class Freenames
    {
        /// <summary>
        /// Свободное имя пк.
        /// </summary>
        public List<string> PcNames { get; }

        /// <summary>
        /// Свободное имя ноутбука.
        /// </summary>
        public List<string> NotebookNames { get; }

        /// <summary>
        /// Свободное имя АРМ.
        /// </summary>
        public List<string> ArmNames { get; }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="pcname"></param>
        /// <param name="notename"></param>
        /// <param name="armname"></param>
        public Freenames(List<string> pcname, List<string> notename, List<string> armname)
        {
            PcNames = pcname;
            NotebookNames = notename;
            ArmNames = armname;
        }
    }
}