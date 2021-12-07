namespace PickUpRecipe.Core.SiteWithDish
{
    /// <summary>
    /// Настройки для парса рецепта.
    /// </summary>
    public class SiteWithDishSetting : IParserSettings
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="address">Адрес сайта.</param>
        public SiteWithDishSetting(string address)
        {
            BaseUrl = address;
        }

        /// <inheritdoc/>
        public string BaseUrl { get; set; }
    }
}
