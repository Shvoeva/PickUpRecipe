using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;

namespace PickUpRecipe.Core.Parsers 
{
	/// <summary>
	/// Парсер ингредиентов.
	/// </summary>
	public class IngredientsParser : IParser<string[]>
	{
		/// <summary>
		/// Название класса для элемента ul.
		/// </summary>
		private const string ClassUl = "wprm-recipe-ingredients";

		/// <inheritdoc/>
		public string[] Parse(IHtmlDocument document)
		{
			var listResult = new List<string>();
			if (document == null)
			{
				return listResult.ToArray();
			}

			var uls = document.GetElementsByTagName("ul");
			listResult.AddRange(from HtmlElement ul in uls
				where ul.GetAttribute("class") == ClassUl
				from liElement in ul.GetElementsByTagName("li")
				select (HtmlElement)liElement
				into li
				select li.GetElementsByTagName("span")
				into spans
				select spans.Cast<HtmlElement>()
					.Aggregate(string.Empty, (current, span) =>
						current + span.TextContent + " "));

			return listResult.ToArray();
		}
	}
}
