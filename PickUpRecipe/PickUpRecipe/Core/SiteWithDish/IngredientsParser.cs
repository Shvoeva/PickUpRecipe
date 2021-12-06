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

            //var amounts = document.QuerySelectorAll("span").Where(amount => amount.ClassName != null && amount.ClassName.Contains("wprm-recipe-ingredient-amount")).ToArray();
            //var units = document.QuerySelectorAll("span").Where(unit => unit.ClassName != null && unit.ClassName.Contains("wprm-recipe-ingredient-unit")).ToArray();
            //var names = document.QuerySelectorAll("span").Where(name => name.ClassName != null && name.ClassName.Contains("wprm-recipe-ingredient-name")).ToArray();

            //foreach (var amount in amounts)
            //{
            //    listAmounts.Add(amount.TextContent);
            //    count++;
            //}

            //foreach (var unit in units)
            //{
            //    listUnits.Add(unit.TextContent);
            //}

            //foreach (var name in names)
            //{
            //    listNames.Add(name.TextContent);
            //}

            //int count = listNames.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    //listResult.Add(listAmounts[i]);
            //    //listResult.Add(listUnits[i]);
            //    listResult.Add(listNames[i]);
            //}

            return listResult.ToArray();
        }
    }
}
