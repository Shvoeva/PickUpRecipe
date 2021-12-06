using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PickUpRecipe.Core.SiteWithDish
{
    class SiteWithDishParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();

            var item1 = document.QuerySelectorAll("h1")
                .Where(item => item.ClassName != null && item.ClassName.Contains("entry-title")).ToArray();
            list.Add(item1[0].TextContent);

            var item2 = document.GetElementsByClassName("wp-block-image")[0];
            Regex r = new Regex(Regex.Escape("data-src=\"") + "(.*?)" + Regex.Escape("\""));
            MatchCollection matches = r.Matches(item2.InnerHtml);
            var line = matches[0].Groups[1].Value;
            if (matches[0].Groups[1].Value == "https://www.carolinescooking.com/wp-content/uploads/2014/10/PB091726-sq-1024x1021.jpg")
            {
                line = matches[1].Groups[1].Value;
            }
            list.Add(line);

            return list.ToArray();
        }
    }
}
