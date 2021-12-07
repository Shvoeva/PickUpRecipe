using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Html.Dom;

namespace PickUpRecipe.Core.Parsers
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

		private const string ImgName = "lazyloaded";

		/// <inheritdoc/>
		public string[] Parse(IHtmlDocument document)
		{
			var list = new List<string>();
			var item1 = document.QuerySelectorAll("h1")
				.Where(item => item.ClassName != null &&
							   item.ClassName.Contains("entry-title"))
				.ToArray();
			list.Add(item1[0].TextContent);

			var item2 = document.QuerySelectorAll("img");
			var src = string.Empty;
			var classImg = "wp-image-";
			foreach (var img in item2)
			{
				if (img.ClassName != null && img.ClassName.Contains(classImg))
				{
					Regex r = new Regex(Regex.Escape("data-src=\"") + "(.*?)" + Regex.Escape("\""));
					MatchCollection matches = r.Matches(img.OuterHtml);
					src = matches[0].Groups[1].Value;
					break;
				}
			}

			list.Add(src);
			return list.ToArray();
		}
	}
}
