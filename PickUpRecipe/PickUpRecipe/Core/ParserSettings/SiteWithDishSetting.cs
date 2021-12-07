namespace PickUpRecipe.Core.ParserSettings
{
	/// <summary>
	/// Настройки для парса рецепта.
	/// </summary>
	public class SiteWithDishSetting : ParserSettingsBase
	{
		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="address">Адрес сайта.</param>
		public SiteWithDishSetting(string address) : base(address) { }
	}
}
