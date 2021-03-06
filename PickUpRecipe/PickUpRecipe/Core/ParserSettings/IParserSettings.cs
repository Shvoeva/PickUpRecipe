namespace PickUpRecipe.Core.ParserSettings
{
	/// <summary>
	/// Настройки парсера.
	/// </summary>
	public interface IParserSettings
	{
		/// <summary>
		/// Возвращает и задает адрес сайта
		/// </summary>
		string BaseUrl { get; set; }
	}
}
