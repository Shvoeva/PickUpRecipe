using AngleSharp.Html.Dom;

namespace PickUpRecipe.Core
{
	/// <summary>
	/// Интерфейс работы с парсом.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IParser<T> where T : class
	{
		/// <summary>
		/// Спарсить.
		/// </summary>
		/// <param name="document">Документ, в котором происходит парс.</param>
		/// <returns>Спаршенный элемент(ы).</returns>
		T Parse(IHtmlDocument document);
	}
}
