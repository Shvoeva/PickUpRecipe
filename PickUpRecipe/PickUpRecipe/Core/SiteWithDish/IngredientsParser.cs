using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PickUpRecipe.Core.SiteWithDish 
{
    class IngredientsParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var listAmounts = new List<string>();
            var listUnits = new List<string>();
            var listNames = new List<string>();
            var listResult = new List<string>();

            if (document != null)
            {
                var uls = document.GetElementsByTagName("ul");
                foreach (HtmlElement ul in uls)
                {
                    if (ul.GetAttribute("class") == "wprm-recipe-ingredients")
                    {
                        var lis = ul.GetElementsByTagName("li");
                        foreach (HtmlElement li in lis)
                        {
                            var spans = li.GetElementsByTagName("span");
                            var spanString = string.Empty;
                            foreach (HtmlElement span in spans)
                            {
                                spanString += span.TextContent + " ";
                            }

                            listResult.Add(spanString);
                        }
                    }
                }
            }
            
            return listResult.ToArray();
        }
    }
}
