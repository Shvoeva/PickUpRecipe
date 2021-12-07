namespace PickUpRecipe.Core.ParserSettings
{
	/// <summary>
	/// Настройки для сайта.
	/// </summary>
	public class RecipeSiteSettings : ParserSettingsBase
	{
		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="address">Адрес сайта.</param>
		public RecipeSiteSettings(string address) : base(address) { }
	}
}
