using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PickUpRecipe.Core.SiteWithDish
{
    /// <summary>
    /// Парсер сайта с рецептом.
    /// </summary>
    public class SiteWithDishParser : IParser<string[]>
    {
        /// <summary>
        /// Игнорируемая ссылка картинки.
        /// </summary>
	    private const string IgnoreImageUrl =
		    "https://www.carolinescooking.com/wp-content/uploads/2014/10/PB091726-sq-1024x1021.jpg";

        /// <summary>
        /// Имя класса для картинки.
        /// </summary>
        private const string ImageClassName = "wp-block-image";

        /// <inheritdoc/>
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var item1 = document.QuerySelectorAll("h1")
                .Where(item => item.ClassName != null &&
                               item.ClassName.Contains("entry-title"))
                .ToArray();
            list.Add(item1[0].TextContent);

            var item2 = document.GetElementsByClassName(ImageClassName).First();
            var regex = new Regex(Regex.Escape("data-src=\"") + "(.*?)" + Regex.Escape("\""));
            var matches = regex.Matches(item2.InnerHtml);
            var line = matches[0].Groups[1].Value;
            if (matches[0].Groups[1].Value == IgnoreImageUrl)
            {
                line = matches[1].Groups[1].Value;
            }

            list.Add(line);
            return list.ToArray();
        }
    }
}
