using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;

namespace PickUpRecipe.Core.RecipeSite
{
    /// <summary>
    /// Парсер сайта.
    /// </summary>
    class RecipeSiteParser : IParser<string[]>
    {
        /// <inheritdoc/>
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("a")
	            .Where(item => item.ClassName != null &&
	                           item.ClassName.Contains("entry-title-link"))
	            .ToArray();

            foreach (var item in items)
            {
                list.Add(item.GetAttribute("href"));
            }

            return list.ToArray();
        }
    }
}
