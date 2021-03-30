namespace FreePcNameWeb.Models.Interfaces
{
    /// <summary>
    /// Интерфейс поискаовика свободных имен.
    /// </summary>
    public interface IFreenameSearcher
    {
        /// <summary>
        /// Возвращает свободные имена устройств.
        /// </summary>
        /// <returns></returns>
        Freenames GetFreenames(string prefix);
    }
}
