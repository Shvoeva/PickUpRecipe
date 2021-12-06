using AngleSharp.Html.Dom;

namespace PickUpRecipe.Core
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
