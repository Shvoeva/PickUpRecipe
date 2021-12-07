using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;

namespace PickUpRecipe.Core.RecipeSite
{
	/// <summary>
	/// Парсер сайта.
	/// </summary>
	public class RecipeSiteParser : IParser<string[]>
	{
		/// <inheritdoc/>
		public string[] Parse(IHtmlDocument document)
		{
			var items = document.QuerySelectorAll("a")
				.Where(item => item.ClassName != null &&
							   item.ClassName.Contains("entry-title-link"))
				.ToArray();
			return items.Select(item => item.GetAttribute("href")).ToArray();
		}
	}
}
