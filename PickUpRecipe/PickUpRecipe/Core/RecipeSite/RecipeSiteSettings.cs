namespace PickUpRecipe.Core.RecipeSite
{
    /// <summary>
    /// Настройки для сайта.
    /// </summary>
    class RecipeSiteSettings : IParserSettings
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="address">Адрес сайта.</param>
        public RecipeSiteSettings(string address)
        {
            BaseUrl = address;
        }
        
        /// <inheritdoc/>
        public string BaseUrl { get; set; }
    }
}
