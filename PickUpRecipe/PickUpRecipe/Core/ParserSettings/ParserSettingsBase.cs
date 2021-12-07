namespace PickUpRecipe.Core.ParserSettings
{
	/// <summary>
	/// Базовый класс для настроек парса.
	/// </summary>
	public abstract class ParserSettingsBase : IParserSettings
	{
		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="address">Адрес сайта.</param>
		protected ParserSettingsBase(string address)
		{
			BaseUrl = address;
		}

		/// <inheritdoc/>
		public string BaseUrl { get; set; }
	}
}